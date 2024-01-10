using Newtonsoft.Json;

namespace Obilet.UI.Models.BusModels
{
    public class BusLocation
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
