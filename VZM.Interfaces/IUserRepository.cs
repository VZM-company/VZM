using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VZM.Entities;

namespace VZM.Interfaces
{
    public interface IUserRepository
    {
        public IEnumerable<User> GetProducts();
        public User GetProduct(Guid id);
        public void SaveProduct(User user);
        public void DeleteProduct(Guid id);
    }
}
