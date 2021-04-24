using AddressBook.Common.Models;
using System.Collections.Generic;

namespace AddressBook.Domain.Models
{
    public abstract class BaseState<TCountry, TCity, TZipCode> : BaseModel<int>
        where TCountry : new()
        where TCity : new()
        where TZipCode : new()
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public int CountryId { get; set; }

        public TCountry Country { get; set; }

        #region Relations

        public IEnumerable<TCity> Cities { get; set; }

        public IEnumerable<TZipCode> ZipCodes { get; set; }

        #endregion Relations
    }
}
