using AddressBook.Common.Helpers;
using AddressBook.Common.Tests;
using AddressBook.DataAccess;

namespace AddressBook.UnitTests
{
    public class FakeAddressBookContext : ContextFactoryMoq<AddressBookContext>
    {
        protected override void SeedData()
        {
            var dataSeed = new FakeDataSeed(Context);
            AsyncUtil.RunSync(() => dataSeed.SeedAllInitialDataAsync());
        }
    }
}