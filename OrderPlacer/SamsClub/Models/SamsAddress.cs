namespace OrderPlacer.SamsClub.Models
{
    using Newtonsoft.Json;

    public partial class SamsAddress
    {


        [JsonProperty("city", NullValueHandling = NullValueHandling.Ignore)]
        public string City { get; set; }

        [JsonProperty("contactFirstName", NullValueHandling = NullValueHandling.Ignore)]
        public string ContactFirstName { get; set; }

        [JsonProperty("contactLastName", NullValueHandling = NullValueHandling.Ignore)]
        public string ContactLastName { get; set; }

        [JsonProperty("contactPhone", NullValueHandling = NullValueHandling.Ignore)]

        public string? ContactPhone { get; set; }

        [JsonProperty("contactPhoneType", NullValueHandling = NullValueHandling.Ignore)]
        public string ContactPhoneType { get; set; }

        [JsonProperty("countryCode", NullValueHandling = NullValueHandling.Ignore)]
        public string CountryCode { get; set; }

        [JsonProperty("isBusiness", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsBusiness { get; set; }

        [JsonProperty("isDefault", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsDefault { get; set; }

        [JsonProperty("isDockDoorPresent", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsDockDoorPresent { get; set; }

        [JsonProperty("lineOne", NullValueHandling = NullValueHandling.Ignore)]
        public string LineOne { get; set; }

        [JsonProperty("lineTwo", NullValueHandling = NullValueHandling.Ignore)]
        public string LineTwo { get; set; }

        [JsonProperty("organizationName", NullValueHandling = NullValueHandling.Ignore)]
        public string OrganizationName { get; set; }

        [JsonProperty("postalCode", NullValueHandling = NullValueHandling.Ignore)]

        public string PostalCode { get; set; }

        [JsonProperty("stateCode", NullValueHandling = NullValueHandling.Ignore)]
        public string StateCode { get; set; }
    }

    public partial class SamsAddress
    {
        public static SamsAddress FromJson(string json) => JsonConvert.DeserializeObject<SamsAddress>(json, Converter.Settings);
    }
}
