using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VZM.Entities;

namespace VZM.Interfaces
{
    public interface IProductRepository
    {
        public IEnumerable<Product> GetProducts();
        public Product GetProduct(Guid id);
        public void SaveProduct(Product product);
        public void DeleteProduct(Guid id);
    }
}
