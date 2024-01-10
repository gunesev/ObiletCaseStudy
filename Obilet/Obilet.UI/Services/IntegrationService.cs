using Newtonsoft.Json;
using Obilet.UI.Interfaces;
using Obilet.UI.Models;
using Obilet.UI.Models.BusModels;
using Obilet.UI.Models.SessionModels;
using System.Net;
using System.Text;

namespace Obilet.UI.Services
{
    public class IntegrationService : IIntegrationService
    {
        #region Fields
        private string apiUrl = "https://v2-api.obilet.com/api/";
        #endregion

        #region Methods
        public async Task<Response<DeviceSession>> GetSession(SessionRequest sessionRequest)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Basic JEcYcEMyantZV095WVc3G2JtVjNZbWx1");
                var url = apiUrl + "client/getsession";

                var jsonSerializerSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                };

                var content = new StringContent(JsonConvert.SerializeObject(sessionRequest, jsonSerializerSettings), Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);

                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception($"Status Code:{response.StatusCode} ReasonPhrase:{response.ReasonPhrase}");

                var responseString = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<Response<DeviceSession>>(responseString);

                return result;
            }
        }

        public async Task<Response<List<BusLocation>>> GetBusLocations(Request<string> request)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Basic JEcYcEMyantZV095WVc3G2JtVjNZbWx1");
                var url = apiUrl + "location/getbuslocations";

                var jsonSerializerSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                };

                var content = new StringContent(JsonConvert.SerializeObject(request, jsonSerializerSettings), Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);

                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception($"Status Code:{response.StatusCode} ReasonPhrase:{response.ReasonPhrase}");

                var responseString = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<Response<List<BusLocation>>>(responseString);

                return result;
            }
        }

        public async Task<Response<List<BusJourney>>> GetBusJourneys(Request<BusJourneyData> request)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Basic JEcYcEMyantZV095WVc3G2JtVjNZbWx1");
                var url = apiUrl + "journey/getbusjourneys";

                var jsonSerializerSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                };

                var content = new StringContent(JsonConvert.SerializeObject(request, jsonSerializerSettings), Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);

                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception($"Status Code:{response.StatusCode} ReasonPhrase:{response.ReasonPhrase}");

                var responseString = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<Response<List<BusJourney>>>(responseString);

                return result;
            }
        }
        #endregion

    }
}