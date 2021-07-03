using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerceApp.Interfaces;
using ECommerceApp.Models;

namespace ECommerceApp.Services
{
    public class ProductService : IProductService
    {
        private readonly IApiService _apiService;

        public ProductService(IApiService apiService)
        {
            _apiService = apiService;
        }

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
    }
}
