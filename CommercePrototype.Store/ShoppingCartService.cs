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
        //TODO:Possibly move save changes into methods in order to assure validation
        public void SaveShoppingCart(ShoppingCart shoppingCart)
        {
            var validator = new ShoppingCartValidator();
            validator.ValidateAndThrow(shoppingCart);

            DataManager.CurrentSession.Store(shoppingCart);
        }
    }
}
