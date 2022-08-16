using FluentValidation;
using MarketProject.CustomerService.Common;
using MarketProject.CustomerService.Domain.Entities;
using MarketProject.CustomerService.Domain.ValueObjects;

namespace MarketProject.CustomerService.Domain.Validators.CustomValidators
{
    public static class ValueObjectCustomValidator
    {
        public static IRuleBuilderOptions<T, string> MustBeValueObject<T, TValueObject>
        (
            this IRuleBuilder<T, string> ruleBuilder, 
            Func<string, Result<TValueObject>> factoryMethod
        ) where TValueObject : struct
        {
            var result = (IRuleBuilderOptions<T, string>)ruleBuilder.Custom((value, context) =>
            {
                Result<TValueObject> result = factoryMethod(value);

                if (result.IsFailure)
                    context.AddFailure(result.Error.Message);
            });

            return result;
        }

        public static IRuleBuilderOptions<T, string> MustBeAnEntity<T, TValueObject>
        (
            this IRuleBuilder<T, string> ruleBuilder, 
            Func<string, Result<TValueObject>> factoryMethod
        ) where TValueObject : Entity
        {
            var result = (IRuleBuilderOptions<T, string>)ruleBuilder.Custom((value, context) =>
            {
                Result<TValueObject> result = factoryMethod(value);

                if (result.IsFailure)
                    context.AddFailure(result.Error.Message);
            });

            return result;
        }

    }
}
