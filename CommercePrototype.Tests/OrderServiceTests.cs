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

        [TestMethod]
        public void CreateOrderFromCart(ShoppingCart cart)
        {
            Assert.Fail("Not Implemented");
        }
        [TestMethod]
        public void GetOrdersForAccountTest()
        {
            Assert.Fail("Not Implemented");
        }
        [TestMethod]
        public void GetLastOrderForAccountTest()
        {
            Assert.Fail("Not Implemented");
        }
        [TestMethod]
        public void SaveOrder(Order order)
        {
            Assert.Fail("Not Implemented");
        }
    }
}
