using AddressBook.Common.Models.Interfaces;

namespace AddressBook.Common.Models
{
    public class BaseModel<T> : IBaseModel<T>
        where T : new()
    {
        public virtual T Id { get; set; }
    }
}
