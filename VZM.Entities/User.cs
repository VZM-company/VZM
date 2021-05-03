using System;
using System.Collections.Generic;

namespace VZM.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Info { get; set; }
        public byte Confirmed { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public List<Product> Products { get; set; }
    }
}
