﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using ShipStationApi.Models;
//
//    var shipstationtags = Shipstationtags.FromJson(jsonString);

namespace ShipStationApi.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Linq;

    public partial class Shipstationtags
    {
        public static List<Shipstationtags> tags = new List<Shipstationtags>();

        public static Shipstationtags GetTag(string tagId)
        {
            var tagIdd = long.Parse(tagId);
            if (tags != null)
            {

                return tags.FirstOrDefault(f => f.TagId == tagIdd);
            }
            return null;
        }
        [JsonProperty("tagId", NullValueHandling = NullValueHandling.Ignore)]
        public long? TagId { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("color", NullValueHandling = NullValueHandling.Ignore)]
        public string Color { get; set; }
    }

    public partial class Shipstationtags
    {
        public static List<Shipstationtags> FromJson(string json) => JsonConvert.DeserializeObject<List<Shipstationtags>>(json, ShipStationApi.Models.Converter.Settings);
    }


}
