using AddressBook.Common.Models;
using System.Collections.Generic;

namespace AddressBook.Domain.Models
{
    public abstract class BaseContact<TPhoneNumber, TZipCode> : BaseAuditableModel<int>
        where TPhoneNumber : new()
        where TZipCode : new()
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Street { get; set; }

        public int? Building { get; set; }

        public int? Appartment { get; set; }

        #region Relations

        public int? ZipCodeId { get; set; }

        public TZipCode ZipCode { get; set; }

        public IEnumerable<TPhoneNumber> PhoneNumbers { get; set; }

        #endregion Relations
    }
}
