﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using OrderPlacer.SamsClub.Models;
//
//    var samsOrderReceiverDto = SamsOrderReceiverDto.FromJson(jsonString);

namespace OrderPlacer.SamsClub.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public partial class SamsOrderReceiverDto
    {
        [JsonProperty("metadata", NullValueHandling = NullValueHandling.Ignore)]
        public Metadata Metadata { get; set; }

        [JsonProperty("payload", NullValueHandling = NullValueHandling.Ignore)]
        public OrderPayload Payload { get; set; }

        [JsonProperty("errors", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> Errors { get; set; }
    }






    public partial class OrderPayload
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("cartId", NullValueHandling = NullValueHandling.Ignore)]
        public string CartId { get; set; }

        [JsonProperty("orderId", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderId { get; set; }

        [JsonProperty("clubId", NullValueHandling = NullValueHandling.Ignore)]
        public long? ClubId { get; set; }

        [JsonProperty("fulfillmentGroups", NullValueHandling = NullValueHandling.Ignore)]
        public List<FulfillmentGroup> FulfillmentGroups { get; set; }


        [JsonProperty("payments", NullValueHandling = NullValueHandling.Ignore)]
        public List<Payment> Payments { get; set; }

        [JsonProperty("giftCards", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> GiftCards { get; set; }

        [JsonProperty("cvvRequired", NullValueHandling = NullValueHandling.Ignore)]
        public bool? CvvRequired { get; set; }

        [JsonProperty("totals", NullValueHandling = NullValueHandling.Ignore)]
        public Totals Totals { get; set; }

        [JsonProperty("prepaySummary", NullValueHandling = NullValueHandling.Ignore)]
        public PrepaySummary PrepaySummary { get; set; }

        [JsonProperty("postpaySummary", NullValueHandling = NullValueHandling.Ignore)]
        public PostpaySummary PostpaySummary { get; set; }

        [JsonProperty("nonEligiblePromotions", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> NonEligiblePromotions { get; set; }

        [JsonProperty("eligiblePromotions", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> EligiblePromotions { get; set; }

        [JsonProperty("substitutionEnabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool? SubstitutionEnabled { get; set; }

        [JsonProperty("exclusionItemPresent", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ExclusionItemPresent { get; set; }
    }











    public partial class SamsOrderReceiverDto
    {
        public static SamsOrderReceiverDto FromJson(string json) => JsonConvert.DeserializeObject<SamsOrderReceiverDto>(json, Converter.Settings);
    }




}