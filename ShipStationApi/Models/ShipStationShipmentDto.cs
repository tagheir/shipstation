using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace ShipStationApi.Models
{
   

    public partial class ShipStationShipmentDto
    {
        [JsonProperty("shipments", NullValueHandling = NullValueHandling.Ignore)]
        public List<Shipment> Shipments { get; set; }

        [JsonProperty("total", NullValueHandling = NullValueHandling.Ignore)]
        public long? Total { get; set; }

        [JsonProperty("page", NullValueHandling = NullValueHandling.Ignore)]
        public long? Page { get; set; }

        [JsonProperty("pages", NullValueHandling = NullValueHandling.Ignore)]
        public long? Pages { get; set; }
    }

    public partial class Shipment
    {
        [JsonProperty("shipmentId", NullValueHandling = NullValueHandling.Ignore)]
        public long? ShipmentId { get; set; }

        [JsonProperty("orderId", NullValueHandling = NullValueHandling.Ignore)]
        public long? OrderId { get; set; }

        [JsonProperty("orderKey", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderKey { get; set; }

        [JsonProperty("userId", NullValueHandling = NullValueHandling.Ignore)]
        public string UserId { get; set; }

        [JsonProperty("customerEmail", NullValueHandling = NullValueHandling.Ignore)]
        public string CustomerEmail { get; set; }

        [JsonProperty("orderNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderNumber { get; set; }

        [JsonProperty("createDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? CreateDate { get; set; }

        [JsonProperty("shipDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? ShipDate { get; set; }

        [JsonProperty("shipmentCost", NullValueHandling = NullValueHandling.Ignore)]
        public double? ShipmentCost { get; set; }

        [JsonProperty("insuranceCost", NullValueHandling = NullValueHandling.Ignore)]
        public long? InsuranceCost { get; set; }

        [JsonProperty("trackingNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string TrackingNumber { get; set; }

        [JsonProperty("isReturnLabel", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsReturnLabel { get; set; }

        [JsonProperty("batchNumber")]
        public string BatchNumber { get; set; }

        [JsonProperty("carrierCode", NullValueHandling = NullValueHandling.Ignore)]
        public string CarrierCode { get; set; }

        [JsonProperty("serviceCode", NullValueHandling = NullValueHandling.Ignore)]
        public string ServiceCode { get; set; }

        [JsonProperty("packageCode", NullValueHandling = NullValueHandling.Ignore)]
        public string PackageCode { get; set; }

        [JsonProperty("confirmation")]
        public string Confirmation { get; set; }

        [JsonProperty("warehouseId", NullValueHandling = NullValueHandling.Ignore)]
        public long? WarehouseId { get; set; }

        [JsonProperty("voided", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Voided { get; set; }

        [JsonProperty("voidDate")]
        public DateTimeOffset? VoidDate { get; set; }

        [JsonProperty("marketplaceNotified", NullValueHandling = NullValueHandling.Ignore)]
        public bool? MarketplaceNotified { get; set; }

        [JsonProperty("notifyErrorMessage")]
        public object NotifyErrorMessage { get; set; }

        [JsonProperty("shipTo", NullValueHandling = NullValueHandling.Ignore)]
        public ShipTo ShipTo { get; set; }

        [JsonProperty("weight", NullValueHandling = NullValueHandling.Ignore)]
        public Weight Weight { get; set; }

        [JsonProperty("dimensions")]
        public Dimensions Dimensions { get; set; }

        [JsonProperty("insuranceOptions", NullValueHandling = NullValueHandling.Ignore)]
        public InsuranceOptions InsuranceOptions { get; set; }

        [JsonProperty("advancedOptions", NullValueHandling = NullValueHandling.Ignore)]
        public AdvancedOptions AdvancedOptions { get; set; }

        [JsonProperty("shipmentItems")]
        public object ShipmentItems { get; set; }

        [JsonProperty("labelData")]
        public object LabelData { get; set; }

        [JsonProperty("formData")]
        public object FormData { get; set; }
    }

    

    

    

    public partial class ShipTo
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("company")]
        public string Company { get; set; }

        [JsonProperty("street1", NullValueHandling = NullValueHandling.Ignore)]
        public string Street1 { get; set; }

        [JsonProperty("street2", NullValueHandling = NullValueHandling.Ignore)]
        public string Street2 { get; set; }

        [JsonProperty("street3")]
        public object Street3 { get; set; }

        [JsonProperty("city", NullValueHandling = NullValueHandling.Ignore)]
        public string City { get; set; }

        [JsonProperty("state", NullValueHandling = NullValueHandling.Ignore)]
        public string State { get; set; }

        [JsonProperty("postalCode", NullValueHandling = NullValueHandling.Ignore)]
        public string PostalCode { get; set; }

        [JsonProperty("country", NullValueHandling = NullValueHandling.Ignore)]
        public string Country { get; set; }

        [JsonProperty("phone", NullValueHandling = NullValueHandling.Ignore)]
        public string Phone { get; set; }

        [JsonProperty("residential")]
        public object Residential { get; set; }

        [JsonProperty("addressVerified")]
        public object AddressVerified { get; set; }
    }

    

    public partial class ShipStationShipmentDto
    {
        public static ShipStationShipmentDto FromJson(string json) => JsonConvert.DeserializeObject<ShipStationShipmentDto>(json, Converter.Settings);
    }
}
