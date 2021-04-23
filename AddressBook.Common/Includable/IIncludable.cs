namespace AddressBook.Common.Includable
{
    public interface IIncludable { }

    public interface IIncludable<out TEntity> : IIncludable { }

    public interface IIncludable<out TEntity, out TProperty> : IIncludable<TEntity> { }
}
