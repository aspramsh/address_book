using AddressBook.Common.Models;
using System.Collections.Generic;

namespace AddressBook.Domain.Models
{
    public class BaseContact<TAddress, TPhoneNumber> : BaseAuditableModel<int>
        where TAddress : new()
        where TPhoneNumber : new()
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        #region Relations

        public int? AddressId { get; set; }

        public TAddress Address { get; set; }

        public IEnumerable<TPhoneNumber> PhoneNumbers { get; set; }

        #endregion Relations
    }
}
