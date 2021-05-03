using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VZM.Entities;

namespace VZM.Interfaces
{
    public interface ICart
    {
        public IEnumerable<Cart> GetCarts();
        public Cart GetCart(int id);
        public void DeleteCart(int id);
        public void SaveCart(Cart cart);

        public IEnumerable<Product> GetCartItems();
        public void DeleteItem(Product product);
        public void AddItem(Product product);
        public void ClearCart();
    }
}
