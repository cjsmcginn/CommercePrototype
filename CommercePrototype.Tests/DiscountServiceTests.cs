using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommercePrototype.Core;
using CommercePrototype.Store;
using CommercePrototype.Admin;

namespace CommercePrototype.Tests
{
    [TestClass]
    public class DiscountServiceTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
            DataManager.RefreshSession();
        }
        [TestMethod]
        public void SaveDiscountTest()
        {
            var discount = TestHelper.GetTestDiscount();
            DiscountService.StoreDiscount.Discounts.Add(discount);
            DataManager.SaveChanges();
            Assert.IsTrue(DiscountService.StoreDiscount.Discounts.Where(x => x.Name == discount.Name).Count() == 1, "Discount not added");

        }
    }
}
