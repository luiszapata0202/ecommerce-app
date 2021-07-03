using System;
using System.Threading.Tasks;
using ECommerceApp.Interfaces;
using ECommerceApp.Models;
using Xamarin.Essentials;

namespace ECommerceApp.Services
{
    public class UserService : IUserService
    {
        private readonly IApiService _apiService;

        public UserService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<bool> Login(string email, string password)
        {
            var login = new
            {
                Email = email,
                Password = password
            };

            var result = await _apiService.PostAsync<Session>(login, "Accounts/Login");

            if (!result.IsSuccess)
            {
                return false;
            }

            Preferences.Set("accessToken", result.Result.AccessToken);
            Preferences.Set("userId", result.Result.UserId);
            Preferences.Set("userName", result.Result.UserName);
            Preferences.Set("tokenExpirationTime", result.Result.ExpirationTime);
            Preferences.Set("currentTime", DateTimeOffset.Now.ToUnixTimeSeconds());

            return true;
        }

        public async Task<bool> Register(string name, string email, string password)
        {
            var register = new
            {
                Name = name,
                Email = email,
                Password = password
            };

            var result = await _apiService.PostAsync<string>(register, "Accounts/Register");

            return result.IsSuccess;
        }
    }
}
