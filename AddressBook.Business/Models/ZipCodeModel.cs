using AddressBook.Domain.Models;
using System;
using System.Collections.Generic;
namespace AddressBook.Business.Models
{
    public class ZipCodeModel : BaseZipCode<CountryModel, StateModel, CityModel>
    {
    }
}
