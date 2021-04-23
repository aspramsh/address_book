using AddressBook.Common.Models;
using System.Collections.Generic;

namespace AddressBook.Domain.Models
{
    public class BaseCountry<TState, TCity, TZipCode> : BaseModel<int>
        where TState : new()
        where TCity : new()
        where TZipCode : new()
    {
        public string Name { get; set; }

        public string Code { get; set; }

        #region Relations

        public IEnumerable<TState> States { get; set; }

        public IEnumerable<TCity> Cities { get; set; }

        public IEnumerable<TZipCode> ZipCodes { get; set; }

        #endregion Relations
    }
}
