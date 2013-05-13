using CommercePrototype.Security;
using CommercePrototype.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercePrototype.Tests
{
    public class TestHelper
    {
        private static Random random = new Random((int)DateTime.Now.Ticks);

        public static string GetRandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }
        public static string GetRandomEmail()
        {
            return String.Format("{0}@donotresolve.com", GetRandomString(8));
        }
        public static decimal GetRandomPrice()
        {
            return decimal.Parse(String.Format("{0}.{1}", random.Next(1, 50).ToString(), random.Next(0, 99)));
        }
        public static Account GetTestAccount()
        {
            var result = new Account
            {
                Active = true,
                CreatedOnUtc = System.DateTime.UtcNow,
                Email = GetRandomEmail(),
                Username = GetRandomString(8),
                Password = GetRandomString(10),
                Roles = new List<Role> { Role.Registered }
            };
            return result;
        }
        public static Product GetTestProduct()
        {
            var target = new Product
            {
                Name = GetRandomString(8),
                CategoryNames = new List<string> { GetRandomString(10) },
                FullDescription = GetRandomString(100),
                CreatedOnUtc = System.DateTime.UtcNow,
                ProductVariants = new List<Product.ProductVariant> {
                    new Product.ProductVariant
                    {
                         Active=true,
                         CreatedOnUtc=System.DateTime.UtcNow,
                         Name=GetRandomString(8),
                         Price=GetRandomPrice()
                           
                    },
                    new Product.ProductVariant
                    {
                        Active=true,
                        CreatedOnUtc=System.DateTime.UtcNow,
                        Name=GetRandomString(8),
                        Price=GetRandomPrice()
                    }

                },
                ShortDescription = GetRandomString(20)
            };
            return target;
        }

    }



}
