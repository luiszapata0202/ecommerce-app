using System;
using System.Threading.Tasks;

namespace ECommerceApp.Interfaces
{
    public interface IShoppingCartService
    {
        Task<int> GetCartItemsCount();
    }
}
