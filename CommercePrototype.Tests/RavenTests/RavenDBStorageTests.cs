using System;
using System.Collections.Generic;
using System.Linq;
using CommercePrototype.Core;
using CommercePrototype.Security;
using CommercePrototype.Store;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raven.Client;
using Raven.Tests.Helpers;

namespace CommercePrototype.Tests.RavenTests
{
    [TestClass]
    public class RavenDBStorageTests
    {
        private static IDocumentStore _Store;
        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            var testDb = new TestDB();
            testDb.CreateDatabase();
            _Store = testDb.Store;
            DataManager.CurrentSession = _Store.OpenSession();
    
        }
        [TestMethod]
        public void InitDB()
        {
          
            // using(var session  = _Store.OpenSession
        }
        [TestMethod]
        public void AssertProductState()
        {
            var products = DataManager.CurrentSession.Query<Product>();
            Assert.IsTrue(products.ToList().Count() > 0,"Products not inserted");
            products.ToList().ForEach(product =>
                {
                    Assert.IsTrue(product.ProductVariants.Count() > 0,"Product does not contain product variants");
                    Assert.IsTrue(product.ProductVariants.Sum(x => x.Price) > 0,"Sum of product variants is 0");

                });
        }

        [TestMethod]
        public void AssertUserState()
        {
            var accounts = DataManager.CurrentSession.Query<Account>();
            Assert.IsTrue(accounts.ToList().Count > 0, "Accounts not inserted");
            accounts.ToList().ForEach(account =>
                {
                    Assert.IsTrue(account.Roles.Count > 0, "Roles not added");
                });
        }

        [TestMethod]
        public void AssertDiscountState()
        {
            var discounts = DataManager.CurrentSession.Query<Discount>();
            Assert.IsTrue(discounts.ToList().Count > 0, "Discounts not inserted");
        }
    }
}
