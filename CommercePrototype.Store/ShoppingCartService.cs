using CommercePrototype.Core;
using CommercePrototype.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
namespace CommercePrototype.Store
{
    /// <summary>
    /// Encapsulates data operations, callers should use these methods when performing operations on a ShoppingCart   
    /// operations may require cleanup tasks etc.. that would be missed if persistance was called directly
    /// </summary>
    public class ShoppingCartService
    {
        const string SHOPPING_CART_BY_ACCOUNT_INDEX = "ShoppingCart/ByAccount";
       
        public ShoppingCart GetShoppingCart(Account account)
        {
            ShoppingCart result = null;
            result = DataManager.CurrentSession.Advanced.LuceneQuery<ShoppingCart>(SHOPPING_CART_BY_ACCOUNT_INDEX).SingleOrDefault(x => x.Account == account.Id);
            if (result == null)
            {
                result = new ShoppingCart
                {
                    Account = account.Id,
                    CreatedOnUtc =System.DateTime.UtcNow
                };
                
            }
            return result;
        }
        public void AddShoppingCartLineItem(ShoppingCart cart, Product product, string productVariantName)
        {
            cart.AddShoppingCartLineItem(product, productVariantName);
        }
        public void RemoveShoppingCartLineItem(ShoppingCart cart,ShoppingCart.LineItem item)
        {
            item.Quantity--;
            if (item.Quantity == 0)
                cart.LineItems.Remove(item);
        }
        /// <summary>
        /// We set the cart to a new cart because removing all items will affect tax rates shipping etc...
        /// </summary>
        /// <param name="cart"></param>
        public void ClearShoppingCartLineItems(ShoppingCart cart)
        {
            cart.LineItems.Clear();
            //reset create date this is essentially a new cart
            cart.CreatedOnUtc = System.DateTime.Now;
        }
        public void SaveShoppingCart(ShoppingCart shoppingCart)
        {
            //var validator = new ShoppingCartValidator();  
            DataManager.CurrentSession.Store(shoppingCart);
        }
    }
}
