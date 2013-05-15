using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommercePrototype.Core;
using CommercePrototype.Security;
using System.Collections.Generic;
using CommercePrototype.Store;
using FluentValidation;

namespace CommercePrototype.Tests
{
    [TestClass]
    public class ShoppingCartServiceTests
    {

        [TestInitialize]
        public void TestInitialize()
        {
            DataManager.RefreshSession();
        }

        #region Utility Methods
     
        #endregion

        [TestMethod]
        public void GetShoppingCartTest()
        {
            var account = TestHelper.GetTestAccount();
            DataManager.CurrentSession.Store(account);
            var shoppingCartService = new ShoppingCartService();
            var cart = shoppingCartService.GetShoppingCart(account);
            shoppingCartService.SaveShoppingCart(cart);            
            DataManager.SaveChanges();
            var actual = shoppingCartService.GetShoppingCart(account);
            Assert.IsTrue(actual.Account == account.Id, "ShoppingCart contains invalid account");
            
        }
        [TestMethod]
        public void SaveShoppingCartTest()
        {
            var account = TestHelper.GetTestAccount();
            DataManager.CurrentSession.Store(account);
            var shoppingCartService = new ShoppingCartService();
            var cart = shoppingCartService.GetShoppingCart(account);
            shoppingCartService.SaveShoppingCart(cart);
            DataManager.SaveChanges();
            var actual = shoppingCartService.GetShoppingCart(account);
            Assert.IsTrue(actual.Account == account.Id, "ShoppingCart contains invalid account");
        }
        [TestMethod]
        public void AddShoppingCartLineItemTest()
        {
            var product = TestHelper.GetTestProduct();
            var account = TestHelper.GetTestAccount();
            var service = new ShoppingCartService();
            var cart = service.GetShoppingCart(account);
            service.AddShoppingCartLineItem(cart, product, product.ProductVariants.First().Name);
            Assert.IsTrue(cart.LineItems.Count == 1, "Product not added");
            Assert.IsTrue(cart.LineItems.First().Quantity == 1, "Quantity is not equal to 1");
            Assert.IsTrue(cart.LineItems.First().ProductName == product.Name, "Product name does not match");
            Assert.IsTrue(cart.LineItems.First().ProductVariantName == product.ProductVariants.First().Name, "Product Variant name does not match");
            Assert.IsTrue(cart.LineItems.First().Price == product.ProductVariants.First().Price, "Product variant price does not match");
        }
        [TestMethod]
        public void RemoveShoppingCartLineItemTest()
        {
            var product = TestHelper.GetTestProduct();
            var account = TestHelper.GetTestAccount();
            var service = new ShoppingCartService();
            var cart = service.GetShoppingCart(account);
            service.AddShoppingCartLineItem(cart, product, product.ProductVariants.First().Name);   
            service.RemoveShoppingCartLineItem(cart, cart.LineItems.First());
            Assert.IsTrue(cart.LineItems.Count == 0);
        }
        [TestMethod]
        public void ClearShoppingCartLineItemsTest()
        {
            var product = TestHelper.GetTestProduct();
            var product2 = TestHelper.GetTestProduct();
            var account = TestHelper.GetTestAccount();
            var service = new ShoppingCartService();
            var cart = service.GetShoppingCart(account);
            service.AddShoppingCartLineItem(cart, product, product.ProductVariants.First().Name);
            service.AddShoppingCartLineItem(cart, product2, product2.ProductVariants.First().Name);
            service.ClearShoppingCartLineItems(cart);
            Assert.IsTrue(cart.LineItems.Count == 0, "Shopping cart items not removed");
        }

        #region behaviors
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SaveShoppingCartTest_WhenInvalid()
        {
            var account = TestHelper.GetTestAccount();
            var shoppingCartService = new ShoppingCartService();
            var cart = shoppingCartService.GetShoppingCart(account);
            cart.Account = "";
            shoppingCartService.SaveShoppingCart(cart);           
            DataManager.SaveChanges();
            
           
        }
        #endregion
    }
}
