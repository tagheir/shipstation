namespace OrderPlacer.Bjs.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public partial class BjsProductInfoDto
    {
        [JsonProperty("bjsClubProduct", NullValueHandling = NullValueHandling.Ignore)]
        public List<BjsClubProduct> BjsClubProduct { get; set; }

        [JsonProperty("clubDetail", NullValueHandling = NullValueHandling.Ignore)]
        public ClubDetail ClubDetail { get; set; }
    }

    public partial class BjsClubProduct
    {
        [JsonProperty("offerStatusinvalid", NullValueHandling = NullValueHandling.Ignore)]
        public string OfferStatusinvalid { get; set; }

        [JsonProperty("clubItemStandardPrice", NullValueHandling = NullValueHandling.Ignore)]
        public ClubItemStandardPrice ClubItemStandardPrice { get; set; }

        [JsonProperty("chanRopicStatus", NullValueHandling = NullValueHandling.Ignore)]
        public string ChanRopicStatus { get; set; }

        [JsonProperty("inClubOfferPrice", NullValueHandling = NullValueHandling.Ignore)]
        public InClubOfferPrice InClubOfferPrice { get; set; }

        [JsonProperty("showInClubInventory", NullValueHandling = NullValueHandling.Ignore)]
        public string ShowInClubInventory { get; set; }

        [JsonProperty("catentryId", NullValueHandling = NullValueHandling.Ignore)]
        public string CatentryId { get; set; }

        [JsonProperty("clubId", NullValueHandling = NullValueHandling.Ignore)]
        public string ClubId { get; set; }

        [JsonProperty("clubDisc", NullValueHandling = NullValueHandling.Ignore)]
        public ClubDisc ClubDisc { get; set; }

        [JsonProperty("clubIdentifier", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? ClubIdentifier { get; set; }

        [JsonProperty("clubPriceVisible", NullValueHandling = NullValueHandling.Ignore)]
        public string ClubPriceVisible { get; set; }

        [JsonProperty("offerStatus")]
        public object OfferStatus { get; set; }
    }

    public partial class ClubDisc
    {
        [JsonProperty("clubDiscPrice", NullValueHandling = NullValueHandling.Ignore)]
        public long? ClubDiscPrice { get; set; }
    }

    public partial class ClubItemStandardPrice
    {
        [JsonProperty("symbol", NullValueHandling = NullValueHandling.Ignore)]
        public string Symbol { get; set; }

        [JsonProperty("amount", NullValueHandling = NullValueHandling.Ignore)]
        public double? Amount { get; set; }
    }

    public partial class InClubOfferPrice
    {
        [JsonProperty("symbol", NullValueHandling = NullValueHandling.Ignore)]
        public string Symbol { get; set; }

        [JsonProperty("amount", NullValueHandling = NullValueHandling.Ignore)]
        public double? Amount { get; set; }

        [JsonProperty("currency", NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; }
    }

    public partial class ClubDetail
    {
        [JsonProperty("clubName", NullValueHandling = NullValueHandling.Ignore)]
        public string ClubName { get; set; }

        [JsonProperty("isClubRopic", NullValueHandling = NullValueHandling.Ignore)]
        public string IsClubRopic { get; set; }

        [JsonProperty("pickUpHours", NullValueHandling = NullValueHandling.Ignore)]
        public string PickUpHours { get; set; }

        [JsonProperty("isClubSap", NullValueHandling = NullValueHandling.Ignore)]
        public string IsClubSap { get; set; }
    }

    public partial class BjsProductInfoDto
    {
        public static BjsProductInfoDto FromJson(string json) => JsonConvert.DeserializeObject<BjsProductInfoDto>(json, Converter.Settings);
    }


}
