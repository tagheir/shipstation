using Newtonsoft.Json;
using System.Collections.Generic;

namespace OrderPlacer.SamsClub.Models
{
    public partial class SamsSearchProductQueryDto
    {
        [JsonProperty("productIds", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> ProductIds { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("clubId", NullValueHandling = NullValueHandling.Ignore)]
        public long? ClubId { get; set; }
    }

    public partial class SamsSearchProductQueryDto
    {
        public static SamsSearchProductQueryDto FromJson(string json) => JsonConvert.DeserializeObject<SamsSearchProductQueryDto>(json, Converter.Settings);
    }
}
