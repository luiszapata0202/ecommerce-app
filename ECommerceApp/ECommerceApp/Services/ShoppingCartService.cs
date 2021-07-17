using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerceApp.Interfaces;
using ECommerceApp.Models;
using Xamarin.Essentials;

namespace ECommerceApp.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        #region Private Attributes
        private readonly IApiService _apiService;
        #endregion

        #region Constructor
        public ShoppingCartService(IApiService apiService)
        {
            _apiService = apiService;
        }
        #endregion

        #region Methods
        public async Task<int> GetCartItemsCount()
        {
            int userId = Preferences.Get("userId", 0);

            var response = await _apiService.GetAsync<Dictionary<string, int>>($"ShoppingCartItems/TotalItems/{userId}");
            var result = response.Result;

            var count = result["totalItems"];

            return count;            
        }

        public async Task<bool> AddProductToCart(Product product, int quantity)
        {
            int userId = Preferences.Get("userId", 0);

            var data = new
            {
                Price = product.Price,
                Qty = quantity,
                TotalAmount = product.Price * quantity,
                ProductId = product.Id,
                CustomerId = userId
            };

            var response = await _apiService.PostAsync<string>(data, "ShoppingCartItems", true);

            return response.IsSuccess;
        }
        #endregion
    }
}
