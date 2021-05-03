using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VZM.Entities
{
    public class Discount
    {
        public int DiscountId { get; set; }
        public float Value { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiredAt { get; set; }
        
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
