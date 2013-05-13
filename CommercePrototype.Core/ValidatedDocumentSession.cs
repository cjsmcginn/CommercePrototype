using Raven.Client.Connection;
using Raven.Client.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercePrototype.Core
{
    public class ValidatedDocumentSession:DocumentSession
    {
        public ValidatedDocumentSession(string dbName, DocumentStore documentStore, DocumentSessionListeners listeners, Guid id, IDatabaseCommands databaseCommands)
            : base(dbName, documentStore, listeners, id, databaseCommands)
        {
        }
    }
}
