using AddressBook.Api.V1.Models.RequestModels;
using FluentValidation;

namespace AddressBook.Api.V1.Validation
{
    public class PhoneNumberCreateModelValidator : AbstractValidator<PhoneNumberCreateRequestModel>
    {
        public PhoneNumberCreateModelValidator()
        {
            RuleFor(x => x.Phone)
                .NotEmpty()
                .Length(10);
        }
    }
}
