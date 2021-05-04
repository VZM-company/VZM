using System;

namespace VZM.Entities
{
    public class Cart
    {
        public Guid CartId { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public Guid CartStatusId { get; set; }
        public CartStatus CartStatus { get; set; }
    }
}
