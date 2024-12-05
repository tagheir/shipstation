﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using OrderPlacer.SamsClub.Models;
//
//    var samsCartResponseDto = SamsCartResponseDto.FromJson(jsonString);

namespace OrderPlacer.SamsClub.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    public partial class SamsCartResponseDto
    {
        [JsonProperty("metadata", NullValueHandling = NullValueHandling.Ignore)]
        public Metadata Metadata { get; set; }

        [JsonProperty("payload", NullValueHandling = NullValueHandling.Ignore)]
        public CartPayload Payload { get; set; }
    }

    public partial class Metadata
    {
        [JsonProperty("clubDetails", NullValueHandling = NullValueHandling.Ignore)]
        public ClubDetails ClubDetails { get; set; }

        [JsonProperty("dfcThresholdAmount", NullValueHandling = NullValueHandling.Ignore)]
        public long? DfcThresholdAmount { get; set; }

        [JsonProperty("itemsRemoved", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> ItemsRemoved { get; set; }
    }

    public partial class ClubDetails
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("sameDayPickup", NullValueHandling = NullValueHandling.Ignore)]
        public bool? SameDayPickup { get; set; }

        [JsonProperty("curbSide", NullValueHandling = NullValueHandling.Ignore)]
        public bool? CurbSide { get; set; }

        [JsonProperty("driveThrough", NullValueHandling = NullValueHandling.Ignore)]
        public bool? DriveThrough { get; set; }

        [JsonProperty("timeZone", NullValueHandling = NullValueHandling.Ignore)]
        public string TimeZone { get; set; }

        [JsonProperty("address", NullValueHandling = NullValueHandling.Ignore)]
        public Address Address { get; set; }
    }

    public partial class Address
    {
        [JsonProperty("addressLine1", NullValueHandling = NullValueHandling.Ignore)]
        public string AddressLine1 { get; set; }

        [JsonProperty("city", NullValueHandling = NullValueHandling.Ignore)]
        public string City { get; set; }

        [JsonProperty("postalCode", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? PostalCode { get; set; }

        [JsonProperty("state", NullValueHandling = NullValueHandling.Ignore)]
        public string State { get; set; }

        [JsonProperty("phone", NullValueHandling = NullValueHandling.Ignore)]
        public string Phone { get; set; }
    }

    public partial class CartPayload
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("clubId", NullValueHandling = NullValueHandling.Ignore)]
        public long? ClubId { get; set; }

        [JsonProperty("customerId", NullValueHandling = NullValueHandling.Ignore)]
        public string CustomerId { get; set; }

        [JsonProperty("itemCount", NullValueHandling = NullValueHandling.Ignore)]
        public long? ItemCount { get; set; }

        [JsonProperty("postalCode", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? PostalCode { get; set; }

        [JsonProperty("lastModified", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? LastModified { get; set; }

        [JsonProperty("lastUsedChannel", NullValueHandling = NullValueHandling.Ignore)]
        public string LastUsedChannel { get; set; }

        [JsonProperty("lineItems", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, LineItems> LineItems { get; set; }

        [JsonProperty("totals", NullValueHandling = NullValueHandling.Ignore)]
        public Totals Totals { get; set; }

        [JsonProperty("signInRequired", NullValueHandling = NullValueHandling.Ignore)]
        public bool? SignInRequired { get; set; }

        [JsonProperty("nonEligiblePromotions", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> NonEligiblePromotions { get; set; }

        [JsonProperty("eligiblePromotions", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> EligiblePromotions { get; set; }

        [JsonProperty("multiChannelInstantSavingsItem", NullValueHandling = NullValueHandling.Ignore)]
        public bool? MultiChannelInstantSavingsItem { get; set; }

        [JsonProperty("cartType", NullValueHandling = NullValueHandling.Ignore)]
        public string CartType { get; set; }

        [JsonProperty("astraCheckoutable", NullValueHandling = NullValueHandling.Ignore)]
        public bool? AstraCheckoutable { get; set; }

        [JsonProperty("astraCheckoutablePhase", NullValueHandling = NullValueHandling.Ignore)]
        public string AstraCheckoutablePhase { get; set; }

        [JsonProperty("exclusionItemPresent", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ExclusionItemPresent { get; set; }
    }

    public partial class LineItems
    {


        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("productInfo", NullValueHandling = NullValueHandling.Ignore)]
        public ProductInfo ProductInfo { get; set; }

        [JsonProperty("quantity", NullValueHandling = NullValueHandling.Ignore)]
        public long? Quantity { get; set; }

        [JsonProperty("channel", NullValueHandling = NullValueHandling.Ignore)]
        public string Channel { get; set; }

        [JsonProperty("fulfillmentType", NullValueHandling = NullValueHandling.Ignore)]
        public string FulfillmentType { get; set; }

        [JsonProperty("qtyEditable", NullValueHandling = NullValueHandling.Ignore)]
        public bool? QtyEditable { get; set; }

        [JsonProperty("itemRemovableFromCart", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ItemRemovableFromCart { get; set; }

        [JsonProperty("priceInfo", NullValueHandling = NullValueHandling.Ignore)]
        public PriceInfo PriceInfo { get; set; }

        [JsonProperty("isGWP", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsGwp { get; set; }

        [JsonProperty("signInRequired", NullValueHandling = NullValueHandling.Ignore)]
        public bool? SignInRequired { get; set; }

        [JsonProperty("lastModifiedTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? LastModifiedTime { get; set; }
    }

    public partial class Li6D2Ead6911774653B2726B712Aa0C640
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("productInfo", NullValueHandling = NullValueHandling.Ignore)]
        public ProductInfo ProductInfo { get; set; }

        [JsonProperty("quantity", NullValueHandling = NullValueHandling.Ignore)]
        public long? Quantity { get; set; }

        [JsonProperty("channel", NullValueHandling = NullValueHandling.Ignore)]
        public string Channel { get; set; }

        [JsonProperty("fulfillmentType", NullValueHandling = NullValueHandling.Ignore)]
        public string FulfillmentType { get; set; }

        [JsonProperty("qtyEditable", NullValueHandling = NullValueHandling.Ignore)]
        public bool? QtyEditable { get; set; }

        [JsonProperty("itemRemovableFromCart", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ItemRemovableFromCart { get; set; }

        [JsonProperty("priceInfo", NullValueHandling = NullValueHandling.Ignore)]
        public PriceInfo PriceInfo { get; set; }

        [JsonProperty("isGWP", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsGwp { get; set; }

        [JsonProperty("signInRequired", NullValueHandling = NullValueHandling.Ignore)]
        public bool? SignInRequired { get; set; }

        [JsonProperty("lastModifiedTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? LastModifiedTime { get; set; }
    }

    public partial class PriceInfo
    {
        [JsonProperty("listPrice", NullValueHandling = NullValueHandling.Ignore)]
        public double? ListPrice { get; set; }

        [JsonProperty("finalPrice", NullValueHandling = NullValueHandling.Ignore)]
        public double? FinalPrice { get; set; }

        [JsonProperty("unitPrice", NullValueHandling = NullValueHandling.Ignore)]
        public double? UnitPrice { get; set; }

        [JsonProperty("itemTotal", NullValueHandling = NullValueHandling.Ignore)]
        public long? ItemTotal { get; set; }

        [JsonProperty("fulfillmentCharges", NullValueHandling = NullValueHandling.Ignore)]
        public long? FulfillmentCharges { get; set; }

        [JsonProperty("discountInfo", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> DiscountInfo { get; set; }
    }

    public partial class ProductInfo
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("upc", NullValueHandling = NullValueHandling.Ignore)]
        public string Upc { get; set; }

        [JsonProperty("gtin", NullValueHandling = NullValueHandling.Ignore)]
        public string Gtin { get; set; }

        [JsonProperty("productId", NullValueHandling = NullValueHandling.Ignore)]
        public string ProductId { get; set; }

        [JsonProperty("skuId", NullValueHandling = NullValueHandling.Ignore)]
        public string SkuId { get; set; }

        [JsonProperty("itemNumber", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? ItemNumber { get; set; }

        [JsonProperty("mdsFamId", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? MdsFamId { get; set; }

        [JsonProperty("imageName", NullValueHandling = NullValueHandling.Ignore)]
        public string ImageName { get; set; }

        [JsonProperty("isWeightedItem", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsWeightedItem { get; set; }

        [JsonProperty("shippingFlag", NullValueHandling = NullValueHandling.Ignore)]
        public string ShippingFlag { get; set; }

        [JsonProperty("subscriptionEligible", NullValueHandling = NullValueHandling.Ignore)]
        public bool? SubscriptionEligible { get; set; }

        [JsonProperty("eligibleChannels", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> EligibleChannels { get; set; }

        [JsonProperty("multiChannel", NullValueHandling = NullValueHandling.Ignore)]
        public bool? MultiChannel { get; set; }

        [JsonProperty("stockLevel", NullValueHandling = NullValueHandling.Ignore)]
        public string StockLevel { get; set; }

        [JsonProperty("minQty", NullValueHandling = NullValueHandling.Ignore)]
        public long? MinQty { get; set; }

        [JsonProperty("maxQty", NullValueHandling = NullValueHandling.Ignore)]
        public long? MaxQty { get; set; }

        [JsonProperty("evalueEligible", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EvalueEligible { get; set; }

        [JsonProperty("isGiftMessageEligible", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsGiftMessageEligible { get; set; }

        [JsonProperty("productType", NullValueHandling = NullValueHandling.Ignore)]
        public string ProductType { get; set; }

        [JsonProperty("isMandatoryPostPaid", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsMandatoryPostPaid { get; set; }

        [JsonProperty("isInstantSavingsItem", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsInstantSavingsItem { get; set; }

        [JsonProperty("vendorSeqNumber", NullValueHandling = NullValueHandling.Ignore)]
        public long? VendorSeqNumber { get; set; }

        [JsonProperty("serviceAgreementEligible", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ServiceAgreementEligible { get; set; }

        [JsonProperty("packageInfos", NullValueHandling = NullValueHandling.Ignore)]
        public List<PackageInfo> PackageInfos { get; set; }

        [JsonProperty("fixedCostAmount", NullValueHandling = NullValueHandling.Ignore)]
        public long? FixedCostAmount { get; set; }
    }

    public partial class PackageInfo
    {
        [JsonProperty("weight", NullValueHandling = NullValueHandling.Ignore)]
        public double? Weight { get; set; }

        [JsonProperty("height", NullValueHandling = NullValueHandling.Ignore)]
        public long? Height { get; set; }

        [JsonProperty("length", NullValueHandling = NullValueHandling.Ignore)]
        public long? Length { get; set; }

        [JsonProperty("width", NullValueHandling = NullValueHandling.Ignore)]
        public long? Width { get; set; }

        [JsonProperty("packageDesc", NullValueHandling = NullValueHandling.Ignore)]
        public string PackageDesc { get; set; }

        [JsonProperty("weightUnitofMeasure", NullValueHandling = NullValueHandling.Ignore)]
        public string WeightUnitofMeasure { get; set; }

        [JsonProperty("lengthUnitOfMeasure", NullValueHandling = NullValueHandling.Ignore)]
        public string LengthUnitOfMeasure { get; set; }

        [JsonProperty("packageType", NullValueHandling = NullValueHandling.Ignore)]
        public string PackageType { get; set; }
    }

    public partial class Totals
    {
        [JsonProperty("subtotal", NullValueHandling = NullValueHandling.Ignore)]
        public long? Subtotal { get; set; }

        [JsonProperty("estimatedTotalSavings", NullValueHandling = NullValueHandling.Ignore)]
        public long? EstimatedTotalSavings { get; set; }

        [JsonProperty("orderTotalAmount", NullValueHandling = NullValueHandling.Ignore)]
        public double? OrderTotalAmount { get; set; }

        [JsonProperty("dfcSubTotal", NullValueHandling = NullValueHandling.Ignore)]
        public long? DfcSubTotal { get; set; }

        [JsonProperty("shippingAmount", NullValueHandling = NullValueHandling.Ignore)]
        public long? ShippingAmount { get; set; }

        [JsonProperty("shippingSurcharges", NullValueHandling = NullValueHandling.Ignore)]
        public long? ShippingSurcharges { get; set; }

        [JsonProperty("pickupAmount", NullValueHandling = NullValueHandling.Ignore)]
        public long? PickupAmount { get; set; }

        [JsonProperty("finalPickupAmount", NullValueHandling = NullValueHandling.Ignore)]
        public long? FinalPickupAmount { get; set; }

        [JsonProperty("deliveryAmount", NullValueHandling = NullValueHandling.Ignore)]
        public long? DeliveryAmount { get; set; }

        [JsonProperty("finalDeliveryAmount", NullValueHandling = NullValueHandling.Ignore)]
        public long? FinalDeliveryAmount { get; set; }

        [JsonProperty("surchargeAmount", NullValueHandling = NullValueHandling.Ignore)]
        public long? SurchargeAmount { get; set; }

        [JsonProperty("totalProductTax", NullValueHandling = NullValueHandling.Ignore)]
        public long? TotalProductTax { get; set; }

        [JsonProperty("orderTotalAmountBeforeSavings", NullValueHandling = NullValueHandling.Ignore)]
        public long? OrderTotalAmountBeforeSavings { get; set; }

        [JsonProperty("discountBreakup", NullValueHandling = NullValueHandling.Ignore)]
        public DiscountBreakup DiscountBreakup { get; set; }
    }

    public partial class DiscountBreakup
    {
    }

    public partial class SamsCartResponseDto
    {
        public static SamsCartResponseDto FromJson(string json) => JsonConvert.DeserializeObject<SamsCartResponseDto>(json, Converter.Settings);
    }




}