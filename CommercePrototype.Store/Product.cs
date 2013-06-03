using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercePrototype.Store
{
    [Validator(typeof(ProductValidator))]
    public class Product
    {
        public string Id { get; set; }        
        public string Name { get; set; }
        List<ProductVariant> _ProductVariants;
        public DateTime CreatedOnUtc { get; set; }          
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        List<ProductAttribute> _ProductAttributes;

        public List<ProductAttribute> ProductAttributes
        {
            get { return _ProductAttributes ?? (_ProductAttributes = new List<ProductAttribute>()); }
            set { _ProductAttributes = value; }
        }

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
       
        [Validator(typeof(ProductVariantValidator))]
        public class ProductVariant
        {            
            public string Name { get; set; }
            public DateTime CreatedOnUtc { get; set; }
            public bool Active { get; set; }
            public bool RequiresShipping { get; set; }
            public decimal Price { get; set; }
            List<ProductVariantAttribute> _ProductVariantAttributes;

            public List<ProductVariantAttribute> ProductVariantAttributes
            {
                get { return _ProductVariantAttributes ?? (new List<ProductVariantAttribute>()); }
                set { _ProductVariantAttributes = value; }
            }

            public class ProductVariantAttribute
            {
                public ProductVariantAttributeType AttributeType { get; set; }
                public string AttributeName { get; set; }
                public string AttributeValue { get; set; }
            }
        }
        public class ProductVariantValidator : AbstractValidator<ProductVariant>
        {
            public ProductVariantValidator()
            {
                RuleFor(pv => pv.Name)
                    .NotEmpty();
                RuleFor(pv => pv.CreatedOnUtc)
                    .NotEqual(System.DateTime.MinValue);
            }
        }
        public class ProductAttribute
        {
            public ProductAttributeType AttributeType { get; set; }
            public string AttributeName { get; set; }
            public string AttributeValue { get; set; }
        }
    }

    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator() {
            RuleFor(product => product.Name)
                .NotEmpty();
            RuleFor(product => product.CreatedOnUtc)
                .NotEqual(System.DateTime.MinValue);
            RuleFor(p => p.ProductVariants).SetValidator(new Product.ProductVariantValidator());
            RuleFor(product=>product.ProductAttributes).Must((product,productAttributes)=>{
                var valid = productAttributes.Count(pa => pa.AttributeType == ProductAttributeType.DefaultPrice) == 1
                    && productAttributes.Count(pa => pa.AttributeType == ProductAttributeType.ProductType) == 1;
                return valid;
                   
            });
           
         

        }
    }
}
