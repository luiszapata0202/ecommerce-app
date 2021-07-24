using Newtonsoft.Json;

namespace ECommerceApp.Models
{
    public class ShoppingCartItem
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("totalAmount")]
        public double TotalAmount { get; set; }

        [JsonProperty("qty")]
        public int Quantity { get; set; }

        [JsonProperty("productName")]
        public string ProductName { get; set; }
    }
}
