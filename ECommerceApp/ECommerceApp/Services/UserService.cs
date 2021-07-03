using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ECommerceApp.Interfaces;
using ECommerceApp.Models;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace ECommerceApp.Services
{
    public class UserService : IUserService
    {
        public async Task<bool> Login(string email, string password)
        {
            var login = new
            {
                Email = email,
                Password = password
            };

            var json = JsonConvert.SerializeObject(login);

            var httpClient = new HttpClient();
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://ecommercebackendapi.azurewebsites.net/api/Accounts/Login", content);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<Session>(jsonResponse);

            Preferences.Set("accessToken", result.AccessToken);
            Preferences.Set("userId", result.UserId);
            Preferences.Set("userName", result.UserName);
            Preferences.Set("tokenExpirationTime", result.ExpirationTime);
            Preferences.Set("currentTime", DateTimeOffset.Now.ToUnixTimeSeconds());

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Register(string name, string email, string password)
        {
            var register = new
            {
                Name = name,
                Email = email,
                Password = password
            };

            var json = JsonConvert.SerializeObject(register);

            var httpClient = new HttpClient();
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://ecommercebackendapi.azurewebsites.net/api/Accounts/Register", content);

            return response.IsSuccessStatusCode;
        }
    }
}
