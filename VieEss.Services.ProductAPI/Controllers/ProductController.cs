using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VietTast.Services.ProductAPI.Data;
using VietTast.Services.ProductAPI.Models;
using VietTast.Services.ProductAPI.Models.DTO;

namespace VietTast.Services.ProductAPI.Controllers
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
        [HttpGet]
        public ResponseDTO Get() {             
            try
            {
                IEnumerable<Product> products = _dbContext.Products.ToList();
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
        public ResponseDTO Post([FromBody] ProductCreateDTO productDTO)
        {
            try
            {
                Product product = _mapper.Map<Product>(productDTO);
                product.CreatedAt = DateTime.UtcNow;
                product.UpdatedAt = DateTime.UtcNow;
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
