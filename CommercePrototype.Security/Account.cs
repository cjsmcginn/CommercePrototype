using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercePrototype.Security
{
    [Validator(typeof(AccountValidator))]
    public class Account
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public bool Active { get; set;}
        List<Role> _Roles;

        public List<Role> Roles
        {
            get { return _Roles??(_Roles = new List<Role>()); }
            set { _Roles = value; }
        }

        //public void AddRole(Role role)
        //{
        //    if (Roles.Contains(Role.Guests) && role != Role.Guests)
        //        Roles.Remove(Role.Guests);
        //    if (!Roles.Contains(role))
        //        Roles.Add(role);

        //}
    }

    public class AccountValidator:AbstractValidator<Account>
    {
        public AccountValidator()
        {
            RuleFor(account => account.Username).NotEmpty()
                .Length(6, 50)
                .Matches(@"[\w]+");
            RuleFor(account => account.Password).NotEmpty()
                .Length(6, 12);
            RuleFor(account => account.Email).NotEmpty()
                .EmailAddress()
                .When(x => !x.Roles.Contains(Role.Guests));
            RuleFor(account => account.CreatedOnUtc)
                .NotEqual(System.DateTime.MinValue);           
            RuleFor(account => account.Roles).Must((roles) => roles.Count > 0)
                .WithMessage("Account must contain at least 1 role");

            RuleFor(account => account.Roles)
               .Must((roles)=>!roles.Contains(Role.Guests))            
               .When(account => account.Roles.Count > 1)
               .WithMessage("Remove Guest role before assigning another role");
            RuleFor(account => account.Roles)
                .Must((roles) => !roles.GroupBy(x => x).Any(x => x.Count() > 1))
            .WithMessage("Cannot assign the same role more than 1 time");
                    
               

            
        }
    }
}
