using AddressBook.Api.V1.Models.RequestModels;
using FluentValidation;

namespace AddressBook.Api.V1.Validation
{
    public class StateCreateModelValidator : AbstractValidator<StateCreateRequestModel>
    {
        public StateCreateModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(2);

            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.CountryId)
                .GreaterThanOrEqualTo(1);
        }
    }
}
