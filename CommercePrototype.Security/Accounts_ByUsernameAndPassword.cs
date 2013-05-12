using Raven.Client.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercePrototype.Security
{
    public class Accounts_ByUsernameAndPasswordAndEmail:AbstractIndexCreationTask<Account>
    {
        public Accounts_ByUsernameAndPasswordAndEmail()
        {
            Map = accounts => from account in accounts
                              select new { account.Username, account.Password, account.Email };
        }
    }
}
