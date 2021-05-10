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
        public IEnumerable<Product> GetProductsByName(string name);
        public IEnumerable<Product> GetProductsByPrice(double startPrice, double endPrice);
        public IEnumerable<Product> GetProductsByCategory(Category category);
        public IEnumerable<Product> GetProductsByUser(User user);
        public IEnumerable<Product> GetProductsBySeller(User user);
        public IEnumerable<Product> GetTopProducts(int num);
    }
}
