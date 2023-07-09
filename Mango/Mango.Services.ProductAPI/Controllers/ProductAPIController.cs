using AutoMapper;
using Mango.Services.ProductAPI.Data;
using Mango.Services.ProductAPI.Model;
using Mango.Services.ProductAPI.Model.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace Mango.Services.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private IMapper _mapper;
        private ResponseDto _respose;
        public ProductAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _respose = new ResponseDto();
            _respose.IsSuccess = true;

        }


        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Product> objList = _db.Products.ToList();
                _respose.Result = _mapper.Map<IEnumerable<ProductDto>>(objList);
                _respose.IsSuccess = true;
            }
            catch (Exception er)
            {
                _respose.Messages = er.Message;
                _respose.IsSuccess = false;

            }
            return _respose;
        }

        [HttpGet]
        [Route("{id:int}")]
        public object Get(int id)
        {
            try
            {
                Product obj = _db.Products.First(i => i.ProductId == id);


                //ProductDto productDto = new ProductDto()
                //{
                //    ProductId = obj.ProductId,
                //    ProductCode = obj.ProductCode,
                //    DiscountAmount = obj.DiscountAmount,
                //    MinAmount = obj.MinAmount
                //};

                _respose.Result = _mapper.Map<ProductDto>(obj);
                _respose.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _respose.IsSuccess = false;
                _respose.Messages = ex.Message;
            }

            return _respose;
        }
       
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Post([FromBody] ProductDto productdto)
        {
            try
            {
                Product obj = _mapper.Map<Product>(productdto);
                _db.Products.Add(obj);
                _db.SaveChanges();

                _respose.IsSuccess = true;
                _respose.Result = _mapper.Map<ProductDto>(obj);

            }
            catch (Exception ex)
            {
                _respose.IsSuccess = false;
                _respose.Messages = ex.Message;

            }
            return _respose;
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Delete(int id)
        {
            try
            {
                Product obj = _db.Products.First(x => x.ProductId == id);
                _db.Products.Remove(obj);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _respose.IsSuccess = false;
                _respose.Messages = ex.Message;
            }
            return _respose;
        }
    }
}
