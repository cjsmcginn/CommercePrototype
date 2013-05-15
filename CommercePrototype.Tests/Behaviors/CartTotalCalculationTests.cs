using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommercePrototype.Store;

namespace CommercePrototype.Tests.Behaviors
{
    [TestClass]
    public class CartTotalCalculationTests
    {
        [TestMethod]
        public void SubTotalTest()
        {
            //var product = TestHelper.GetTestProduct();
            var shoppingCart = new ShoppingCart();
            shoppingCart.LineItems.Add(new ShoppingCart.LineItem
            {
                Quantity = 1,
                UnitPrice = 10.00m
            });
            shoppingCart.LineItems.Add(new ShoppingCart.LineItem
            {
                Quantity = 3,
                UnitPrice = 10.00m
            });
            Assert.IsTrue(shoppingCart.Subtotal == 40.00m);
        }
    }
}
