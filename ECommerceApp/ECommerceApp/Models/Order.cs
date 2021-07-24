namespace ECommerceApp.Models
{
    public class Order
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public double OrderTotal { get; set; }
        public int UserId { get; set; }
    }
}
