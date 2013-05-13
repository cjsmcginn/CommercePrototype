using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercePrototype.Core
{
    public class ValidatorFactory:ValidatorFactoryBase
    {
        public override IValidator CreateInstance(Type validatorType)
        {
            //return base.GetValidator(validatorType);
            //base.
            return Activator.CreateInstance(validatorType) as IValidator;
        }

        
    }
}
