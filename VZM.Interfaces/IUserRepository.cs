﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VZM.Entities;

namespace VZM.Interfaces
{
    public interface IUserRepository
    {
        public IEnumerable<User> GetUsers();
        public User GetUser(Guid id);
        public void SaveUser(User user);
        public void DeleteUser(Guid id);
    }
}
