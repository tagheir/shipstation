using System;
namespace OrderPlacer.SamsClub.Models
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class SamsTrackingDetailsDto
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("payload")]
        public SamsTrackingPayload Payload { get; set; }
    }

    public partial class SamsTrackingPayload
    {
        [JsonProperty("topRightRedirectUrl")]
        public Uri TopRightRedirectUrl { get; set; }

        [JsonProperty("originalEstimatedDeliveryDate")]
        public string OriginalEstimatedDeliveryDate { get; set; }

        [JsonProperty("carrierTrackingList")]
        public List<CarrierTrackingList> CarrierTrackingList { get; set; }

        [JsonProperty("universalTrackingNumber")]
        public string UniversalTrackingNumber { get; set; }

        [JsonProperty("statusText")]
        public string StatusText { get; set; }

        [JsonProperty("topRightMobileImageUrl")]
        public Uri TopRightMobileImageUrl { get; set; }

        [JsonProperty("progress")]
        public Progress Progress { get; set; }

        [JsonProperty("topRightDesktopImageUrl")]
        public Uri TopRightDesktopImageUrl { get; set; }

        [JsonProperty("trackingNumber")]
        public string TrackingNumber { get; set; }

        [JsonProperty("carrierTrackingURL")]
        public Uri CarrierTrackingUrl { get; set; }

        [JsonProperty("estimatedDeliveryDate")]
        public string EstimatedDeliveryDate { get; set; }
    }

    public partial class CarrierTrackingList
    {
        [JsonProperty("statusDate")]
        public string StatusDate { get; set; }

        [JsonProperty("carrierStatus")]
        public string CarrierStatus { get; set; }

        [JsonProperty("trackingNo")]
        public string TrackingNo { get; set; }

        [JsonProperty("scanTime")]
        public string ScanTime { get; set; }

        [JsonProperty("milestoneStatusCode")]
        public string MilestoneStatusCode { get; set; }

        [JsonProperty("statusText")]
        public string StatusText { get; set; }

        [JsonProperty("milestoneStatusDesc")]
        public string MilestoneStatusDesc { get; set; }

        [JsonProperty("createTs")]
        public string CreateTs { get; set; }

        [JsonProperty("scanCity")]
        public string ScanCity { get; set; }

        [JsonProperty("schdDeliveryDate")]
        public string SchdDeliveryDate { get; set; }

        [JsonProperty("scanState")]
        public string ScanState { get; set; }
    }

    public partial class Progress
    {
        [JsonProperty("ordered")]
        public string Ordered { get; set; }

        [JsonProperty("shipped")]
        public string Shipped { get; set; }

        [JsonProperty("orderedDate")]
        public string OrderedDate { get; set; }

        [JsonProperty("shippedDate")]
        public string ShippedDate { get; set; }
    }

    public partial class SamsTrackingDetailsDto
    {
        public static SamsTrackingDetailsDto FromJson(string json) => JsonConvert.DeserializeObject<SamsTrackingDetailsDto>(json, Converter.Settings);
    }

}
