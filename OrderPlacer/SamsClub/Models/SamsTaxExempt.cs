using Newtonsoft.Json;
using System.Collections.Generic;

namespace OrderPlacer.SamsClub.Models
{



    public partial class SamsTaxExempt
    {
        [JsonProperty("payload", NullValueHandling = NullValueHandling.Ignore)]
        public TaxPayload Payload { get; set; }
    }

    public partial class TaxPayload
    {
        [JsonProperty("lineItems", NullValueHandling = NullValueHandling.Ignore)]
        public List<LineItem> LineItems { get; set; }
    }

    public partial class LineItem
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("taxExempt", NullValueHandling = NullValueHandling.Ignore)]
        public bool? TaxExempt { get; set; }
    }

    public partial class SamsTaxExempt
    {
        public static SamsTaxExempt FromJson(string json) => JsonConvert.DeserializeObject<SamsTaxExempt>(json, Converter.Settings);
    }




}
