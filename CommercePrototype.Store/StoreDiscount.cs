using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercePrototype.Store
{
    public enum DiscountType
    {
        None,
        OrderSubtotal,
        OrderTotal,
        OrderShipping,
        OrderItem
        
    }
    

        [Validator(typeof(DiscountValidator))]
        public class Discount
        {
            public string Name { get; set; }
            public DiscountType DiscountType { get; set; }
            public bool Active { get; set; }
            public DateTime CreatedOnUtc { get; set; }
            public bool UsePercentage { get; set; }
            public decimal Amount { get; set; }
            public string Code { get; set; }
        }
        public class DiscountValidator : AbstractValidator<Discount>
        {
            public DiscountValidator()
            {
                RuleFor(discount => discount.Name)
                    .NotEmpty();
                RuleFor(discount => discount.CreatedOnUtc)
                    .NotEqual(System.DateTime.MinValue);
                RuleFor(discount => discount.Amount)
                    .NotEqual(0);
                RuleFor(discount => discount.Amount)
                    .LessThanOrEqualTo(100)
                    .When(discount => discount.UsePercentage);
                RuleFor(discount => discount.DiscountType)
                    .NotEqual(DiscountType.None);

            }
        }
        
    }

