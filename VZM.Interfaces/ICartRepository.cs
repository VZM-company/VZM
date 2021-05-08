using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VZM.Entities;

namespace VZM.Interfaces
{
    public interface ICartRepository
    {
        public IEnumerable<Product> GetCurrentCart();
        public void Add(Product product);
        public void Delete(Product product);
        public void Purchase();
    }
}
