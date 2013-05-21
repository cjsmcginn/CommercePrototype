using AutoMapper;
using CommercePrototype.Api.Models.Admin;
using CommercePrototype.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommercePrototype.Api.Mapping
{
    public class MappingExtensions
    {
        public void Execute()
        {
            Mapper.CreateMap<ProductListViewModel.ProductListItemViewModel, Product>();  
                
        }
    }
}