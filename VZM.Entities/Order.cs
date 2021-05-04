using System;

namespace VZM.Entities
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public DateTime Created { get; set; }

        public Guid? CartId { get; set; } = null;
        public Cart Cart { get; set; }

        public Guid? OrderStatusId { get; set; } = null;
        public OrderStatus OrderStatus { get; set; }
    }
}
