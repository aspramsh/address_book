using AddressBook.Business.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AddressBook.Business.Geocoders.Interfaces
{
    public interface IPositionStackHttpClient
    {
        Task<List<GeoLocationModel>> GetPlaceAsync(
            string name,
            CancellationToken cancellationToken);
    }
}
