using System.Threading.Tasks;
using ECommerceApp.Models;

namespace ECommerceApp.Interfaces
{
    public interface IApiService
    {
        Task<Response<T>> PostAsync<T>(object model, string service) where T : class;
        Task<Response<T>> GetAsync<T>(string service) where T : class;
    }
}
