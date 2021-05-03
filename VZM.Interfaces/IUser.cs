using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VZM.Interfaces
{
    public interface IUser
    {
        public IEnumerable<User> GetProducts();
        public User GetProduct(int id);
        public void SaveProduct(User user);
        public void DeleteProduct(int id);
    }
}
