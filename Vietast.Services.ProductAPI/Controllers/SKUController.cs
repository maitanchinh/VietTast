using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vietast.Services.ProductAPI.Data;
using Vietast.Services.ProductAPI.Models;
using Vietast.Services.ProductAPI.Models.DTO;

namespace Vietast.Services.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SKUController : ControllerBase
    {
        private readonly ProductDbContext _dbContext;
        private ResponseDTO _response;
        private readonly IMapper _mapper;
        public SKUController(ProductDbContext dbContext, IMapper mapper) {
            _dbContext = dbContext;
            _response = new ResponseDTO();
            _mapper = mapper;
        }
        [HttpGet]
        public ResponseDTO Get() {             
            try
            {
                IEnumerable<SKU> skus = _dbContext.SKUs.ToList();
                _response.Result = _mapper.Map<IEnumerable<SKUDTO>>(skus);
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
                SKU sku = _dbContext.SKUs.First(s => s.SKUId == id);
                _response.Result = _mapper.Map<SKUDTO>(sku);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        public ResponseDTO Post([FromBody] SKUCreateDTO skuDTO)
        {
            try
            {
                SKU sku = _mapper.Map<SKU>(skuDTO);
                sku.CreatedAt = DateTime.UtcNow;
                sku.UpdatedAt = DateTime.UtcNow;
                _dbContext.SKUs.Add(sku);
                _dbContext.SaveChanges();
                _response.Result = _mapper.Map<SKUCreateDTO>(sku);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        public ResponseDTO Put([FromBody] SKUUpdateDTO skuDTO)
        {
            try
            {
                SKU sku = _mapper.Map<SKU>(skuDTO);
                sku.UpdatedAt = DateTime.UtcNow;
                _dbContext.SKUs.Update(sku);
                _dbContext.SaveChanges();
                _response.Result = _mapper.Map<SKUDTO>(sku);
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
                SKU sku = _dbContext.SKUs.First(s => s.SKUId == id);
                _dbContext.SKUs.Remove(sku);
                _dbContext.SaveChanges();
                _response.Result = _mapper.Map<SKUDTO>(sku);
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
