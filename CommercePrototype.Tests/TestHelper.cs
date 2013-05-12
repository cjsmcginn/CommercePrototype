using CommercePrototype.Security;
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

        public static Account CreateTestAccount()
        {
            var result = new Account
            {
                Active = true,
                CreatedOnUtc = System.DateTime.UtcNow,
                Email = GetRandomEmail(),
                Username = GetRandomString(8),
                Password = GetRandomString(10)
            };
            return result;
        }
    }



}
