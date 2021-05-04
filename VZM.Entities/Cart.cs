using System;

namespace VZM.Entities
{
    public class Cart
    {
        public Guid CartId { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid? CartStatusId { get; set; } = null;
        public CartStatus CartStatus { get; set; }
    }
}
