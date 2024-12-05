using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using OrderPlacer.Bjs.Models;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace OrderPlacer
{
    public static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
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
}
internal class PurpleParseStringConverter : JsonConverter
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

    public static readonly PurpleParseStringConverter Singleton = new PurpleParseStringConverter();
}

internal class GbiCategorConverter : JsonConverter
{
    public override bool CanConvert(Type t) => t == typeof(GbiCategor) || t == typeof(GbiCategor?);

    public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
    {
        switch (reader.TokenType)
        {
            case JsonToken.String:
            case JsonToken.Date:
                var stringValue = serializer.Deserialize<string>(reader);
                return new GbiCategor { String = stringValue };
            case JsonToken.StartArray:
                var arrayValue = serializer.Deserialize<List<object>>(reader);
                return new GbiCategor { AnythingArray = arrayValue };
        }
        throw new Exception("Cannot unmarshal type GbiCategor");
    }

    public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
    {
        var value = (GbiCategor)untypedValue;
        if (value.String != null)
        {
            serializer.Serialize(writer, value.String);
            return;
        }
        if (value.AnythingArray != null)
        {
            serializer.Serialize(writer, value.AnythingArray);
            return;
        }
        throw new Exception("Cannot marshal type GbiCategor");
    }

    public static readonly GbiCategorConverter Singleton = new GbiCategorConverter();
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
            var converter = PurpleParseStringConverter.Singleton;
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
            var converter = PurpleParseStringConverter.Singleton;
            converter.WriteJson(writer, arrayItem, serializer);
        }
        writer.WriteEndArray();
        return;
    }

    public static readonly DecodeArrayConverter Singleton = new DecodeArrayConverter();
}




internal class FluffyParseStringConverter : JsonConverter
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

    public static readonly FluffyParseStringConverter Singleton = new FluffyParseStringConverter();
}


