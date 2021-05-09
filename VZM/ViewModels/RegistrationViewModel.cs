using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VZM.Entities;

namespace VZM.ViewModels
{
    public class RegistrationViewModel:User
    {
        public new string Role { get; set; }
    }
}
