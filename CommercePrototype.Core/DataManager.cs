using Raven.Client;
using Raven.Client.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercePrototype.Core
{
    public class DataManager
    {
  
        static IDocumentSession _documentSession;

        /// <summary>
        /// Mainly for purposes of testing, clearing out static session for each test
        /// </summary>
        public static void RefreshSession()
        {
            _documentSession = null;
        }
    
        static void Initialize()
        {
            var _documentStore =  new DocumentStore{ Url = "http://localhost:8892", DefaultDatabase = "Commerce" };
            _documentStore.RegisterListener(new ValidationStoreListener());
            _documentStore.Initialize();
          
            _documentSession = _documentStore.OpenSession();
        }

        public static IDocumentSession CurrentSession
        {
            get {
                if(_documentSession==null)
                    Initialize();
                return DataManager._documentSession; 
            }
           
        }
        //allows a single place to handle exceptions before forwarding to caller
        public static void SaveChanges()
        {
            _documentSession.SaveChanges();
        }

    }
}
