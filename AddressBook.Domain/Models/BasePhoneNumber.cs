using AddressBook.Common.Models;
using AddressBook.Domain.Enums;

namespace AddressBook.Domain.Models
{
    public abstract class BasePhoneNumber<TContact> : BaseAuditableModel<int>
        where TContact : new()
    {
        public string Phone { get; set; }

        public int ContactId { get; set; }

        public TContact Contact { get; set; }

        public PhoneType PhoneType { get; set; }
    }
}
