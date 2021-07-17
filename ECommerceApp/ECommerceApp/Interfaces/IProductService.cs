using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerceApp.Models;

namespace ECommerceApp.Interfaces
{
    public interface IProductService
    {
        Task<List<Category>> GetCategories();
        Task<List<Product>> GetPopularProducts();
        Task<Product> GetProductInfo(int productId);
    }
}
