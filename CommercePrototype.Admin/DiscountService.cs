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
        public void SaveStoreDiscount(StoreDiscount storeDiscount)
        {
            DataManager.CurrentSession.Store(storeDiscount);
        }

        public static StoreDiscount StoreDiscount
        {
            get
            {
                var result = DataManager.CurrentSession.Query<StoreDiscount>().SingleOrDefault();
                if (result == null)
                {

                    result = new StoreDiscount();
                    DataManager.CurrentSession.Store(result);
                }
                return result;
            }
        }

    }
}
