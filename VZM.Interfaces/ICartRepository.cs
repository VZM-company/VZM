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
        public IEnumerable<Product> GetItems(Guid userId);
        public void Add(Guid productId, Guid userId);
        public void DeleteProduct(Guid productId, Guid userId);
        public void DeleteAll(Guid userId);
        public void Purchase(Guid userId);
    }
}
