using System;
using System.Collections.Generic;

namespace VZM.Entities
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Info { get; set; }
        public bool Confirmed { get; set; }
        public string ImageUrl { get; set; }

        public Guid? RoleId { get; set; } = null;
        public Role Role { get; set; }

        public List<Product> Products { get; set; }
    }
}
