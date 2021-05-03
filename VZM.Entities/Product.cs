using System;

namespace VZM.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string MetaTitle { get; set; }
        public float Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; }
        public string DescriptionShort { get; set; }

        public int CartId { get; set; }
        public Cart Cart { get; set; }
    }
}
