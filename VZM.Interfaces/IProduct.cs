using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VZM.Interfaces
{
    public interface IProduct
    {
        public IEnumerable<Product> GetProducts();
        public Product GetProduct(int id);
        public void SaveProduct(Product product);
        public void DeleteProduct(int id);
    }
}
