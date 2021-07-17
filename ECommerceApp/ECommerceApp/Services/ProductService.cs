using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerceApp.Interfaces;
using ECommerceApp.Models;

namespace ECommerceApp.Services
{
    public class ProductService : IProductService
    {
        #region Private Attributes
        private readonly IApiService _apiService;
        #endregion

        #region Constructor
        public ProductService(IApiService apiService)
        {
            _apiService = apiService;
        }
        #endregion

        #region Methods
        public async Task<List<Category>> GetCategories()
        {
            var response = await _apiService.GetAsync<List<Category>>("Categories");

            return response.Result;
        }

        public async Task<List<Product>> GetPopularProducts()
        {
            var response = await _apiService.GetAsync<List<Product>>("Products/PopularProducts");

            return response.Result;
        }

        public async Task<Product> GetProductInfo(int productId)
        {
            var response = await _apiService.GetAsync<Product>($"Products/{productId}");

            return response.Result;
        }

        public async Task<List<Product>> GetProductsByCategory(int categoryId)
        {
            var response = await _apiService.GetAsync<List<Product>>($"Products/ProductsByCategory/{categoryId}");

            return response.Result;
        }
        #endregion
    }
}
