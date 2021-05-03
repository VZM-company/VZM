using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VZM.Entities;

namespace VZM.Interfaces
{
    public interface IRoleRepository
    {
        public Role GetRole(User user);
        public void SaveRole(User user, Role role);
    }
}
