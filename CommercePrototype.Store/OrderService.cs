using CommercePrototype.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercePrototype.Store
{
    public class OrderService
    {
        const string ORDERS_BY_ACCOUNT_INDEX = "Orders/ByAccount";
        public Order CreateOrderFromCart(ShoppingCart cart)
        {
            return cart.CreateOrderFromShoppingCart();
        }
        public List<Order> GetOrdersByAccountId(string id)
        {
            List<Order> result = null;
            var clause = string.Format("Account:{0}", id);
            result = DataManager.CurrentSession.Advanced.LuceneQuery<Order>(ORDERS_BY_ACCOUNT_INDEX).Where(clause).ToList();
            return result;
        }
        public Order GetOrderById(string id)
        {
            Order result = null;
            result = DataManager.CurrentSession.Load<Order>(id);
            return result;
        }
        public void SaveOrder(Order order)
        {
            DataManager.CurrentSession.Store(order);
        }
    }
}
