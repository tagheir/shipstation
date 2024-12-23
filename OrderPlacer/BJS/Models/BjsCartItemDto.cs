﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using OrderPlacer.Bjs.Models;
//
//    var bjsCartResponseDto = BjsCartResponseDto.FromJson(jsonString);

namespace OrderPlacer.Bjs.Models
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class BjsCartResponseDto
    {
        [JsonProperty("BJsOrderItemDisplayOutputMember", NullValueHandling = NullValueHandling.Ignore)]
        public BJsOrderItemDisplayOutputMember BJsOrderItemDisplayOutputMember { get; set; }

        [JsonProperty("RAOrderDataBeanOutput", NullValueHandling = NullValueHandling.Ignore)]
        public RaOrderDataBeanOutput RaOrderDataBeanOutput { get; set; }

        [JsonProperty("couponDetails", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> CouponDetails { get; set; }
    }

    public partial class BJsOrderItemDisplayOutputMember
    {
        [JsonProperty("orderId", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(DecodeArrayConverter))]
        public List<long> OrderId { get; set; }

        [JsonProperty("zipcode", NullValueHandling = NullValueHandling.Ignore)]
        
        public long? Zipcode { get; set; }

        [JsonProperty("continueURL", NullValueHandling = NullValueHandling.Ignore)]
        public string ContinueUrl { get; set; }

        [JsonProperty("emptyCart", NullValueHandling = NullValueHandling.Ignore)]
        
        public bool? EmptyCart { get; set; }

        [JsonProperty("viewPage", NullValueHandling = NullValueHandling.Ignore)]
        public string ViewPage { get; set; }
    }

    public partial class RaOrderDataBeanOutput
    {
        [JsonProperty("msg", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> Msg { get; set; }

        [JsonProperty("orderItemsWithInvalidPrice", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> OrderItemsWithInvalidPrice { get; set; }

        [JsonProperty("customOrderItems", NullValueHandling = NullValueHandling.Ignore)]
        public CustomOrderItems CustomOrderItems { get; set; }

        [JsonProperty("orderExtendAttribute", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> OrderExtendAttribute { get; set; }

        [JsonProperty("orderLevelPromotion", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> OrderLevelPromotion { get; set; }

        [JsonProperty("appliedPromoCodes", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> AppliedPromoCodes { get; set; }

        [JsonProperty("grandTotal", NullValueHandling = NullValueHandling.Ignore)]
        public double? GrandTotal { get; set; }

        [JsonProperty("totalTax", NullValueHandling = NullValueHandling.Ignore)]
        public double? TotalTax { get; set; }

        [JsonProperty("formattedShippingAdjustment", NullValueHandling = NullValueHandling.Ignore)]
        public long? FormattedShippingAdjustment { get; set; }

        [JsonProperty("formattedShippingCharge", NullValueHandling = NullValueHandling.Ignore)]
        public double? FormattedShippingCharge { get; set; }

        [JsonProperty("specialMemberType", NullValueHandling = NullValueHandling.Ignore)]
        public string SpecialMemberType { get; set; }

        [JsonProperty("freeShipOrderTotal", NullValueHandling = NullValueHandling.Ignore)]
        public long? FreeShipOrderTotal { get; set; }

        [JsonProperty("totalQuantityInOrder", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(FluffyParseStringConverter))]
        public long? TotalQuantityInOrder { get; set; }

        [JsonProperty("allowTiresPurchase", NullValueHandling = NullValueHandling.Ignore)]
        public string AllowTiresPurchase { get; set; }

        [JsonProperty("freeShippingThreshold", NullValueHandling = NullValueHandling.Ignore)]
        public long? FreeShippingThreshold { get; set; }

        [JsonProperty("allFreeShippingItems", NullValueHandling = NullValueHandling.Ignore)]
        public bool? AllFreeShippingItems { get; set; }

        [JsonProperty("allShippingIncludedItems", NullValueHandling = NullValueHandling.Ignore)]
        public bool? AllShippingIncludedItems { get; set; }

        [JsonProperty("orderHasValidMemSKU", NullValueHandling = NullValueHandling.Ignore)]
        public bool? OrderHasValidMemSku { get; set; }

        [JsonProperty("formattedTotalProductPrice", NullValueHandling = NullValueHandling.Ignore)]
        public double? FormattedTotalProductPrice { get; set; }

        [JsonProperty("orderId", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(FluffyParseStringConverter))]
        public long? OrderId { get; set; }

        [JsonProperty("totalShippingTax", NullValueHandling = NullValueHandling.Ignore)]
        public double? TotalShippingTax { get; set; }

        [JsonProperty("PayPalDetails", NullValueHandling = NullValueHandling.Ignore)]
        public PayPalDetails PayPalDetails { get; set; }

        [JsonProperty("surchargeAdjustmentAmout", NullValueHandling = NullValueHandling.Ignore)]
        public long? SurchargeAdjustmentAmout { get; set; }

        [JsonProperty("fees", NullValueHandling = NullValueHandling.Ignore)]
        public long? Fees { get; set; }

        [JsonProperty("isPayPalSelected", NullValueHandling = NullValueHandling.Ignore)]
        public string IsPayPalSelected { get; set; }

        [JsonProperty("isGuestCheckOutEnabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsGuestCheckOutEnabled { get; set; }

        [JsonProperty("awardAmount", NullValueHandling = NullValueHandling.Ignore)]
        public long? AwardAmount { get; set; }

        [JsonProperty("extraGiftCard", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ExtraGiftCard { get; set; }

        [JsonProperty("InvalidPrice", NullValueHandling = NullValueHandling.Ignore)]
        
        public bool? InvalidPrice { get; set; }

        [JsonProperty("isMarketCodePresent", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsMarketCodePresent { get; set; }

        [JsonProperty("isApplyButtonEnabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsApplyButtonEnabled { get; set; }

        [JsonProperty("isExpressCheckoutEnabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsExpressCheckoutEnabled { get; set; }

        [JsonProperty("cobrandPriceOff", NullValueHandling = NullValueHandling.Ignore)]
        public long? CobrandPriceOff { get; set; }

        [JsonProperty("orderAdjustment", NullValueHandling = NullValueHandling.Ignore)]
        public long? OrderAdjustment { get; set; }

        [JsonProperty("isApplianceItemPresent", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsApplianceItemPresent { get; set; }

        [JsonProperty("totalAmtForInstSavings", NullValueHandling = NullValueHandling.Ignore)]
        public long? TotalAmtForInstSavings { get; set; }

        [JsonProperty("bopicLocationToItemCountMap", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> BopicLocationToItemCountMap { get; set; }

        [JsonProperty("bopicCount", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(FluffyParseStringConverter))]
        public long? BopicCount { get; set; }

        [JsonProperty("onlineCount", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(FluffyParseStringConverter))]
        public long? OnlineCount { get; set; }

        [JsonProperty("tireCount", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(FluffyParseStringConverter))]
        public long? TireCount { get; set; }

        [JsonProperty("membershipCount", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(FluffyParseStringConverter))]
        public long? MembershipCount { get; set; }

        [JsonProperty("appliedDriverTip", NullValueHandling = NullValueHandling.Ignore)]
        public string AppliedDriverTip { get; set; }

        [JsonProperty("SDDProductTotal", NullValueHandling = NullValueHandling.Ignore)]
        public string SddProductTotal { get; set; }

        [JsonProperty("sddCount", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(FluffyParseStringConverter))]
        public long? SddCount { get; set; }

        [JsonProperty("sddDeliveryFeeAmount", NullValueHandling = NullValueHandling.Ignore)]
        public long? SddDeliveryFeeAmount { get; set; }

        [JsonProperty("subscriptionItemCount", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(FluffyParseStringConverter))]
        public long? SubscriptionItemCount { get; set; }

        [JsonProperty("majorApplianceCount", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(FluffyParseStringConverter))]
        public long? MajorApplianceCount { get; set; }
    }

    public partial class CustomOrderItems
    {
        [JsonProperty("BOPIC", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> Bopic { get; set; }

        [JsonProperty("Online", NullValueHandling = NullValueHandling.Ignore)]
        public List<Online> Online { get; set; }

        [JsonProperty("SDD", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> Sdd { get; set; }

        [JsonProperty("Membership", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> Membership { get; set; }

        [JsonProperty("Tires", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> Tires { get; set; }

        [JsonProperty("DC", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> Dc { get; set; }

        [JsonProperty("MajorAppliance", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> MajorAppliance { get; set; }
    }

    public partial class Online
    {
        [JsonProperty("orderItem", NullValueHandling = NullValueHandling.Ignore)]
        public OrderItem OrderItem { get; set; }
    }

    public partial class OrderItem
    {
        [JsonProperty("appliedProductPromotions", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> AppliedProductPromotions { get; set; }

        [JsonProperty("immediateParentName", NullValueHandling = NullValueHandling.Ignore)]
        public string ImmediateParentName { get; set; }

        [JsonProperty("shippingOptionsVO", NullValueHandling = NullValueHandling.Ignore)]
        public ShippingOptionsVo ShippingOptionsVo { get; set; }

        [JsonProperty("bopicClubLocation", NullValueHandling = NullValueHandling.Ignore)]
        public BopicClubLocation BopicClubLocation { get; set; }

        [JsonProperty("manufacturerName", NullValueHandling = NullValueHandling.Ignore)]
        public string ManufacturerName { get; set; }

        [JsonProperty("membershipAddress")]
        public object MembershipAddress { get; set; }

        [JsonProperty("associatedPromotions")]
        public object AssociatedPromotions { get; set; }

        [JsonProperty("FormattedProductTotalAmount", NullValueHandling = NullValueHandling.Ignore)]
        public double? FormattedProductTotalAmount { get; set; }

        [JsonProperty("productPartNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string ProductPartNumber { get; set; }

        [JsonProperty("promoProduct", NullValueHandling = NullValueHandling.Ignore)]
        public string PromoProduct { get; set; }

        [JsonProperty("displayPrice", NullValueHandling = NullValueHandling.Ignore)]
        public double? DisplayPrice { get; set; }

        [JsonProperty("lineItemType")]
        public object LineItemType { get; set; }

        [JsonProperty("AutoRenewalChoice")]
        public object AutoRenewalChoice { get; set; }

        [JsonProperty("fulfillmentFrequencyUOM", NullValueHandling = NullValueHandling.Ignore)]
        public string FulfillmentFrequencyUom { get; set; }

        [JsonProperty("totalProductPrice", NullValueHandling = NullValueHandling.Ignore)]
        public double? TotalProductPrice { get; set; }

        [JsonProperty("giftCardReceipentDetails", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> GiftCardReceipentDetails { get; set; }

        [JsonProperty("orderItemsExt")]
        public object OrderItemsExt { get; set; }

        [JsonProperty("shipModeCode", NullValueHandling = NullValueHandling.Ignore)]
        public string ShipModeCode { get; set; }

        [JsonProperty("fulfillmentFrequency", NullValueHandling = NullValueHandling.Ignore)]
        public string FulfillmentFrequency { get; set; }

        [JsonProperty("orderItemId", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(FluffyParseStringConverter))]
        public long? OrderItemId { get; set; }

        [JsonProperty("eachPrice", NullValueHandling = NullValueHandling.Ignore)]
        public double? EachPrice { get; set; }

        [JsonProperty("productAttributes", NullValueHandling = NullValueHandling.Ignore)]
        public ProductAttributes ProductAttributes { get; set; }

        [JsonProperty("formattedQuantity", NullValueHandling = NullValueHandling.Ignore)]
        public long? FormattedQuantity { get; set; }

        [JsonProperty("isRopicClub", NullValueHandling = NullValueHandling.Ignore)]
        public string IsRopicClub { get; set; }

        [JsonProperty("offerMaxQty")]
        public object OfferMaxQty { get; set; }

        [JsonProperty("catalogEntryIdentifier", NullValueHandling = NullValueHandling.Ignore)]
        public CatalogEntryIdentifier CatalogEntryIdentifier { get; set; }

        [JsonProperty("addressDataBean", NullValueHandling = NullValueHandling.Ignore)]
        public AddressDataBean AddressDataBean { get; set; }

        [JsonProperty("bopicStandardPrice")]
        public object BopicStandardPrice { get; set; }

        [JsonProperty("adjAmountForInstSavings", NullValueHandling = NullValueHandling.Ignore)]
        public long? AdjAmountForInstSavings { get; set; }

        [JsonProperty("catalogEntryDataBean", NullValueHandling = NullValueHandling.Ignore)]
        public CatalogEntryDataBean CatalogEntryDataBean { get; set; }

        [JsonProperty("strikePrice", NullValueHandling = NullValueHandling.Ignore)]
        public long? StrikePrice { get; set; }

        [JsonProperty("itemDataBean", NullValueHandling = NullValueHandling.Ignore)]
        public ItemDataBean ItemDataBean { get; set; }

        [JsonProperty("couponDetails", NullValueHandling = NullValueHandling.Ignore)]
        public CouponDetails CouponDetails { get; set; }

        [JsonProperty("OrderItemInventory", NullValueHandling = NullValueHandling.Ignore)]
        public OrderItemInventory OrderItemInventory { get; set; }
    }

    public partial class AddressDataBean
    {
        [JsonProperty("zipCode", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(FluffyParseStringConverter))]
        public long? ZipCode { get; set; }
    }

    public partial class BopicClubLocation
    {
        [JsonProperty("clubCity", NullValueHandling = NullValueHandling.Ignore)]
        public string ClubCity { get; set; }

        [JsonProperty("clubStoreHours", NullValueHandling = NullValueHandling.Ignore)]
        public string ClubStoreHours { get; set; }

        [JsonProperty("clubName", NullValueHandling = NullValueHandling.Ignore)]
        public string ClubName { get; set; }

        [JsonProperty("clubAddress", NullValueHandling = NullValueHandling.Ignore)]
        public string ClubAddress { get; set; }

        [JsonProperty("clubId", NullValueHandling = NullValueHandling.Ignore)]
        public long? ClubId { get; set; }

        [JsonProperty("clubZip", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(FluffyParseStringConverter))]
        public long? ClubZip { get; set; }

        [JsonProperty("clubPhone", NullValueHandling = NullValueHandling.Ignore)]
        public string ClubPhone { get; set; }

        [JsonProperty("clubState", NullValueHandling = NullValueHandling.Ignore)]
        public string ClubState { get; set; }
    }

    public partial class CatalogEntryDataBean
    {
        [JsonProperty("item", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Item { get; set; }

        [JsonProperty("offerPrice", NullValueHandling = NullValueHandling.Ignore)]
        public Price OfferPrice { get; set; }

        [JsonProperty("package", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Package { get; set; }

        [JsonProperty("packageDataBean")]
        public object PackageDataBean { get; set; }
    }

    public partial class Price
    {
        [JsonProperty("amount", NullValueHandling = NullValueHandling.Ignore)]
        public double? Amount { get; set; }
    }

    public partial class CatalogEntryIdentifier
    {
        [JsonProperty("uniqueID", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(FluffyParseStringConverter))]
        public long? UniqueId { get; set; }
    }

    public partial class CouponDetails
    {
        [JsonProperty("availableOffers", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> AvailableOffers { get; set; }

        [JsonProperty("gtin", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> Gtin { get; set; }

        [JsonProperty("articleId", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(FluffyParseStringConverter))]
        public long? ArticleId { get; set; }

        [JsonProperty("activatedOffers", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> ActivatedOffers { get; set; }
    }

    public partial class ItemDataBean
    {
        [JsonProperty("giftCardDescription", NullValueHandling = NullValueHandling.Ignore)]
        public GiftCardDescription GiftCardDescription { get; set; }

        [JsonProperty("isSubscriptionEligible", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsSubscriptionEligible { get; set; }

        [JsonProperty("maxPackWeight", NullValueHandling = NullValueHandling.Ignore)]
        public string MaxPackWeight { get; set; }

        [JsonProperty("membershipItemType")]
        public object MembershipItemType { get; set; }

        [JsonProperty("MinQuantity")]
        public object MinQuantity { get; set; }

        [JsonProperty("catEntryId", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(FluffyParseStringConverter))]
        public long? CatEntryId { get; set; }

        [JsonProperty("isRewards")]
        public object IsRewards { get; set; }

        [JsonProperty("parentProductDataBean", NullValueHandling = NullValueHandling.Ignore)]
        public ParentProductDataBean ParentProductDataBean { get; set; }

        [JsonProperty("membershipFlowType")]
        public object MembershipFlowType { get; set; }

        [JsonProperty("mapPrice", NullValueHandling = NullValueHandling.Ignore)]
        public Price MapPrice { get; set; }

        [JsonProperty("productUOM", NullValueHandling = NullValueHandling.Ignore)]
        public string ProductUom { get; set; }

        [JsonProperty("MaxQuantity", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(FluffyParseStringConverter))]
        public long? MaxQuantity { get; set; }

        [JsonProperty("isSDDEligible", NullValueHandling = NullValueHandling.Ignore)]
        public string IsSddEligible { get; set; }

        [JsonProperty("thumbnailImage", NullValueHandling = NullValueHandling.Ignore)]
        public Uri ThumbnailImage { get; set; }

        [JsonProperty("InstantSavings", NullValueHandling = NullValueHandling.Ignore)]
        public bool? InstantSavings { get; set; }

        [JsonProperty("isAvailInClub", NullValueHandling = NullValueHandling.Ignore)]
        public string IsAvailInClub { get; set; }

        [JsonProperty("gtin", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Gtin { get; set; }

        [JsonProperty("parentProductId", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(FluffyParseStringConverter))]
        public long? ParentProductId { get; set; }

        [JsonProperty("isAvailOnline", NullValueHandling = NullValueHandling.Ignore)]
        public string IsAvailOnline { get; set; }

        [JsonProperty("itemStandardPrice", NullValueHandling = NullValueHandling.Ignore)]
        public Price ItemStandardPrice { get; set; }

        [JsonProperty("minPackWeight", NullValueHandling = NullValueHandling.Ignore)]
        public string MinPackWeight { get; set; }

        [JsonProperty("isBopicable", NullValueHandling = NullValueHandling.Ignore)]
        public string IsBopicable { get; set; }

  

        [JsonProperty("ItemStandardPriceAmount")]
        public object ItemStandardPriceAmount { get; set; }

        [JsonProperty("onSpecial", NullValueHandling = NullValueHandling.Ignore)]
       
        public long? OnSpecial { get; set; }

        [JsonProperty("partNumber", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(FluffyParseStringConverter))]
        public long? PartNumber { get; set; }

        [JsonProperty("ItemMapPriceAmount")]
        public object ItemMapPriceAmount { get; set; }

        [JsonProperty("definingAttributeValueDataBeans", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> DefiningAttributeValueDataBeans { get; set; }

        [JsonProperty("field5", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(FluffyParseStringConverter))]
        public long? Field5 { get; set; }
    }

    public partial class GiftCardDescription
    {
        [JsonProperty("longDescription", NullValueHandling = NullValueHandling.Ignore)]
        public string LongDescription { get; set; }

        [JsonProperty("shortDescription", NullValueHandling = NullValueHandling.Ignore)]
        public string ShortDescription { get; set; }

        [JsonProperty("Name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
    }

    public partial class ParentProductDataBean
    {
        [JsonProperty("buyable", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(FluffyParseStringConverter))]
        public long? Buyable { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public ParentProductDataBeanDescription Description { get; set; }

        [JsonProperty("markForDelete", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(FluffyParseStringConverter))]
        public long? MarkForDelete { get; set; }
    }

    public partial class ParentProductDataBeanDescription
    {
        [JsonProperty("longDescription", NullValueHandling = NullValueHandling.Ignore)]
        public string LongDescription { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("published", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(FluffyParseStringConverter))]
        public long? Published { get; set; }

        [JsonProperty("shortDescription", NullValueHandling = NullValueHandling.Ignore)]
        public string ShortDescription { get; set; }
    }

    public partial class OrderItemInventory
    {
        [JsonProperty("sddInventoryStatus", NullValueHandling = NullValueHandling.Ignore)]
        public string SddInventoryStatus { get; set; }

        [JsonProperty("availableSddInventory", NullValueHandling = NullValueHandling.Ignore)]
        public string AvailableSddInventory { get; set; }

        [JsonProperty("onlineInventoryStatus", NullValueHandling = NullValueHandling.Ignore)]
        public string OnlineInventoryStatus { get; set; }

        [JsonProperty("availableCLUBInventory", NullValueHandling = NullValueHandling.Ignore)]
        public string AvailableClubInventory { get; set; }

        [JsonProperty("availableOnlineInventory", NullValueHandling = NullValueHandling.Ignore)]
        public string AvailableOnlineInventory { get; set; }

        [JsonProperty("clubInventoryStatus", NullValueHandling = NullValueHandling.Ignore)]
        public string ClubInventoryStatus { get; set; }

        [JsonProperty("deliveryDate", NullValueHandling = NullValueHandling.Ignore)]
        public string DeliveryDate { get; set; }

        [JsonProperty("responseCode", NullValueHandling = NullValueHandling.Ignore)]
        public string ResponseCode { get; set; }

        [JsonProperty("responseMsg", NullValueHandling = NullValueHandling.Ignore)]
        public string ResponseMsg { get; set; }
    }

    public partial class ProductAttributes
    {
        [JsonProperty("SFC", NullValueHandling = NullValueHandling.Ignore)]
        public string Sfc { get; set; }

        [JsonProperty("Stocked", NullValueHandling = NullValueHandling.Ignore)]
        public string Stocked { get; set; }

        [JsonProperty("mapType", NullValueHandling = NullValueHandling.Ignore)]
        public string MapType { get; set; }

        [JsonProperty("estimatedDelivery")]
        public object EstimatedDelivery { get; set; }
    }

    public partial class ShippingOptionsVo
    {
        [JsonProperty("currentShipModeDesc")]
        public object CurrentShipModeDesc { get; set; }

        [JsonProperty("isOptions", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsOptions { get; set; }

        [JsonProperty("freeShipping", NullValueHandling = NullValueHandling.Ignore)]
        public bool? FreeShipping { get; set; }

        [JsonProperty("shippingPayeeIncluded", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ShippingPayeeIncluded { get; set; }

        [JsonProperty("shippingIncluded", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ShippingIncluded { get; set; }

        [JsonProperty("BJsShippingHelperDataBean", NullValueHandling = NullValueHandling.Ignore)]
        public BJsShippingHelperDataBean BJsShippingHelperDataBean { get; set; }

        [JsonProperty("text")]
        public object Text { get; set; }

        [JsonProperty("currentShipModeId", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(FluffyParseStringConverter))]
        public long? CurrentShipModeId { get; set; }

        [JsonProperty("estimatedDelivery")]
        public object EstimatedDelivery { get; set; }
    }

    public partial class BJsShippingHelperDataBean
    {
        [JsonProperty("shipModes", NullValueHandling = NullValueHandling.Ignore)]
        public List<ShipMode> ShipModes { get; set; }

        [JsonProperty("shipModesLength", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(FluffyParseStringConverter))]
        public long? ShipModesLength { get; set; }
    }

    public partial class ShipMode
    {
        [JsonProperty("shippingModeId", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(FluffyParseStringConverter))]
        public long? ShippingModeId { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public ShipModeDescription Description { get; set; }
    }

    public partial class ShipModeDescription
    {
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
    }

    public partial class PayPalDetails
    {
        [JsonProperty("SavePaypal", NullValueHandling = NullValueHandling.Ignore)]
        public string SavePaypal { get; set; }
    }

    public partial class BjsCartResponseDto
    {
        public static BjsCartResponseDto FromJson(string json) => JsonConvert.DeserializeObject<BjsCartResponseDto>(json, Converter.Settings);
    }



    

    
}
