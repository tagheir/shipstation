﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using OrderPlacer.Bjs.Models;
//
//    var orderHistoryDetailDto = OrderHistoryDetailDto.FromJson(jsonString);

namespace OrderPlacer.Bjs.Models
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class OrderHistoryDetailDto
    {
        [JsonProperty("GeneratedOn", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? GeneratedOn { get; set; }

        [JsonProperty("IsFirstPage", NullValueHandling = NullValueHandling.Ignore)]
        public string IsFirstPage { get; set; }

        [JsonProperty("IsLastPage", NullValueHandling = NullValueHandling.Ignore)]
        public string IsLastPage { get; set; }

        [JsonProperty("IsValidPage", NullValueHandling = NullValueHandling.Ignore)]
        public string IsValidPage { get; set; }

        [JsonProperty("PageNumber", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? PageNumber { get; set; }

        [JsonProperty("PageSize", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? PageSize { get; set; }

        [JsonProperty("Output", NullValueHandling = NullValueHandling.Ignore)]
        public Output Output { get; set; }
    }

    public partial class Output
    {
        [JsonProperty("OrderList", NullValueHandling = NullValueHandling.Ignore)]
        public OrderList OrderList { get; set; }
    }

    public partial class OrderList
    {
        [JsonProperty("LastOrderHeaderKey", NullValueHandling = NullValueHandling.Ignore)]
        public string LastOrderHeaderKey { get; set; }

        [JsonProperty("LastRecordSet", NullValueHandling = NullValueHandling.Ignore)]
        public string LastRecordSet { get; set; }

        [JsonProperty("ReadFromHistory", NullValueHandling = NullValueHandling.Ignore)]
        public string ReadFromHistory { get; set; }

        [JsonProperty("TotalOrderList", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? TotalOrderList { get; set; }

        [JsonProperty("Order", NullValueHandling = NullValueHandling.Ignore)]
        public List<Order> Order { get; set; }
    }

    public partial class Order
    {
        [JsonProperty("OrderLines", NullValueHandling = NullValueHandling.Ignore)]
        public List<OrderLine> OrderLines { get; set; }

        [JsonProperty("OverallTotals", NullValueHandling = NullValueHandling.Ignore)]
        public OverallTotals OverallTotals { get; set; }

        [JsonProperty("MinOrderStatus", NullValueHandling = NullValueHandling.Ignore)]
        public string MinOrderStatus { get; set; }

        [JsonProperty("OrderDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? OrderDate { get; set; }

        [JsonProperty("OrderHeaderKey", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderHeaderKey { get; set; }

        [JsonProperty("OrderNo", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? OrderNo { get; set; }

        [JsonProperty("DepartmentCode", NullValueHandling = NullValueHandling.Ignore)]
        public string DepartmentCode { get; set; }

        [JsonProperty("Extn", NullValueHandling = NullValueHandling.Ignore)]
        public OrderExtn Extn { get; set; }

        [JsonProperty("OrderStatusDesc", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderStatusDesc { get; set; }
    }

    public partial class OrderExtn
    {
        [JsonProperty("ExtnIsSDDMixCart", NullValueHandling = NullValueHandling.Ignore)]
        public string ExtnIsSddMixCart { get; set; }
    }

    public partial class OrderLine
    {
        [JsonProperty("Item", NullValueHandling = NullValueHandling.Ignore)]
        public Item Item { get; set; }

        [JsonProperty("LinePriceInfo", NullValueHandling = NullValueHandling.Ignore)]
        public LinePriceInfo LinePriceInfo { get; set; }

        [JsonProperty("Extn", NullValueHandling = NullValueHandling.Ignore)]
        public OrderLineExtn Extn { get; set; }

        [JsonProperty("PersonInfoShipTo", NullValueHandling = NullValueHandling.Ignore)]
        public PersonInfoShipTo PersonInfoShipTo { get; set; }

        [JsonProperty("OrderStatuses", NullValueHandling = NullValueHandling.Ignore)]
        public OrderStatuses OrderStatuses { get; set; }

        [JsonProperty("LineType", NullValueHandling = NullValueHandling.Ignore)]
        public string LineType { get; set; }
    }

    public partial class OrderLineExtn
    {
        [JsonProperty("ExtnProductID", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? ExtnProductId { get; set; }
    }

    public partial class Item
    {
        [JsonProperty("ItemID", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? ItemId { get; set; }

        [JsonProperty("ItemShortDesc", NullValueHandling = NullValueHandling.Ignore)]
        public string ItemShortDesc { get; set; }

        [JsonProperty("CatentryId", NullValueHandling = NullValueHandling.Ignore)]
        public string CatentryId { get; set; }

        [JsonProperty("ParentCatentryId", NullValueHandling = NullValueHandling.Ignore)]
        public string ParentCatentryId { get; set; }
    }

    public partial class LinePriceInfo
    {
        [JsonProperty("UnitPrice", NullValueHandling = NullValueHandling.Ignore)]
        public string UnitPrice { get; set; }
    }

    public partial class OrderStatuses
    {
        [JsonProperty("OrderStatus", NullValueHandling = NullValueHandling.Ignore)]
        public OrderStatus OrderStatus { get; set; }
    }

    public partial class OrderStatus
    {
        [JsonProperty("Status", NullValueHandling = NullValueHandling.Ignore)]
        public Status? Status { get; set; }

        [JsonProperty("Details", NullValueHandling = NullValueHandling.Ignore)]
        public Details Details { get; set; }
    }

    public partial class Details
    {
        [JsonProperty("ExpectedDeliveryDate", NullValueHandling = NullValueHandling.Ignore)]
        public ExpectedDate? ExpectedDeliveryDate { get; set; }

        [JsonProperty("ExpectedShipmentDate", NullValueHandling = NullValueHandling.Ignore)]
        public ExpectedDate? ExpectedShipmentDate { get; set; }
    }

    public partial class PersonInfoShipTo
    {
        [JsonProperty("AddressLine1", NullValueHandling = NullValueHandling.Ignore)]
        public string AddressLine1 { get; set; }

        [JsonProperty("Country", NullValueHandling = NullValueHandling.Ignore)]
        public string Country { get; set; }

        [JsonProperty("DayPhone", NullValueHandling = NullValueHandling.Ignore)]
        public string DayPhone { get; set; }

        [JsonProperty("EMailID", NullValueHandling = NullValueHandling.Ignore)]
        public string EMailId { get; set; }

        [JsonProperty("FirstName", NullValueHandling = NullValueHandling.Ignore)]
        public string FirstName { get; set; }

        [JsonProperty("LastName", NullValueHandling = NullValueHandling.Ignore)]
        public string LastName { get; set; }

        [JsonProperty("State", NullValueHandling = NullValueHandling.Ignore)]
        public string State { get; set; }

        [JsonProperty("ZipCode", NullValueHandling = NullValueHandling.Ignore)]
        public string ZipCode { get; set; }

        [JsonProperty("AddressLine2", NullValueHandling = NullValueHandling.Ignore)]
        public string AddressLine2 { get; set; }
    }

    public partial class OverallTotals
    {
        [JsonProperty("GrandCharges", NullValueHandling = NullValueHandling.Ignore)]
        public string GrandCharges { get; set; }

        [JsonProperty("GrandDiscount", NullValueHandling = NullValueHandling.Ignore)]
        public string GrandDiscount { get; set; }

        [JsonProperty("GrandTax", NullValueHandling = NullValueHandling.Ignore)]
        public string GrandTax { get; set; }

        [JsonProperty("GrandTotal", NullValueHandling = NullValueHandling.Ignore)]
        public string GrandTotal { get; set; }

        [JsonProperty("HdrCharges", NullValueHandling = NullValueHandling.Ignore)]
        public string HdrCharges { get; set; }

        [JsonProperty("HdrDiscount", NullValueHandling = NullValueHandling.Ignore)]
        public string HdrDiscount { get; set; }

        [JsonProperty("HdrTax", NullValueHandling = NullValueHandling.Ignore)]
        public string HdrTax { get; set; }

        [JsonProperty("HdrTotal", NullValueHandling = NullValueHandling.Ignore)]
        public string HdrTotal { get; set; }

        [JsonProperty("LineSubTotal", NullValueHandling = NullValueHandling.Ignore)]
        public string LineSubTotal { get; set; }
    }

    public partial struct ExpectedDate
    {
        public DateTimeOffset? DateTime;
        public List<DateTimeOffset> DateTimeArray;

        public static implicit operator ExpectedDate(DateTimeOffset DateTime) => new ExpectedDate { DateTime = DateTime };
        public static implicit operator ExpectedDate(List<DateTimeOffset> DateTimeArray) => new ExpectedDate { DateTimeArray = DateTimeArray };
    }

    public partial struct Status
    {
        public string String;
        public List<dynamic> StringArray;

        public static implicit operator Status(string String) => new Status { String = String };
        public static implicit operator Status(List<dynamic> StringArray) => new Status { StringArray = StringArray };
    }

    public partial class OrderHistoryDetailDto
    {
        public static OrderHistoryDetailDto FromJson(string json) => JsonConvert.DeserializeObject<OrderHistoryDetailDto>(json, OrderPlacer.Bjs.Models.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this OrderHistoryDetailDto self) => JsonConvert.SerializeObject(self, OrderPlacer.Bjs.Models.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                ExpectedDateConverter.Singleton,
                StatusConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }

    internal class ExpectedDateConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ExpectedDate) || t == typeof(ExpectedDate?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    DateTimeOffset dt;
                    if (DateTimeOffset.TryParse(stringValue, out dt))
                    {
                        return new ExpectedDate { DateTime = dt };
                    }
                    break;
                case JsonToken.StartArray:
                    var arrayValue = serializer.Deserialize<List<DateTimeOffset>>(reader);
                    return new ExpectedDate { DateTimeArray = arrayValue };
            }
            throw new Exception("Cannot unmarshal type ExpectedDate");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (ExpectedDate)untypedValue;
            if (value.DateTime != null)
            {
                serializer.Serialize(writer, value.DateTime.Value.ToString("o", System.Globalization.CultureInfo.InvariantCulture));
                return;
            }
            if (value.DateTimeArray != null)
            {
                serializer.Serialize(writer, value.DateTimeArray);
                return;
            }
            throw new Exception("Cannot marshal type ExpectedDate");
        }

        public static readonly ExpectedDateConverter Singleton = new ExpectedDateConverter();
    }

    internal class StatusConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Status) || t == typeof(Status?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    return new Status { String = stringValue };
                case JsonToken.StartArray:
                    var arrayValue = serializer.Deserialize<List<dynamic>>(reader);
                    return new Status { StringArray = arrayValue };
            }
            throw new Exception("Cannot unmarshal type Status");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (Status)untypedValue;
            if (value.String != null)
            {
                serializer.Serialize(writer, value.String);
                return;
            }
            if (value.StringArray != null)
            {
                var converter = DecodeArrayConverter.Singleton;
                converter.WriteJson(writer, value.StringArray, serializer);
                return;
            }
            throw new Exception("Cannot marshal type Status");
        }

        public static readonly StatusConverter Singleton = new StatusConverter();
    }

    internal class DecodeArrayConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(List<long>);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            reader.Read();
            var value = new List<long>();
            while (reader.TokenType != JsonToken.EndArray)
            {
                var converter = ParseStringConverter.Singleton;
                var arrayItem = (long)converter.ReadJson(reader, typeof(long), null, serializer);
                value.Add(arrayItem);
                reader.Read();
            }
            return value;
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (List<long>)untypedValue;
            writer.WriteStartArray();
            foreach (var arrayItem in value)
            {
                var converter = ParseStringConverter.Singleton;
                converter.WriteJson(writer, arrayItem, serializer);
            }
            writer.WriteEndArray();
            return;
        }

        public static readonly DecodeArrayConverter Singleton = new DecodeArrayConverter();
    }
}
