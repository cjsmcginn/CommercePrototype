using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercePrototype.Store
{
    [Validator(typeof(ShoppingCartValidator))]
    public class ShoppingCart
    {
        public string Id { get; set; }
        public string Account { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        List<LineItem> _LineItems;
        public decimal Subtotal
        {
            get
            {
                return LineItems.Sum(x => x.UnitPrice * x.Quantity);
            }
        }
        public List<LineItem> LineItems
        {
            get { return _LineItems ?? (_LineItems = new List<LineItem>()); }
            set { _LineItems = value; }
        }

        public class LineItem
        {
            public string ProductId { get; set; }
            public string ProductName { get; set; }
            public string ProductVariantName { get; set; }
            public decimal UnitPrice { get; set; }
            public int Quantity { get; set; }
        }
    }

    public class ShoppingCartValidator : AbstractValidator<ShoppingCart>
    {
        public ShoppingCartValidator()
        {
            RuleFor(sc => sc.Account)
                .NotEmpty()
                .Matches(@"accounts/[0-9]+");
            RuleFor(sc => sc.CreatedOnUtc)
                .NotEqual(System.DateTime.MinValue);
        }
    }
}
