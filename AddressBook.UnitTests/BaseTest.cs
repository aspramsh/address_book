using AddressBook.Api.V1.Mapping;
using AutoMapper;

namespace AddressBook.UnitTests
{
    public class BaseTest
    {
        protected static readonly IMapper Mapper;

        static BaseTest()
        {
            Mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
                cfg.AddProfile(new Business.Mapping.MappingProfile());
            }).CreateMapper();
        }

        protected BaseTest()
        {
            FakeDbContext.Create();
        }

        protected FakeAddressBookContext FakeDbContext { get; }
            = new FakeAddressBookContext();
    }
}
