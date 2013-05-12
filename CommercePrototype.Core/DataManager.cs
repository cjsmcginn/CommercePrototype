﻿using Raven.Client;
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
        //static DocumentStore _documentStore;
        static IDocumentSession _documentSession;

    
        static void Initialize()
        {
            var _documentStore =  new DocumentStore{ Url = "http://localhost:8892", DefaultDatabase = "Commerce" };
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
