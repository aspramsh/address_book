using AddressBook.Api.V1.Controllers;
using AddressBook.Api.V1.Models.ViewModels;
using AddressBook.Business.Services;
using AddressBook.DataAccess.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Xunit;

namespace AddressBook.UnitTests.V1
{
    public class CountriesControllerTest : BaseTest
    {
        private readonly CountriesController _countriesController;

        public CountriesControllerTest()
        {
            if (!FakeDbContext.Context.Country.Any())
            {
                throw new InvalidDataException("No data for testing ");
            }

            var countryRepository =
                new CountryRepository(FakeDbContext.Context, Mock.Of<ILoggerFactory>());

            var countryService =
                new CountryService(Mapper, Mock.Of<ILoggerFactory>(), countryRepository);

            _countriesController = new CountriesController(
                Mock.Of<ILogger<CountriesController>>(),
                Mapper,
                countryService);
        }

        [Fact(DisplayName = "Company list: Should return Ok")]
        public async void Company_List_Should_Return_Ok_Async()
        {
            // Act
            var response = await _countriesController.GetAsync(
                cancellationToken: CancellationToken.None);

            // Assert
            Assert.IsType<List<CountryViewModel>>(response.Value);
            Assert.NotEmpty(response.Value);
        }

        [Theory(DisplayName = "Company list with search: Should return Ok")]
        [InlineData("armenia")]
        public async void Company_List_With_Search_Should_Return_Ok_Async(string searchValue)
        {
            // Act
            var response = await _countriesController.GetAsync(
                searchValue,
                CancellationToken.None);

            // Assert
            Assert.IsType<List<CountryViewModel>>(response.Value);
            Assert.NotEmpty(response.Value);
            Assert.Single(response.Value);
        }
    }
}
