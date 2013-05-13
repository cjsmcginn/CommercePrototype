using Raven.Client.Listeners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
namespace CommercePrototype.Core
{
    public class ValidationStoreListener : IDocumentStoreListener
    {
        public void AfterStore(string key, object entityInstance, Raven.Json.Linq.RavenJObject metadata)
        {
           //do nothing
        }

        public bool BeforeStore(string key, object entityInstance, Raven.Json.Linq.RavenJObject metadata, Raven.Json.Linq.RavenJObject original)
        {
            var validatorAttribute = entityInstance.GetType().CustomAttributes.SingleOrDefault(x => x.AttributeType.FullName == "FluentValidation.Attributes.ValidatorAttribute");
            if (validatorAttribute == null)
                return true;

            var factory = new ValidatorFactory();
            var validator = factory.CreateInstance((Type)validatorAttribute.ConstructorArguments[0].Value);
            var validationResult = validator.Validate(entityInstance);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);


            return true;
        }
    }
}
