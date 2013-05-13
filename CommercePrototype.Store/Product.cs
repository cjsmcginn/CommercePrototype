using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercePrototype.Store
{
    public class Product
    {
        public string Id { get; set; }        
        public string Name { get; set; }
        List<ProductVariant> _ProductVariants;
        public DateTime CreatedOnUtc { get; set; }
        public bool IsBundle { get; set; }       
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public List<ProductVariant> ProductVariants
        {
            get { return _ProductVariants ?? (_ProductVariants = new List<ProductVariant>()); }
            set { _ProductVariants = value; }
        }
        List<string> _CategoryNames;

        public List<string> CategoryNames
        {
            get { return _CategoryNames ?? (_CategoryNames = new List<string>()); }
            set { _CategoryNames = value; }
        }
       
        public class ProductVariant
        {            
            public string Name { get; set; }
            public DateTime CreatedOnUtc { get; set; }
            public bool Active { get; set; }
            public bool RequiresShipping { get; set; }
            public decimal Price { get; set; }
            public string AffiliateAccount { get; set; }
        }
    }
}
