using FluentValidation;
using MarketProject.CustomerService.Domain.Models;
using MarketProject.CustomerService.Domain.Validators.CustomValidators;
using MarketProject.CustomerService.Domain.ValueObjects;

namespace MarketProject.CustomerService.Domain.Validators
{
    public class CreateCustomerRequestValidator : AbstractValidator<CreateCustomerRequestModel>
    {
        public CreateCustomerRequestValidator()
        {
            RuleFor(_ => _.Name)
                .MustBeValueObject(_ => CustomerName.Create(_))
                .MustBeValueObject(_ => CustomerName.Create(_));
        }
    }
}
