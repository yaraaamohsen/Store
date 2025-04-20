using System.ComponentModel.DataAnnotations;

namespace Shared
{
    public class BasketItemDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string PictureUrl { get; set; }
        
        [Range(0, double.MaxValue)]
        public decimal price { get; set; }

        [Range(1, 99)]
        public int quantity { get; set; }
    }
}