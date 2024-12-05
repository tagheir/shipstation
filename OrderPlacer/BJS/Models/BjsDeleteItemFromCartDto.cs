using Newtonsoft.Json;

namespace OrderPlacer.BJS.Models
{
    public partial class BjsDeleteItemFromCartDto
    {
        [JsonProperty("calculateOrder", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? CalculateOrder { get; set; }

        [JsonProperty("catalogId", NullValueHandling = NullValueHandling.Ignore)]
        public long? CatalogId { get; set; }

        [JsonProperty("langId", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? LangId { get; set; }

        [JsonProperty("orderId", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderId { get; set; }

        [JsonProperty("orderItemId", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? OrderItemId { get; set; }

        [JsonProperty("storeId", NullValueHandling = NullValueHandling.Ignore)]
        public long? StoreId { get; set; }
    }

    public partial class BjsDeleteItemFromCartDto
    {
        public static BjsDeleteItemFromCartDto FromJson(string json) => JsonConvert.DeserializeObject<BjsDeleteItemFromCartDto>(json, Converter.Settings);
    }
}
