using System;

namespace VZM.Entities
{
    public class Discount
    {
        public Guid DiscountId { get; set; }
        public float Value { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiredAt { get; set; }
        
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
