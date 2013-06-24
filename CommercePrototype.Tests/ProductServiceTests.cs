using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommercePrototype.Store;
using FluentValidation;
using CommercePrototype.Admin;
using CommercePrototype.Core;
using Raven.Client;

namespace CommercePrototype.Tests
{
    [TestClass]
    public class ProductServiceTests
    {
        private static IDocumentStore _Store;
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var testDb = new TestDB();
            testDb.CreateDatabase();
            _Store = testDb.Store;
            testDb.InsertProducts();
            DataManager.CurrentSession = _Store.OpenSession();
        }

        [TestInitialize]
        public void TestInitialize()
        {
           
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
        [TestMethod]
        public void SearchProductsTest()
        {
            var service = new ProductService();
            var actual = service.SearchProducts(0, 15);
            Assert.IsInstanceOfType(actual, typeof(List<Product>));
        }
      
    }
}
