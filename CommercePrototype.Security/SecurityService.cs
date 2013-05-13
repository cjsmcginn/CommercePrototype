using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using CommercePrototype.Core;
namespace CommercePrototype.Security
{
    /// <summary>
    /// Encapsulates data operations, callers should use these methods when operation on accounts   
    /// operations may require cleanup tasks etc.. that would be missed if persistance was called directly
    /// </summary>
    public class SecurityService
    {
        const string ACCOUNT_LOOKUP_INDEX_NAME = "Accounts/ByUsernameAndPasswordAndEmail";
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
        public void SaveAccount(Account account)
        {
            var validator = new AccountValidator();
            DataManager.CurrentSession.Store(account);
        }
        public Account GetAccountByUsername(string username)
        {
            Account result = null;
            result = DataManager.CurrentSession.Advanced.LuceneQuery<Account>(ACCOUNT_LOOKUP_INDEX_NAME).SingleOrDefault(x => x.Username == username);
            return result;
        }
        public Account GetAuthenticatedAccount(string username, string password)
        {
            Account result = null;
            result = DataManager.CurrentSession.Advanced.LuceneQuery<Account>(ACCOUNT_LOOKUP_INDEX_NAME).SingleOrDefault(x => x.Username == username && x.Password == password);
            return result;
        }
        public Account GetAccountByEmail(string email)
        {
            Account result = null;
            result = DataManager.CurrentSession.Advanced.LuceneQuery<Account>(ACCOUNT_LOOKUP_INDEX_NAME).SingleOrDefault(x => x.Email == email);
            return result;
        }
        public void DeleteAccount(Account account)
        {
            DataManager.CurrentSession.Delete<Account>(account);
        }
        public Account CreateGuestAccount()
        {
            var result = new Account
            {
                CreatedOnUtc = System.DateTime.UtcNow,
                Username = String.Format("Guest-{0}", Guid.NewGuid()),
                Password = GetRandomString(12),
                Roles = new List<Role> { Role.Guests }
            };
            SaveAccount(result);
            return result;
        }
        public Account CreateAccountByEmail(string email)
        {
            Account result = null;
            result = new Account
            {
                Active = true,
                CreatedOnUtc = System.DateTime.UtcNow,
                Email = email,
                Username = email,
                Password = GetRandomString(9),
                Roles = new List<Role> { Role.Registered }
            };
            SaveAccount(result);
            return result;
        }
    }
}
