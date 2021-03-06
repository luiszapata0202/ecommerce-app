using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerceApp.Models;

namespace ECommerceApp.Interfaces
{
    public interface IShoppingCartService
    {
        Task<int> GetCartItemsCount();
        Task<bool> AddProductToCart(Product product, int quantity);
        Task<List<ShoppingCartItem>> GetShoppingCartItems();
        Task<bool> ClearShoppingCartItems();
    }
}
