using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vietast.Services.ProductAPI.Data;
using Vietast.Services.ProductAPI.Models;
using Vietast.Services.ProductAPI.Models.DTO;
using Vietast.Services.ProductAPI.Services;

namespace Vietast.Services.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductDbContext _dbContext;
        private ResponseDTO _response;
        private readonly IMapper _mapper;
        public ProductController(ProductDbContext dbContext, IMapper mapper) {
            _dbContext = dbContext;
            _response = new ResponseDTO();
            _mapper = mapper;
        }
        [HttpPost("filter")]
        public ResponseDTO GetFilter([FromBody] Filter<ProductFilterDTO> filter)
        {
            try
            {
                IQueryable<Product> query = _dbContext.Products.Include(p => p.Category);

                // Apply filtering
                if (filter.Criteria != null)
                {
                    if (!string.IsNullOrEmpty(filter.Criteria.Name))
                    {
                        query = query.Where(p => p.Name.Contains(filter.Criteria.Name));
                    }
                    if (filter.Criteria.CategoryId != null)
                    {
                        query = query.Where(p => p.CategoryId == filter.Criteria.CategoryId);
                    }
                }

                // Apply sorting
                if (!string.IsNullOrEmpty(filter.SortBy))
                {
                    if (filter.SortOrder != "asc")
                    {
                        query = query.OrderByDescending(e => EF.Property<object>(e, filter.SortBy));
                    }
                    else
                    {
                        query = query.OrderBy(e => EF.Property<object>(e, filter.SortBy));
                    }
                }

                // Apply pagination
                query = query.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);

                IEnumerable<Product> products = query.ToList();
                _response.Result = _mapper.Map<IEnumerable<ProductDTO>>(products);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet("{id}")]
        public ResponseDTO Get(Guid id)
        {
            try
            {
                Product product = _dbContext.Products.First(p => p.ProductId == id);
                _response.Result = _mapper.Map<ProductDTO>(product);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ResponseDTO> Post([FromForm] ProductCreateDTO productDTO)
        {
            try
            {
                Product product = _mapper.Map<Product>(productDTO);
                product.CreatedAt = DateTime.UtcNow;
                product.UpdatedAt = DateTime.UtcNow;
                using (var stream = productDTO.Image.OpenReadStream())
                {
                    var firebaseStorageService = new FirebaseStorageService();
                    string imageUrl = await firebaseStorageService.UploadImageAsync(DateTime.UtcNow +".jpg", stream);
                    product.ImageUrl = imageUrl;
                }
                    _dbContext.Products.Add(product);
                _dbContext.SaveChanges();
                _response.Result = _mapper.Map<ProductDTO>(product);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public ResponseDTO Put([FromBody] ProductUpdateDTO productDTO)
        {
            try
            {
                var product = _dbContext.Products.Find(productDTO.ProductId);
                if (product == null)
                {
                    throw new Exception("Product not found");
                }
                _mapper.Map(productDTO, product);
                product.UpdatedAt = DateTime.UtcNow;
                //_dbContext.Products.Update(product);
                _dbContext.SaveChanges();
                _response.Result = _mapper.Map<ProductDTO>(product);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDTO Delete(Guid id)
        {
            try
            {
                Product product = _dbContext.Products.First(p => p.ProductId == id);
                _dbContext.Products.Remove(product);
                _dbContext.SaveChanges();
                _response.Result = _mapper.Map<ProductDTO>(product);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
