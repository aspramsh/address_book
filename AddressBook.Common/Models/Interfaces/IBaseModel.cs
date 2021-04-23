namespace AddressBook.Common.Models.Interfaces
{
    public interface IBaseModel<T>
    {
        T Id { get; set; }
    }
}
