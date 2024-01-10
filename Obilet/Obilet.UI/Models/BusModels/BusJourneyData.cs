using Newtonsoft.Json;
using System;

namespace Obilet.UI.Models.BusModels
{
    public class BusJourneyData
    {
        [JsonProperty("origin-id")]
        public int OriginId { get; set; }

        [JsonProperty("destination-id")]
        public int DestinationId { get; set; }

        [JsonProperty("departure-date")]
        public string DepartureDate { get; set; }
    }
}
