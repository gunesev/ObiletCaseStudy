using Newtonsoft.Json;
using Obilet.UI.Interfaces;
using Obilet.UI.Models;
using Obilet.UI.Models.BusModels;
using Obilet.UI.Models.SessionModels;
using System.Net;
using System.Text;
using Obilet.UI.Configuration;

namespace Obilet.UI.Services
{
    public class IntegrationService : IIntegrationService
    {
        #region Fields
        private readonly AppSettings _appSettings;
        private readonly HttpClient _client;
        private string apiUrl;
        #endregion

        #region Ctor
        public IntegrationService(AppSettings appSettings, IHttpClientFactory httpClientFactory)
        {
            _appSettings = appSettings;
            _client = httpClientFactory.CreateClient();
            apiUrl = _appSettings.ObiletApiUrl;
        }
        #endregion

        #region Methods
        private void AddAuthorization()
        {
            _client.DefaultRequestHeaders.Add("Authorization", "Basic JEcYcEMyantZV095WVc3G2JtVjNZbWx1");
        }

        private StringContent CreateStringContent(object _object)
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
            };

            var content = new StringContent(JsonConvert.SerializeObject(_object, jsonSerializerSettings), Encoding.UTF8, "application/json");

            return content;
        }
        public async Task<Response<DeviceSession>> GetSession(SessionRequest sessionRequest)
        {
            AddAuthorization();

            var url = $"{apiUrl}client/getsession";

            var content = CreateStringContent(sessionRequest);

            var response = await _client.PostAsync(url, content);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception($"Status Code:{response.StatusCode} ReasonPhrase:{response.ReasonPhrase}");

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<Response<DeviceSession>>(responseString);

            return result;
        }

        public async Task<Response<List<BusLocation>>> GetBusLocations(Request<string> request)
        {
            AddAuthorization();

            var url = $"{apiUrl}location/getbuslocations";

            var content = CreateStringContent(request);

            var response = await _client.PostAsync(url, content);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception($"Status Code:{response.StatusCode} ReasonPhrase:{response.ReasonPhrase}");

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<Response<List<BusLocation>>>(responseString);

            return result;
        }

        public async Task<Response<List<BusJourney>>> GetBusJourneys(Request<BusJourneyData> request)
        {
            AddAuthorization();

            var url = $"{apiUrl}journey/getbusjourneys";

            var content = CreateStringContent(request);

            var response = await _client.PostAsync(url, content);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception($"Status Code:{response.StatusCode} ReasonPhrase:{response.ReasonPhrase}");

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<Response<List<BusJourney>>>(responseString);

            return result;
        }
        #endregion

    }
}