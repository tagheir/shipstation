namespace OrderPlacer.SamsClub.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public partial class SamsRemoveItemFromCartDto
    {
        [JsonProperty("payload", NullValueHandling = NullValueHandling.Ignore)]
        public Payload Payload { get; set; }
    }

    public partial class Payload
    {
        [JsonProperty("lineItems", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> LineItems { get; set; }
    }

    public partial class SamsRemoveItemFromCartDto
    {
        public static SamsRemoveItemFromCartDto FromJson(string json) => JsonConvert.DeserializeObject<SamsRemoveItemFromCartDto>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this SamsRemoveItemFromCartDto self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }
}