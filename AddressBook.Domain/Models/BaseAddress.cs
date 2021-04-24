using AddressBook.Common.Models;

namespace AddressBook.Domain.Models
{
    public abstract class BaseAddress<TZipCode, TContact> : BaseAuditableModel<int>
        where TZipCode : new()
        where TContact : new()
    {
        public string Street { get; set; }

        public int Building { get; set; }

        public int Appartment { get; set; }

        public int ZipCodeId { get; set; }

        public TZipCode ZipCode { get; set; }

        #region Relations

        public int ContactId { get; set; }

        public TContact Contact { get; set; }

        #endregion Relations
    }
}
