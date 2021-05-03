using System;

namespace VZM.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime Created { get; set; }

        public int CartId { get; set; }
        public Cart Cart { get; set; }

        public int OrderStatusId { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
