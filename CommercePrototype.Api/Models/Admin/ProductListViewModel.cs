using CommercePrototype.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommercePrototype.Api.Models.Admin
{
    public class ProductListViewModel
    {
        List<ProductListItemViewModel> _Products;

        public List<ProductListItemViewModel> Products
        {
            get { return _Products ?? (_Products = new List<ProductListItemViewModel>()); }
            set { _Products = value; }


        }

        public class ProductListItemViewModel
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public bool Active { get; set; }
            public ProductVariantListViewModel ProductVariants { get; set; }
            public class ProductVariantListViewModel
            {

                List<ProductVariantListItemViewModel> _ProductVariants;

                public List<ProductVariantListItemViewModel> ProductVariants
                {
                    get { return _ProductVariants ?? (_ProductVariants = new List<ProductVariantListItemViewModel>()); }
                    set { _ProductVariants = value; }
                }
                public class ProductVariantListItemViewModel
                {
                    public string Name { get; set; }
                    public bool Active { get; set; }
                    public bool RequiresShipping { get; set; }
                    public decimal Price { get; set; }
                }
            }
        }

        public static ProductListViewModel GetModel(List<Product> products)
        {
            var result = new ProductListViewModel
            {
                Products = products.Select(item => new ProductListItemViewModel
                    {
                        Active = true,
                        Id = item.Id,
                        Name = item.Name,
                        ProductVariants = new ProductListItemViewModel.ProductVariantListViewModel
                        {
                            ProductVariants = item.ProductVariants.Select(pv => new ProductListItemViewModel.ProductVariantListViewModel.ProductVariantListItemViewModel
                            {
                                Active = pv.Active,
                                Name = pv.Name,
                                Price = pv.Price,
                                RequiresShipping = pv.RequiresShipping
                            }).ToList()
                        }


                    }).ToList()
            };
            return result;
        }
    }
}