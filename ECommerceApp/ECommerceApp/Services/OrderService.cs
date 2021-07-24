using System;
using System.Threading.Tasks;
using ECommerceApp.Interfaces;
using ECommerceApp.Models;

namespace ECommerceApp.Services
{
    public class OrderService : IOrderService
    {
        #region Private Attributes
        private readonly IApiService _apiService;
        #endregion

        #region Constructor
        public OrderService(IApiService apiService)
        {
            _apiService = apiService;
        }
        #endregion


        public async Task<int> PlaceOrder(Order order)
        {
            var response = await _apiService.PostAsync<OrderResponse>(order, "Orders", authRequired: true);

            return response.Result.OrderId;
        }
    }
}
