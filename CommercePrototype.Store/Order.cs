using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercePrototype.Store
{
    public enum OrderStatus
    {
        NotProcessed,
        Processed,
        Pending,
        Complete,
        Refunded,
        PartiallyRefunded,
        Canceled
    }
    [Validator(typeof(OrderValidator))]
    public class Order
    {
        public string Id { get; set; }
        public string Account { get; set; }
        public DateTime CreateOnUtc { get; set; }
        public OrderStatus OrderStatus { get; set; }
        List<OrderLineItem> _LineItems;
       
        public List<OrderLineItem> LineItems
        {
            get { return _LineItems ?? (_LineItems = new List<OrderLineItem>()); }
            set { _LineItems = value; }
        }

        public class OrderLineItem
        {
            public string Product { get; set; }
            public string ProductVariantName { get; set; }
            public decimal UnitPrice { get; set; }   
         
        }
    }

    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(order => order.CreateOnUtc).NotEqual(System.DateTime.MinValue);
            RuleFor(order => order.Account)
                .NotEmpty()
                .Matches(@"accounts/[0-9]+");
        }
    }

}
