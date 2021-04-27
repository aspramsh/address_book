using AddressBook.Api.V1.Models.RequestModels;
using FluentValidation;

namespace AddressBook.Api.V1.Validation
{
    public class CityCreateModelValidator : AbstractValidator<CityCreateRequestModel>
    {
        public CityCreateModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(2);

            RuleFor(x => x.CountryId)
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.StateId)
                .GreaterThanOrEqualTo(1)
                .When(x => x.StateId != default);
        }
    }
}