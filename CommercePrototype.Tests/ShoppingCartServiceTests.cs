using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommercePrototype.Core;
using CommercePrototype.Security;
using System.Collections.Generic;
using CommercePrototype.Store;

namespace CommercePrototype.Tests
{
    [TestClass]
    public class ShoppingCartServiceTests
    {

        #region Utility Methods
        Account GetAccount()
        {
            var target = TestHelper.CreateTestAccount();
            target.Roles = new List<Role> { Role.Registered };
            var securityService = new SecurityService();
            securityService.SaveAccount(target);
            DataManager.SaveChanges();
            return target;
        }
        #endregion

        [TestMethod]
        public void GetShoppingCartTest()
        {
            var account = GetAccount();
            var shoppingCartService = new ShoppingCartService();
            var cart = shoppingCartService.GetShoppingCart(account);
            shoppingCartService.SaveShoppingCart(cart);            
            DataManager.SaveChanges();
            var actual = shoppingCartService.GetShoppingCart(account);
            Assert.IsTrue(actual.Account == account.Id, "ShoppingCart contains invalid account");
            
        }

        [TestMethod]
        public void ClearShoppingCartTest()
        {
            Assert.Fail("Not Implemented");
        }
        [TestMethod]
        public void AddShoppingCartLineItemTest()
        {
            Assert.Fail("Not Implemented");
        }
        [TestMethod]
        public void RemoveShoppingCartLineItemTest()
        {
            Assert.Fail("Not Implemented");
        }

    }
}
