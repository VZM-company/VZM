using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VZM.Interfaces
{
    public interface IRole
    {
        public Role GetRole(User user);
        public void SaveRole(User user, Role role);
    }
}
