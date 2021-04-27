using AddressBook.Api.V1.Models.RequestModels;
using FluentValidation;

namespace AddressBook.Api.V1.Validation
{
    public class ZipCodeCreateModelValidator : AbstractValidator<ZipCodeCreateRequestModel>
    {
        public ZipCodeCreateModelValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty()
                .MinimumLength(2);

            RuleFor(x => x.CityId)
                .GreaterThanOrEqualTo(1);
        }
    }
}
