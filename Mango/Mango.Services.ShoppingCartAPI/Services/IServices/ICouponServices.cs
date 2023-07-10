using Mango.Services.ShoppingCartAPI.Model.Dto;

namespace Mango.Services.ShoppingCartAPI.Services.IServices
{
    public interface ICouponServices
    {
        Task<CouponDto> GetCoupon(string couponCode);
    }
}
