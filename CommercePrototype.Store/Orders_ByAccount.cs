using Raven.Client.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercePrototype.Store
{
    public class Orders_ByAccount:AbstractIndexCreationTask<Order>
    {
        public Orders_ByAccount()
        {
            Map = orders => from order in orders
                            select new { order.Account };
        }

    }
}
