using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Model;
using Mango.Services.CouponAPI.Model.Dto;
using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private IMapper _mapper;
        private ResponseDto _respose;
        public CouponAPIController(AppDbContext db, IMapper mapper)
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
                IEnumerable<Coupon> objList = _db.Coupons.ToList();
                _respose.Result = _mapper.Map<IEnumerable<CouponDto>>(objList);
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
        [Route("GetByCode/{id:int}")]
        public object Get(int id)
        {
            try
            {
                Coupon obj = _db.Coupons.First(i => i.CouponId == id);


                //CouponDto couponDto = new CouponDto()
                //{
                //    CouponId = obj.CouponId,
                //    CouponCode = obj.CouponCode,
                //    DiscountAmount = obj.DiscountAmount,
                //    MinAmount = obj.MinAmount
                //};

                _respose.Result = _mapper.Map<CouponDto>(obj);
                _respose.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _respose.IsSuccess = false;
                _respose.Messages = ex.Message;
            }

            return _respose;
        }

        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDto GetByCode(string code)
        {
            try
            {
                Coupon obj = _db.Coupons.First(x => x.CouponCode.ToLower() == code.ToLower());

                _respose.Result = _mapper.Map<CouponDto>(obj);
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
        public ResponseDto Post([FromBody] CouponDto coupondto)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(coupondto);
                _db.Coupons.Add(obj);
                _db.SaveChanges();

                _respose.IsSuccess = true;
                _respose.Result = _mapper.Map<CouponDto>(obj);

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
        public ResponseDto Delete(int id)
        {
            try
            {
                Coupon obj = _db.Coupons.First(x => x.CouponId == id);
                _db.Coupons.Remove(obj);
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
