using AddressBook.Business.Configuration.Models;
using AddressBook.Business.Geocoders.Interfaces;
using AddressBook.Business.Geocoders.Models;
using AddressBook.Business.Models;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AddressBook.Business.Geocoders
{
    public class PositionStackHttpClient : IPositionStackHttpClient
    {
        private readonly ILogger<PositionStackHttpClient> _logger;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly GeoLocationSettings _locationSettings;

        public PositionStackHttpClient(
            ILogger<PositionStackHttpClient> logger,
            IMapper mapper,
            IHttpClientFactory httpClientFactory,
            IOptions<GeoLocationSettings> locationOptions)
        {
            _logger = logger;
            _mapper = mapper;
            _httpClient = httpClientFactory.CreateClient(nameof(PositionStackHttpClient));
            _locationSettings = locationOptions.Value;
        }

        public async Task<List<GeoLocationModel>> GetPlaceAsync(
            string name,
            CancellationToken cancellationToken)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"{_locationSettings.BaseUrl}{name}");

            var result = await _httpClient.SendAsync(message, cancellationToken);

            var stringContent = await result.Content.ReadAsStringAsync(cancellationToken);
            var response = JsonConvert.DeserializeObject<LocationData>(stringContent);

            var locations = _mapper.Map<List<GeoLocationModel>>(response.Data);
            
            return locations;
        }
    }
}
