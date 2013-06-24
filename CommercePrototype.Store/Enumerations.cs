using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercePrototype.Store
{
    /// <summary>
    /// Any information that changes can be stored, types are defined on Products based on this enumeration
    /// </summary>
    public enum ProductAttributeType
    {
        Published,
        DefaultPrice,
        Subscription,
        ProductType
    }
    /// <summary>
    /// Any information that changes can be stored, types are defined on ProductVariants based on this enumeration
    /// </summary>
    public enum ProductVariantAttributeType
    {
        Published,
        RecurringBillingPeriod,
        Color,
        Size,
        Manufacturer,
        Affiliate,
        ShippingAdjustment,
        DomesticShippingAdjustment,
        InternationalShippingAdjustment,
        AffiliateShippingAdjustment

    }
    

}
