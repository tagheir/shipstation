﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using OrderPlacer.SamsClub.Models;
//
//    var samsAddItemToCartDto = SamsAddItemToCartDto.FromJson(jsonString);

namespace OrderPlacer.SamsClub.Models
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class SamsAddItemToCartDto
    {
        [JsonProperty("payload", NullValueHandling = NullValueHandling.Ignore)]
        public Payload1 Payload { get; set; }
    }

    public partial class Payload1
    {
        [JsonProperty("lineItems", NullValueHandling = NullValueHandling.Ignore)]
        public List<LineItem> LineItems { get; set; }
    }

    public partial class LineItem
    {
        [JsonProperty("skuId", NullValueHandling = NullValueHandling.Ignore)]
        public string SkuId { get; set; }

        [JsonProperty("productId", NullValueHandling = NullValueHandling.Ignore)]
        public string ProductId { get; set; }

        [JsonProperty("itemNumber", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? ItemNumber { get; set; }

        [JsonProperty("channel", NullValueHandling = NullValueHandling.Ignore)]
        public string Channel { get; set; }

        [JsonProperty("quantity", NullValueHandling = NullValueHandling.Ignore)]
        public long? Quantity { get; set; }
    }

    public partial class SamsAddItemToCartDto
    {
        public static SamsAddItemToCartDto FromJson(string json) => JsonConvert.DeserializeObject<SamsAddItemToCartDto>(json, Converter.Settings);
    }



}

