using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommercePrototype.Core;
using CommercePrototype.Security;

namespace CommercePrototype.Tests
{
    [TestClass]
    public class SecurityServiceTests
    {
        public SecurityServiceTests()
        {
            DataManager.CurrentSession.Advanced.DocumentStore.Conventions.DefaultQueryingConsistency = Raven.Client.Document.ConsistencyOptions.QueryYourWrites;
        }
        #region Utility Methods
        public Account CreateAccount()
        {
            var target = TestHelper.CreateTestAccount();
            target.AddRole(Role.Guests);
            var securityService = new SecurityService();
            securityService.SaveAccount(target);
            DataManager.SaveChanges();
            var actual = DataManager.CurrentSession.Load<Account>(target.Id);
            Assert.IsInstanceOfType(actual, typeof(Account));
            return actual;
        }
        #endregion

        [TestMethod]
        public void CreateAccountTest()
        {
           //Self Asserting
            CreateAccount();
        }

        [TestMethod]
        public void GetAuthenticatedAccountTest()
        {
            var target = CreateAccount();
            var securityService = new SecurityService();
            var actual = securityService.GetAuthenticatedAccount(target.Username, target.Password);
            Assert.IsInstanceOfType(actual, typeof(Account));
        }

        [TestMethod]
        public void CreateGuestAccountTest()
        {
            var securityService = new SecurityService();
            var target = securityService.CreateGuestAccount();
            DataManager.SaveChanges();
   
            var actual = securityService.GetAuthenticatedAccount(target.Username, target.Password);
            Assert.IsTrue(actual.Username == target.Username);
            Assert.IsTrue(actual.Roles.Single() == Role.Guests);
        }

        [TestMethod]
        public void DeleteAccountTest()
        {
            var target = CreateAccount();
            var securityService = new SecurityService();
            securityService.DeleteAccount(target);
            DataManager.SaveChanges();
        
            var actual = securityService.GetAuthenticatedAccount(target.Username, target.Password);
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void GetAccountByEmailTest()
        {
            var target = CreateAccount();
            var securityService = new SecurityService();
            DataManager.SaveChanges();
    
            var actual = securityService.GetAccountByEmail(target.Email);
            Assert.IsInstanceOfType(actual, typeof(Account));
        }

        [TestMethod]
        public void GetAccountByUsernameTest()
        {
            var target = CreateAccount();
            var securityService = new SecurityService();
            DataManager.SaveChanges();
     
            var actual = securityService.GetAccountByUsername(target.Username);
            Assert.IsInstanceOfType(actual, typeof(Account));
        }
    }
}
