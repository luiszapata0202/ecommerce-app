namespace ECommerceApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string FullImageUrl => $"{AppSettings.ApiUrl}/{ImageUrl}";
    }
}
