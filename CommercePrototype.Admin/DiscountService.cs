using CommercePrototype.Core;
using CommercePrototype.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercePrototype.Admin
{
    public class DiscountService
    {
        public void SaveStoreDiscount(Discount discount)
        {
            DataManager.CurrentSession.Store(discount);
        }
    }
}
