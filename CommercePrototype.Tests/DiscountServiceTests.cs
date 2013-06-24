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
       
    }
}
