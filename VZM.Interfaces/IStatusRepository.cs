using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VZM.Entities;

namespace VZM.Interfaces
{
    public interface IStatusRepository
    {
        public CartStatus GetCartStatus(ICartRepository cart);
    }
}
