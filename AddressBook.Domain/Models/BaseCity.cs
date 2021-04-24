using AddressBook.Common.Models;
using System.Collections.Generic;

namespace AddressBook.Domain.Models
{
    public abstract class BaseCity<TCountry, TState, TZipCode> : BaseModel<int>
        where TCountry : new()
        where TState : new()
        where TZipCode : new()
    {
        public string Name { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string TimeZone { get; set; }

        #region Relations

        public int CountryId { get; set; }

        public TCountry Country { get; set; }

        public int? StateId { get; set; }

        public TState State { get; set; }

        public IEnumerable<TZipCode> ZipCodes { get; set; }

        #endregion Relations
    }
}
