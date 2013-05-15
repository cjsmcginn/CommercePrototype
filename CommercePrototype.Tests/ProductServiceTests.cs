using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommercePrototype.Store;
using FluentValidation;
using CommercePrototype.Admin;
using CommercePrototype.Core;
namespace CommercePrototype.Tests
{
    [TestClass]
    public class ProductServiceTests
    {

        [TestInitialize]
        public void TestInitialize()
        {
            DataManager.RefreshSession();
        }
        [TestMethod]
        public void SaveProductTest()
        {
            var service = new ProductService();
            var target = TestHelper.GetTestProduct();
            service.SaveProduct(target);
            DataManager.SaveChanges();
            var actual = service.GetProductById(target.Id);
            Assert.IsInstanceOfType(actual,typeof(Product));

        }
      
    }
}
