namespace Domain.Models
{
    public class BasketItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public decimal price { get; set; }
        public int quantity { get; set; }
    }
}