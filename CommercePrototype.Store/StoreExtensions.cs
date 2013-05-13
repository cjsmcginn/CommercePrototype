using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercePrototype.Store
{
    /// <summary>
    /// These extensions facilitate testing as many of the operations of a service call these directly,
    /// tests can better isolate there scope.
    /// </summary>
    public static class StoreExtensions
    {
        public static void AddShoppingCartLineItem(this ShoppingCart cart, Product product, string productVariantName)
        {
            var productVariant = product.ProductVariants.Single(x => x.Name == productVariantName);
            var existing = cart.LineItems.SingleOrDefault(x => x.ProductVariantName == productVariant.Name);
            if (existing != null)
            {
                existing.Quantity++;
                return;
            }

            var lineItem = new ShoppingCart.LineItem
            {
                Price = productVariant.Price,
                ProductId = product.Id,
                ProductName = product.Name,
                ProductVariantName = productVariant.Name,
                Quantity = 1
            };
            cart.LineItems.Add(lineItem);
        }
        public static Order CreateOrderFromShoppingCart(this ShoppingCart cart)
        {
            Order result = new Order
            {
                Account = cart.Account,
                CreateOnUtc = System.DateTime.UtcNow,
                LineItems = cart.LineItems.Select(lineItem => new Order.OrderLineItem
                {
                    Product = lineItem.ProductName,
                    ProductVariantName = lineItem.ProductVariantName,
                    UnitPrice = lineItem.Price
                }).ToList(),
                OrderStatus = OrderStatus.NotProcessed
            };
            return result;
        }
    }
}
