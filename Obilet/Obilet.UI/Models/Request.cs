using Newtonsoft.Json;
using Obilet.UI.Models.SessionModels;
using System;

namespace Obilet.UI.Models
{
    public class Request<T>
    {

        [JsonProperty("data")]
        public T Data { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; } = DateTime.Now;

        [JsonProperty("language")]
        public string Language { get; set; } = "tr-TR";

        [JsonProperty("device-session")]
        public DeviceSession DeviceSession { get; set; }
    }
}
