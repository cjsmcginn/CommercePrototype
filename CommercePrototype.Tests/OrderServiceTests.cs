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
    public class OrderServiceTests
    {
        #region UtilityMethods
        Order CreateAccountOrder(Account account)
        {
            DataManager.CurrentSession.Store(account);
            var cart = new ShoppingCart { Account = account.Id, CreatedOnUtc = System.DateTime.UtcNow };
            var product = TestHelper.GetTestProduct();
            var product2 = TestHelper.GetTestProduct();
            var product3 = TestHelper.GetTestProduct();
            cart.AddShoppingCartLineItem(product, product.ProductVariants.First().Name);
            cart.AddShoppingCartLineItem(product2, product2.ProductVariants.First().Name);
            cart.AddShoppingCartLineItem(product3, product3.ProductVariants.First().Name);

            var target = cart.CreateOrderFromShoppingCart();
          
            Assert.IsTrue(target.LineItems.Count == cart.LineItems.Count, "Item count does not match cart");
            return target;
        }
        #endregion
        [TestMethod]
        public void CreateOrderFromCartTest()
        {
            //Self asserting
            var account = TestHelper.GetTestAccount();
            var order1 = CreateAccountOrder(account);
           
        }
        [TestMethod]
        public void GetOrdersForAccountTest()
        {
            var account = TestHelper.GetTestAccount();
            var order1 = CreateAccountOrder(account);
            var order2 = CreateAccountOrder(account);
            var order3 = CreateAccountOrder(account);
            DataManager.CurrentSession.Store(order1);
            DataManager.CurrentSession.Store(order2);
            DataManager.CurrentSession.Store(order3);
            DataManager.SaveChanges();
            var service = new OrderService();
            var actual = service.GetOrdersByAccountId(account.Id);
            Assert.IsTrue(actual.Count == 3, "Count does not match");
        }
        [TestMethod]
        public void GetOrdersByIdTest()
        {
            var account = TestHelper.GetTestAccount();
            var order1 = CreateAccountOrder(account);            
            DataManager.CurrentSession.Store(order1);
            DataManager.SaveChanges();
            var service = new OrderService();
            var actual = service.GetOrderById(order1.Id);
            Assert.IsInstanceOfType(actual, typeof(Order), "Could not retreive order by id");
        }      
        [TestMethod]
        public void SaveOrder()
        {
            var account = TestHelper.GetTestAccount();
            var order = CreateAccountOrder(account);
            var service = new OrderService();
            service.SaveOrder(order);
            DataManager.SaveChanges();
            var actual = DataManager.CurrentSession.Load<Order>(order.Id);
            Assert.IsInstanceOfType(actual, typeof(Order), "Order not saved");
        }
    }
}
