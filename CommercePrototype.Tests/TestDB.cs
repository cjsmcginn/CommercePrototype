using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommercePrototype.Core;
using CommercePrototype.Security;
using CommercePrototype.Store;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;
using Raven.Tests.Helpers;

namespace CommercePrototype.Tests
{
    public class TestDB : RavenTestBase
    {
        private DocumentStoreBase _Store;

        public DocumentStoreBase Store
        {
            get { return _Store; }
            set { _Store = value; }
        }

        public void CreateDatabase()
        {
            var db = NewDocumentStore();
            db.DefaultDatabase = "Commerce";
            db.Conventions.DefaultQueryingConsistency = ConsistencyOptions.QueryYourWrites;
            db.OpenSession();
            db.RegisterListener(new ValidationStoreListener());
            db.Initialize();
            IndexCreation.CreateIndexes(typeof(CommercePrototype.Security.Accounts_ByUsernameAndPasswordAndEmail).Assembly, db);
            IndexCreation.CreateIndexes(typeof (CommercePrototype.Store.Orders_ByAccount).Assembly, db);
            Store = db;

            InsertProducts();
            InsertAccounts();
            InsertDiscounts();
        }

        public void InsertProducts()
        {
            var productNames =
                SampleData.ProductNames.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
            List<Product> products = new List<Product>();
            productNames.ForEach(x =>
                {
                    var p = TestHelper.GetTestProduct();
                    p.Name = x;
                    products.Add(p);

                });
            using (var session = _Store.OpenSession())
            {
                products.ToList().ForEach(product =>
                    {
                        session.Store(product);
                    });
                session.SaveChanges();
            }
        }

        public void InsertAccounts()
        {
            var usernames = SampleData.Usernames.Split(";".ToArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
            var emails = SampleData.Emails.Split(";".ToArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
            var passwords = SampleData.Passwords.Split(";".ToArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
            List<Account> accounts = new List<Account>();
            var counter = 0;
            while (counter < usernames.Count)
            {
                var account = new Account
                    {
                        Active = true,
                        CreatedOnUtc = System.DateTime.UtcNow,
                        Email = emails[counter],
                        Password = passwords[counter],
                        Username = usernames[counter]
                        
                    };
                account.Roles.Add(Role.Registered);
                accounts.Add(account);
                counter++;

            }
            using (var session = _Store.OpenSession())
            {
                accounts.ToList().ForEach(account =>
                {
                    session.Store(account);
                });
                session.SaveChanges();
            }
        }

        public void InsertDiscounts()
        {
            var discounts = new List<Discount>();

            var shippingDiscount = TestHelper.GetTestDiscount();
            var shippingFixedDiscount = TestHelper.GetTestDiscount();
            var orderTotalDiscount = TestHelper.GetTestDiscount();
            var orderTotalFixedDiscount = TestHelper.GetTestDiscount();
            var orderSubtotalDiscount = TestHelper.GetTestDiscount();
            var orderSubtotalFixedDiscount = TestHelper.GetTestDiscount();
            var orderTotalCodeFixedDiscount = TestHelper.GetTestDiscount();

            discounts.Add(shippingDiscount);
            discounts.Add(shippingFixedDiscount);
            discounts.Add(orderTotalDiscount);
            discounts.Add(orderTotalFixedDiscount);
            discounts.Add(orderSubtotalDiscount);
            discounts.Add(orderSubtotalFixedDiscount);
            discounts.Add(orderTotalCodeFixedDiscount);

            shippingDiscount.Name = "20 Percent Off Shipping";
            shippingDiscount.UsePercentage = true;
            shippingDiscount.Amount = .20m;
            orderTotalDiscount.Name = "20 Percent Off Order Total";
            orderTotalDiscount.UsePercentage = true;
            orderTotalDiscount.Amount = .20m;
            orderSubtotalDiscount.Name = "20 Percent Off Order Subtotal";
            orderSubtotalDiscount.UsePercentage = true;
            orderSubtotalDiscount.Amount = .20m;

            shippingFixedDiscount.Name = "1 dollar off shipping";
            shippingFixedDiscount.Amount = 1.00m;
            orderTotalFixedDiscount.Name = "1 dollar off order total";
            orderTotalFixedDiscount.Amount = 1.00m;
            orderSubtotalFixedDiscount.Name = "1 dollar off order subtotal";
            orderSubtotalFixedDiscount.Amount = 1.00m;

            orderTotalCodeFixedDiscount.Name = "1 dollar off order total with code";
            orderTotalCodeFixedDiscount.Amount = 1.00m;
            orderTotalCodeFixedDiscount.Code = "12345";

            using (var session = _Store.OpenSession())
            {
                discounts.ToList().ForEach(discount =>
                {
                    session.Store(discount);
                });
                session.SaveChanges();
            }



        }
    }
}


