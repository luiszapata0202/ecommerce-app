using System.Threading.Tasks;

namespace ECommerceApp.Interfaces
{
    public interface IUserService
    {
        Task<bool> Register(string name, string email, string password);
        Task<bool> Login(string email, string password);
    }
}
