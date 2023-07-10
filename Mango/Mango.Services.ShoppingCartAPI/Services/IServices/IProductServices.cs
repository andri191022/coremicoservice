using Mango.Services.ShoppingCartAPI.Model.Dto;

namespace Mango.Services.ShoppingCartAPI.Services.IServices
{
    public interface  IProductServices
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
