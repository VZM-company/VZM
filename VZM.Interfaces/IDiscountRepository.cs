using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VZM.Entities;

namespace VZM.Interfaces
{
    public interface IDiscountRepository
    {
        public Discount GetDiscount(Guid productId);
        public void SaveDiscount(Discount discount);
        public void DeleteDiscount(Guid discountId);
    }
}
