using CommercePrototype.Core;
using CommercePrototype.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercePrototype.Admin
{
    public class ProductService
    {
        public void SaveProduct(Product product)
        {
            DataManager.CurrentSession.Store(product);
        }
        public Product GetProductById(string id)
        {
            Product result = null;
            result = DataManager.CurrentSession.Load<Product>(id);
            return result;
        }
      
    }
}
