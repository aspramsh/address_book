using AddressBook.Common.Models;

namespace AddressBook.Domain.Models
{
    public abstract class BaseZipCode<TCountry, TState, TCity> : BaseModel<int>
        where TCountry : new()
        where TState : new()
        where TCity : new()
    {
        public string Code { get; set; }

        public int CountryId { get; set; }

        public TCountry Country { get; set; }

        public int? StateId { get; set; }

        public TState State { get; set; }

        public int CityId { get; set; }

        public TCity City { get; set; }
    }
}
