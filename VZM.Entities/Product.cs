using System;
using System.Collections.Generic;

namespace VZM.Entities
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public string MetaTitle { get; set; }
        public double Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; }
        public string DescriptionShort { get; set; }
        public string ImageUrl { get; set; }

        public Guid? SellerId { get; set; } = null;
        public User Seller { get; set; }

        public List<User> Users { get; set; }
    }
}
