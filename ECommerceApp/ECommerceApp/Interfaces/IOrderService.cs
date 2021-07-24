using System.Threading.Tasks;
using ECommerceApp.Models;

namespace ECommerceApp.Interfaces
{
    public interface IOrderService
    {
        Task<int> PlaceOrder(Order order);
    }
}
