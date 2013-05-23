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
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using WebApiContrib.Formatting.Jsonp;

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
        public HttpResponseMessage Put(ProductListViewModel.ProductListItemViewModel value)
        {
            Mapper.CreateMap<ProductListViewModel.ProductListItemViewModel, Product>();
            Mapper.CreateMap<ProductListViewModel.ProductListItemViewModel.ProductVariantListItemViewModel, Product.ProductVariant>();
            var item = _ProductService.GetProductById(value.Id);
            var product = Mapper.Map(value, item);
            _ProductService.SaveProduct(item);
            DataManager.SaveChanges();
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            IContentNegotiator negotiator = Configuration.Services.GetContentNegotiator();
            MediaTypeHeaderValue mediaType;
            var contentNegotiationResult = negotiator.Negotiate(typeof(ProductListViewModel.ProductListItemViewModel), Request, Configuration.Formatters);  
            result.Content = new System.Net.Http.ObjectContent<ProductListViewModel.ProductListItemViewModel>(value,contentNegotiationResult.Formatter);

            return result;
        }

        // DELETE api/product/5
        public void Delete(int id)
        {
        }
    }
}
