using AddressBook.Api.V1.Models.RequestModels;
using FluentValidation;

namespace AddressBook.Api.V1.Validation
{
    public class ContactCreateModelValidator : AbstractValidator<ContactCreateRequestModel>
    {
        public ContactCreateModelValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MinimumLength(2);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MinimumLength(2);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.ZipCodeId)
                .GreaterThanOrEqualTo(1)
                .When(x => x.ZipCodeId != default);

            RuleFor(x => x.PhoneNumbers)
                .NotEmpty();

            RuleForEach(x => x.PhoneNumbers)
                .SetValidator(new PhoneNumberCreateModelValidator());
        }
    }
}
