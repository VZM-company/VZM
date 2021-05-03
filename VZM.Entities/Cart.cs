using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VZM.Entities
{
    public class Cart
    {
        public int CartId { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public int CartStatusId { get; set; }
        public CartStatus CartStatus { get; set; }
    }
}
