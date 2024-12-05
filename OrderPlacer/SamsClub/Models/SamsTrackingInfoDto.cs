﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using OrderPlacer.Bjs.Models;
//
//    var samsTrackingInfoDto = SamsTrackingInfoDto.FromJson(jsonString);

namespace OrderPlacer.SamsClub.Models
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class SamsTrackingInfoDto
    {
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }

        [JsonProperty("payload", NullValueHandling = NullValueHandling.Ignore)]
        public TrackingPayload Payload { get; set; }
    }

    public partial class TrackingPayload
    {
        [JsonProperty("topRightRedirectUrl", NullValueHandling = NullValueHandling.Ignore)]
        public Uri TopRightRedirectUrl { get; set; }

        [JsonProperty("originalEstimatedDeliveryDate", NullValueHandling = NullValueHandling.Ignore)]
        public string OriginalEstimatedDeliveryDate { get; set; }

        [JsonProperty("carrierTrackingList", NullValueHandling = NullValueHandling.Ignore)]
        public List<CarrierTrackingList> CarrierTrackingList { get; set; }

        [JsonProperty("universalTrackingNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string UniversalTrackingNumber { get; set; }

        [JsonProperty("statusText", NullValueHandling = NullValueHandling.Ignore)]
        public string StatusText { get; set; }

        [JsonProperty("topRightMobileImageUrl", NullValueHandling = NullValueHandling.Ignore)]
        public Uri TopRightMobileImageUrl { get; set; }

        [JsonProperty("progress", NullValueHandling = NullValueHandling.Ignore)]
        public Progress Progress { get; set; }

        [JsonProperty("topRightDesktopImageUrl", NullValueHandling = NullValueHandling.Ignore)]
        public Uri TopRightDesktopImageUrl { get; set; }

        [JsonProperty("trackingNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string TrackingNumber { get; set; }

        [JsonProperty("carrierTrackingURL", NullValueHandling = NullValueHandling.Ignore)]
        public Uri CarrierTrackingUrl { get; set; }

        [JsonProperty("estimatedDeliveryDate", NullValueHandling = NullValueHandling.Ignore)]
        public string EstimatedDeliveryDate { get; set; }
    }

    public partial class CarrierTrackingList
    {

        [JsonProperty("utcScanDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? UtcScanDate { get; set; }

    }

    public partial class Progress
    {
        

        [JsonProperty("outForDelivery", NullValueHandling = NullValueHandling.Ignore)]
        public string OutForDelivery { get; set; }

        [JsonProperty("delivered", NullValueHandling = NullValueHandling.Ignore)]
        public string Delivered { get; set; }


    }

    public partial class SamsTrackingInfoDto
    {
        public static SamsTrackingInfoDto FromJson(string json) => JsonConvert.DeserializeObject<SamsTrackingInfoDto>(json, Converter.Settings);
    }

    
}
