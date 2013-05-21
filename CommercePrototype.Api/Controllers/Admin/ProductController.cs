using AutoMapper;
using CommercePrototype.Admin;
using CommercePrototype.Api.Models.Admin;
using CommercePrototype.Core;
using CommercePrototype.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CommercePrototype.Api.Controllers.Admin
{
    public class ProductController : ApiController
    {
        private readonly ProductService _ProductService;

        public ProductController()
        {
            _ProductService = new ProductService();
        }
        // GET api/product
        public ProductListViewModel Get()
        {
            DataManager.RefreshSession();
            ProductListViewModel result = null;
            var products = _ProductService.SearchProducts(0,15);
            result = ProductListViewModel.GetModel(products);
            return result;
        }

        // GET api/product/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/product
        public void Post(ProductListViewModel.ProductListItemViewModel value)
        {
            Mapper.CreateMap<ProductListViewModel.ProductListItemViewModel, Product>()
                .ForMember(dest => dest.Id, p => p.Ignore())
                .ForMember(dest => dest.CreatedOnUtc, p => p.UseValue(System.DateTime.UtcNow));
            var product = Mapper.Map<Product>(value);
            _ProductService.SaveProduct(product);
            DataManager.SaveChanges();
          
        }

        // PUT api/product/5
        public void Put(int id, [FromBody]string value)
        {
            var x = "Y";
        }

        // DELETE api/product/5
        public void Delete(int id)
        {
        }
    }
}
