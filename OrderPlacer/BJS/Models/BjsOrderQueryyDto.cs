﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using OrderPlacer.Bjs.Models;
//
//    var bjsOrderQueryDto = BjsOrderQueryDto.FromJson(jsonString);

namespace OrderPlacer.Bjs.Models
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class BjsOrderQueryDto
    {
        public BjsOrderQueryDto(string headerKey)
        {
            Order = new Order()
            {
                OrderHeaderKey = headerKey,
                UserOverride = new UserOverride()
                {
                    OrderHeader = new OrderHeader()
                    {
                        EnterpriseCode = 10201
                    }
                }

            };
            TrackingInvocationPage = "Tracking";
            
        }
        [JsonProperty("Order", NullValueHandling = NullValueHandling.Ignore)]
        public Order Order { get; set; }

        [JsonProperty("trackingInvocationPage", NullValueHandling = NullValueHandling.Ignore)]
        public string TrackingInvocationPage { get; set; }
    }

    public partial class Order
    {
        [JsonProperty("UserOverride", NullValueHandling = NullValueHandling.Ignore)]
        public UserOverride UserOverride { get; set; }


    }

    public partial class UserOverride
    {
        [JsonProperty("Order_Header", NullValueHandling = NullValueHandling.Ignore)]
        public OrderHeader OrderHeader { get; set; }
    }

    public partial class OrderHeader
    {
        [JsonProperty("EnterpriseCode", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? EnterpriseCode { get; set; }
    }

    public partial class BjsOrderQueryDto
    {
        public static BjsOrderQueryDto FromJson(string json) => JsonConvert.DeserializeObject<BjsOrderQueryDto>(json, Converter.Settings);
    }


}
