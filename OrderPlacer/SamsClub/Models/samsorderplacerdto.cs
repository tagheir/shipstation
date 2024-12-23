﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using OrderPlacer.SamsClub.Models;
//
//    var samsOrderPlaceDto = SamsOrderPlaceDto.FromJson(jsonString);

namespace OrderPlacer.SamsClub.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public partial class SamsOrderPlaceDto
    {
        [JsonProperty("payload", NullValueHandling = NullValueHandling.Ignore)]
        public SamsOrderPayload Payload { get; set; }
    }

    public partial class SamsOrderPayload
    {
        [JsonProperty("contractId", NullValueHandling = NullValueHandling.Ignore)]
        public string ContractId { get; set; }

        [JsonProperty("payments", NullValueHandling = NullValueHandling.Ignore)]
        public List<Payment> Payments { get; set; }
    }

    public partial class Payment
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("amountToBeCharged", NullValueHandling = NullValueHandling.Ignore)]
        public double? AmountToBeCharged { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }
    }

    public partial class SamsOrderPlaceDto
    {
        public static SamsOrderPlaceDto FromJson(string json) => JsonConvert.DeserializeObject<SamsOrderPlaceDto>(json, Converter.Settings);
    }


}
