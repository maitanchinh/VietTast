using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vietast.Services.ProductAPI.Models;
using Vietast.Services.ProductAPI.Data;
using Vietast.Services.ProductAPI.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace Vietast.Services.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ProductDbContext _dbContext;
        private ResponseDTO _response;
        private readonly IMapper _mapper;
        public CategoryController(ProductDbContext dbContext, IMapper mapper) {
            _dbContext = dbContext;
            _response = new ResponseDTO();
            _mapper = mapper;
        }

        [HttpGet]
        public ResponseDTO Get()
        {
            try
            {
                IEnumerable<Category> categories = _dbContext.Categories.ToList();
                _response.Result = categories;
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
                Category category = _dbContext.Categories.First(c => c.CategoryId == id);
                _response.Result = _mapper.Map<CategoryDTO>(category);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public ResponseDTO Post([FromBody] CategoryCreateDTO categoryDTO)
        {
            try
            {
                Category category = _mapper.Map<Category>(categoryDTO);
                category.CreatedAt = DateTime.UtcNow;
                category.UpdatedAt = DateTime.UtcNow;
                _dbContext.Categories.Add(category);
                _dbContext.SaveChanges();
                _response.Result = _mapper.Map<CategoryDTO>(category);
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
        public ResponseDTO Put([FromBody] CategoryUpdateDTO categoryDTO)
        {
            try
            {
                Category category = _mapper.Map<Category>(categoryDTO);
                category.UpdatedAt = DateTime.UtcNow;
                _dbContext.Categories.Update(category);
                _dbContext.SaveChanges();
                _response.Result = _mapper.Map<CategoryDTO>(category);
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
                Category category = _dbContext.Categories.First(c => c.CategoryId == id);
                _dbContext.Categories.Remove(category);
                _dbContext.SaveChanges();
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
