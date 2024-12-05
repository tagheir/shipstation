using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShipStationApi.Models
{
    public partial class ShipStationStoreInfoDto
    {
        [JsonProperty("storeId", NullValueHandling = NullValueHandling.Ignore)]
        public long? StoreId { get; set; }

        [JsonProperty("storeName", NullValueHandling = NullValueHandling.Ignore)]
        public string StoreName { get; set; }

        [JsonProperty("marketplaceId", NullValueHandling = NullValueHandling.Ignore)]
        public long? MarketplaceId { get; set; }

        [JsonProperty("marketplaceName", NullValueHandling = NullValueHandling.Ignore)]
        public string MarketplaceName { get; set; }

        [JsonProperty("accountName")]
        public object AccountName { get; set; }

        [JsonProperty("email")]
        public object Email { get; set; }

        [JsonProperty("integrationUrl")]
        public Uri IntegrationUrl { get; set; }

        [JsonProperty("active", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Active { get; set; }

        [JsonProperty("companyName", NullValueHandling = NullValueHandling.Ignore)]
        public string CompanyName { get; set; }

        [JsonProperty("phone", NullValueHandling = NullValueHandling.Ignore)]
        public string Phone { get; set; }

        [JsonProperty("publicEmail", NullValueHandling = NullValueHandling.Ignore)]
        public string PublicEmail { get; set; }

        [JsonProperty("website", NullValueHandling = NullValueHandling.Ignore)]
        public string Website { get; set; }

        [JsonProperty("refreshDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? RefreshDate { get; set; }

        [JsonProperty("lastRefreshAttempt", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? LastRefreshAttempt { get; set; }

        [JsonProperty("createDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? CreateDate { get; set; }

        [JsonProperty("modifyDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? ModifyDate { get; set; }

        [JsonProperty("autoRefresh", NullValueHandling = NullValueHandling.Ignore)]
        public bool? AutoRefresh { get; set; }
    }

}
