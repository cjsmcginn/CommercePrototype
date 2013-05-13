using Raven.Client.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercePrototype.Store
{
    public class ShoppingCart_ByAccount:AbstractIndexCreationTask<ShoppingCart>
    {
        public ShoppingCart_ByAccount()
        {
            Map = shoppingCarts => from shoppingCart in shoppingCarts
                                   select new { shoppingCart.Account };
        }
    }
}
