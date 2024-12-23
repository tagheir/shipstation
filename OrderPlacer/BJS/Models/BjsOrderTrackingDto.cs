﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using OrderPlacer.Bjs.Models;
//
//    var bjsTrackingOrderDto = BjsTrackingOrderDto.FromJson(jsonString);

namespace OrderPlacer.Bjs.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public partial class BjsTrackingOrderDto
    {
        [JsonProperty("Order", NullValueHandling = NullValueHandling.Ignore)]
        public BjsTrackingOrderDtoOrder Order { get; set; }
    }

    public partial class BjsTrackingOrderDtoOrder
    {
        [JsonProperty("Extn", NullValueHandling = NullValueHandling.Ignore)]
        public OrderExtn Extn { get; set; }



        [JsonProperty("OrderLines", NullValueHandling = NullValueHandling.Ignore)]
        public List<OrderLines> OrderLines { get; set; }

        [JsonProperty("EnterpriseCode", NullValueHandling = NullValueHandling.Ignore)]
        public string EnterpriseCode { get; set; }

        [JsonProperty("EntryType", NullValueHandling = NullValueHandling.Ignore)]
        public string EntryType { get; set; }

        [JsonProperty("IsOrderCancellable", NullValueHandling = NullValueHandling.Ignore)]
        public string IsOrderCancellable { get; set; }

        [JsonProperty("IsReturnable", NullValueHandling = NullValueHandling.Ignore)]
        public string IsReturnable { get; set; }

        [JsonProperty("MinOrderStatus", NullValueHandling = NullValueHandling.Ignore)]
        public string MinOrderStatus { get; set; }

        [JsonProperty("OrderName", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderName { get; set; }

        [JsonProperty("OrderNo", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? OrderNo { get; set; }

        [JsonProperty("OrderType", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderType { get; set; }



        [JsonProperty("OrderDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? OrderDate { get; set; }

        [JsonProperty("OrderCancellationOverride", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderCancellationOverride { get; set; }

        [JsonProperty("CustomerLastName", NullValueHandling = NullValueHandling.Ignore)]
        public string CustomerLastName { get; set; }

        [JsonProperty("CustomerFirstName", NullValueHandling = NullValueHandling.Ignore)]
        public string CustomerFirstName { get; set; }

        [JsonProperty("CustomerZipCode", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? CustomerZipCode { get; set; }

        [JsonProperty("MaxOrderStatus", NullValueHandling = NullValueHandling.Ignore)]
        public string MaxOrderStatus { get; set; }

        [JsonProperty("OrderStatus", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderStatus { get; set; }



        

        [JsonProperty("OrderStatusDesc", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderStatusDesc { get; set; }

        [JsonProperty("TotalShippingCharge", NullValueHandling = NullValueHandling.Ignore)]
        public string TotalShippingCharge { get; set; }

        [JsonProperty("TotalRewards", NullValueHandling = NullValueHandling.Ignore)]
        public string TotalRewards { get; set; }

        [JsonProperty("TotalShippingDiscount", NullValueHandling = NullValueHandling.Ignore)]
        public string TotalShippingDiscount { get; set; }

        [JsonProperty("TotalPromotion", NullValueHandling = NullValueHandling.Ignore)]
        public string TotalPromotion { get; set; }

        [JsonProperty("TotalTax", NullValueHandling = NullValueHandling.Ignore)]
        public string TotalTax { get; set; }

        [JsonProperty("TotalFees", NullValueHandling = NullValueHandling.Ignore)]
        public string TotalFees { get; set; }
    }

    public partial class OrderExtn
    {
        [JsonProperty("ExtnBulkOrderFlag", NullValueHandling = NullValueHandling.Ignore)]
        public string ExtnBulkOrderFlag { get; set; }
    }

    public  class OrderLines
    {
        [JsonProperty("CarrierServiceCode", NullValueHandling = NullValueHandling.Ignore)]
        public string CarrierServiceCode { get; set; }

        [JsonProperty("ISLineCancellable", NullValueHandling = NullValueHandling.Ignore)]
        public string IsLineCancellable { get; set; }

        [JsonProperty("KitCode", NullValueHandling = NullValueHandling.Ignore)]
        public string KitCode { get; set; }

        [JsonProperty("ReturnableQty", NullValueHandling = NullValueHandling.Ignore)]
        public string ReturnableQty { get; set; }

        [JsonProperty("OrderedQty", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderedQty { get; set; }

        [JsonProperty("MaxLineStatus", NullValueHandling = NullValueHandling.Ignore)]
        public string MaxLineStatus { get; set; }


        [JsonProperty("MinLineStatus", NullValueHandling = NullValueHandling.Ignore)]
        public string MinLineStatus { get; set; }



        [JsonProperty("Root", NullValueHandling = NullValueHandling.Ignore)]
        public Root Root { get; set; }



        [JsonProperty("ComputedPrice", NullValueHandling = NullValueHandling.Ignore)]
        public ComputedPrice ComputedPrice { get; set; }

        [JsonProperty("ItemDetails", NullValueHandling = NullValueHandling.Ignore)]
        public ItemDetails ItemDetails { get; set; }



        [JsonProperty("ShipmentLines", NullValueHandling = NullValueHandling.Ignore)]
        public List<ShipmentLine> ShipmentLines { get; set; }

        [JsonProperty("Containers", NullValueHandling = NullValueHandling.Ignore)]
        public List<Container> Containers { get; set; }

        [JsonProperty("ReturnPolicyViolations", NullValueHandling = NullValueHandling.Ignore)]
        public ReturnPolicyViolations ReturnPolicyViolations { get; set; }

        [JsonProperty("SCAC", NullValueHandling = NullValueHandling.Ignore)]
        public string Scac { get; set; }

        [JsonProperty("ISLineCancelEligible", NullValueHandling = NullValueHandling.Ignore)]
        public string IsLineCancelEligible { get; set; }

        [JsonProperty("PrimeLineNo", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? PrimeLineNo { get; set; }

        [JsonProperty("SubLineNo", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? SubLineNo { get; set; }

        [JsonProperty("AllowPartialLineCancellation", NullValueHandling = NullValueHandling.Ignore)]
        public string AllowPartialLineCancellation { get; set; }

        [JsonProperty("LineCharges", NullValueHandling = NullValueHandling.Ignore)]
        public List<LineCharge> LineCharges { get; set; }

        [JsonProperty("LineTaxes", NullValueHandling = NullValueHandling.Ignore)]
        public List<LineTax> LineTaxes { get; set; }

        [JsonProperty("LineStatusDesc", NullValueHandling = NullValueHandling.Ignore)]
        public string LineStatusDesc { get; set; }
    }

    public partial class ComputedPrice
    {
        [JsonProperty("ExtendedPrice", NullValueHandling = NullValueHandling.Ignore)]
        public string ExtendedPrice { get; set; }
    }

    public partial class Container
    {
        [JsonProperty("TrackingNo", NullValueHandling = NullValueHandling.Ignore)]
        public string TrackingNo { get; set; }

        [JsonProperty("TrackingURL", NullValueHandling = NullValueHandling.Ignore)]
        public Uri TrackingUrl { get; set; }
    }

    public partial class OrderLineExtn
    {
        [JsonProperty("ExtnIsMigrated", NullValueHandling = NullValueHandling.Ignore)]
        public string ExtnIsMigrated { get; set; }

        [JsonProperty("ExtnWCSOrderLineReference", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? ExtnWcsOrderLineReference { get; set; }
    }

    public partial class Item
    {


        [JsonProperty("PrimaryInformation", NullValueHandling = NullValueHandling.Ignore)]
        public ItemPrimaryInformation PrimaryInformation { get; set; }

        [JsonProperty("UnitOfMeasure", NullValueHandling = NullValueHandling.Ignore)]
        public string UnitOfMeasure { get; set; }


    }

    public partial class ItemPrimaryInformation
    {
        [JsonProperty("IsReturnable", NullValueHandling = NullValueHandling.Ignore)]
        public string IsReturnable { get; set; }
    }

    public partial class ItemDetails
    {
        [JsonProperty("PrimaryInformation", NullValueHandling = NullValueHandling.Ignore)]
        public ItemDetailsPrimaryInformation PrimaryInformation { get; set; }

        [JsonProperty("ItemID", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? ItemId { get; set; }

        [JsonProperty("AdditionalAttributeList", NullValueHandling = NullValueHandling.Ignore)]
        public List<AdditionalAttributeList> AdditionalAttributeList { get; set; }
    }

    public partial class AdditionalAttributeList
    {
        [JsonProperty("Name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("Value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }
    }

    public partial class ItemDetailsPrimaryInformation
    {
        [JsonProperty("Description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("IsReturnable", NullValueHandling = NullValueHandling.Ignore)]
        public string IsReturnable { get; set; }

        [JsonProperty("MaxReturnWindow", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? MaxReturnWindow { get; set; }

        [JsonProperty("ProductLine", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? ProductLine { get; set; }

        [JsonProperty("ReturnWindow", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? ReturnWindow { get; set; }

        [JsonProperty("ShortDescription", NullValueHandling = NullValueHandling.Ignore)]
        public string ShortDescription { get; set; }

        [JsonProperty("ExtendedDisplayDescription", NullValueHandling = NullValueHandling.Ignore)]
        public string ExtendedDisplayDescription { get; set; }
    }

    public partial class LineCharge
    {
        [JsonProperty("ChargeAmount", NullValueHandling = NullValueHandling.Ignore)]
        public string ChargeAmount { get; set; }

        [JsonProperty("ChargeCategory", NullValueHandling = NullValueHandling.Ignore)]
        public string ChargeCategory { get; set; }
    }

    public partial class LinePriceInfo
    {
        [JsonProperty("LineTotal", NullValueHandling = NullValueHandling.Ignore)]
        public string LineTotal { get; set; }

        [JsonProperty("ListPrice", NullValueHandling = NullValueHandling.Ignore)]
        public string ListPrice { get; set; }

    }

    public partial class LineTax
    {
        [JsonProperty("ChargeCategory", NullValueHandling = NullValueHandling.Ignore)]
        public string ChargeCategory { get; set; }

        [JsonProperty("Tax", NullValueHandling = NullValueHandling.Ignore)]
        public string Tax { get; set; }

        [JsonProperty("TaxName", NullValueHandling = NullValueHandling.Ignore)]
        public string TaxName { get; set; }

        [JsonProperty("TaxPercentage", NullValueHandling = NullValueHandling.Ignore)]
        public string TaxPercentage { get; set; }
    }

    

    public partial class OrderStatus
    {


        [JsonProperty("OrderHeaderKey", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderHeaderKey { get; set; }

        [JsonProperty("OrderLineKey", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderLineKey { get; set; }

        [JsonProperty("ShipNode", NullValueHandling = NullValueHandling.Ignore)]
        public string ShipNode { get; set; }



        [JsonProperty("StatusDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? StatusDate { get; set; }

        [JsonProperty("StatusQty", NullValueHandling = NullValueHandling.Ignore)]
        public string StatusQty { get; set; }

        [JsonProperty("TotalQuantity", NullValueHandling = NullValueHandling.Ignore)]
        public string TotalQuantity { get; set; }
    }

    

    public partial class PersonInfoTo
    {
        [JsonProperty("FirstName", NullValueHandling = NullValueHandling.Ignore)]
        public string FirstName { get; set; }

        [JsonProperty("LastName", NullValueHandling = NullValueHandling.Ignore)]
        public string LastName { get; set; }

        [JsonProperty("AddressLine1", NullValueHandling = NullValueHandling.Ignore)]
        public string AddressLine1 { get; set; }

        [JsonProperty("AddressLine2", NullValueHandling = NullValueHandling.Ignore)]
        public string AddressLine2 { get; set; }

        [JsonProperty("AddressLine3", NullValueHandling = NullValueHandling.Ignore)]
        public string AddressLine3 { get; set; }

        [JsonProperty("State", NullValueHandling = NullValueHandling.Ignore)]
        public string State { get; set; }

        [JsonProperty("ZipCode", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? ZipCode { get; set; }

        [JsonProperty("EMailID", NullValueHandling = NullValueHandling.Ignore)]
        public string EMailId { get; set; }

        [JsonProperty("DayPhone", NullValueHandling = NullValueHandling.Ignore)]
        public string DayPhone { get; set; }

        [JsonProperty("Country", NullValueHandling = NullValueHandling.Ignore)]
        public string Country { get; set; }

        [JsonProperty("City", NullValueHandling = NullValueHandling.Ignore)]
        public string City { get; set; }
    }

    public partial class ReturnPolicyViolations
    {
        [JsonProperty("HasViolations", NullValueHandling = NullValueHandling.Ignore)]
        public string HasViolations { get; set; }
    }

    public partial class Root
    {
        [JsonProperty("Order", NullValueHandling = NullValueHandling.Ignore)]
        public RootOrder Order { get; set; }
    }

    public partial class RootOrder
    {
        [JsonProperty("DocumentType", NullValueHandling = NullValueHandling.Ignore)]
        public string DocumentType { get; set; }

        [JsonProperty("DraftOrderFlag", NullValueHandling = NullValueHandling.Ignore)]
        public string DraftOrderFlag { get; set; }

        [JsonProperty("EnterpriseCode", NullValueHandling = NullValueHandling.Ignore)]
        public string EnterpriseCode { get; set; }

        [JsonProperty("OrderHeaderKey", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderHeaderKey { get; set; }

        [JsonProperty("SellerOrganizationCode", NullValueHandling = NullValueHandling.Ignore)]
        public string SellerOrganizationCode { get; set; }

        [JsonProperty("PriceInfo", NullValueHandling = NullValueHandling.Ignore)]
        public PriceInfo PriceInfo { get; set; }
    }

    public partial class PriceInfo
    {
        [JsonProperty("Currency", NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; }

        [JsonProperty("TotalAmount", NullValueHandling = NullValueHandling.Ignore)]
        public string TotalAmount { get; set; }
    }

    public partial class ShipmentLine
    {
        [JsonProperty("Shipment", NullValueHandling = NullValueHandling.Ignore)]
        public Shipment Shipment { get; set; }





        [JsonProperty("GiftFlag", NullValueHandling = NullValueHandling.Ignore)]
        public string GiftFlag { get; set; }

        [JsonProperty("CustomerPoLineNo", NullValueHandling = NullValueHandling.Ignore)]
        public string CustomerPoLineNo { get; set; }

        [JsonProperty("CustomerPoNo", NullValueHandling = NullValueHandling.Ignore)]
        public string CustomerPoNo { get; set; }

        [JsonProperty("Createuserid", NullValueHandling = NullValueHandling.Ignore)]
        public string Createuserid { get; set; }

        [JsonProperty("CountryOfOrigin", NullValueHandling = NullValueHandling.Ignore)]
        public string CountryOfOrigin { get; set; }

        [JsonProperty("Createprogid", NullValueHandling = NullValueHandling.Ignore)]
        public string Createprogid { get; set; }

        [JsonProperty("Createts", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? Createts { get; set; }

        [JsonProperty("IsPickable", NullValueHandling = NullValueHandling.Ignore)]
        public string IsPickable { get; set; }

        [JsonProperty("ItemDesc", NullValueHandling = NullValueHandling.Ignore)]
        public string ItemDesc { get; set; }



        [JsonProperty("KitCode", NullValueHandling = NullValueHandling.Ignore)]
        public string KitCode { get; set; }

        [JsonProperty("KitQty", NullValueHandling = NullValueHandling.Ignore)]
        public string KitQty { get; set; }



        [JsonProperty("Modifyprogid", NullValueHandling = NullValueHandling.Ignore)]
        public string Modifyprogid { get; set; }

        [JsonProperty("Modifyts", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? Modifyts { get; set; }



       

        

        [JsonProperty("ShipmentLineKey", NullValueHandling = NullValueHandling.Ignore)]
        public string ShipmentLineKey { get; set; }

        [JsonProperty("ShipmentLineNo", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? ShipmentLineNo { get; set; }



        [JsonProperty("ShortageQty", NullValueHandling = NullValueHandling.Ignore)]
        public string ShortageQty { get; set; }

        [JsonProperty("UnitOfMeasure", NullValueHandling = NullValueHandling.Ignore)]
        public string UnitOfMeasure { get; set; }

        [JsonProperty("WaveNo", NullValueHandling = NullValueHandling.Ignore)]
        public string WaveNo { get; set; }

        [JsonProperty("isHistory", NullValueHandling = NullValueHandling.Ignore)]
        public string IsHistory { get; set; }

        [JsonProperty("OrderHeaderKey", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderHeaderKey { get; set; }
    }

    public partial class Shipment
    {
        [JsonProperty("CarrierServiceCode", NullValueHandling = NullValueHandling.Ignore)]
        public string CarrierServiceCode { get; set; }

        [JsonProperty("ShipmentNo", NullValueHandling = NullValueHandling.Ignore)]
       
        public string ShipmentNo { get; set; }

        [JsonProperty("SCAC", NullValueHandling = NullValueHandling.Ignore)]
        public string Scac { get; set; }

        [JsonProperty("Containers", NullValueHandling = NullValueHandling.Ignore)]
        public List<Container> Containers { get; set; }

        [JsonProperty("Instructions", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> Instructions { get; set; }
    }

    public partial class OverallTotals
    {
        [JsonProperty("AdjustedSubtotalWithoutTaxes", NullValueHandling = NullValueHandling.Ignore)]
        public string AdjustedSubtotalWithoutTaxes { get; set; }

        [JsonProperty("GrandAdjustmentsWithoutShipping", NullValueHandling = NullValueHandling.Ignore)]
        public string GrandAdjustmentsWithoutShipping { get; set; }

        [JsonProperty("GrandAdjustmentsWithoutTotalShipping", NullValueHandling = NullValueHandling.Ignore)]
        public string GrandAdjustmentsWithoutTotalShipping { get; set; }



        [JsonProperty("GrandDeliveryCharges", NullValueHandling = NullValueHandling.Ignore)]
        public string GrandDeliveryCharges { get; set; }



        [JsonProperty("GrandExchangeTotal", NullValueHandling = NullValueHandling.Ignore)]
        public string GrandExchangeTotal { get; set; }

        [JsonProperty("GrandRefundTotal", NullValueHandling = NullValueHandling.Ignore)]
        public string GrandRefundTotal { get; set; }

        [JsonProperty("GrandShippingBaseCharge", NullValueHandling = NullValueHandling.Ignore)]
        public string GrandShippingBaseCharge { get; set; }

        [JsonProperty("GrandShippingCharges", NullValueHandling = NullValueHandling.Ignore)]
        public string GrandShippingCharges { get; set; }

        [JsonProperty("GrandShippingDiscount", NullValueHandling = NullValueHandling.Ignore)]
        public string GrandShippingDiscount { get; set; }

        [JsonProperty("GrandShippingTotal", NullValueHandling = NullValueHandling.Ignore)]
        public string GrandShippingTotal { get; set; }



        [JsonProperty("HdrAdjustment", NullValueHandling = NullValueHandling.Ignore)]
        public string HdrAdjustment { get; set; }

        [JsonProperty("SubtotalWithoutTaxes", NullValueHandling = NullValueHandling.Ignore)]
        public string SubtotalWithoutTaxes { get; set; }



        [JsonProperty("HdrShippingBaseCharge", NullValueHandling = NullValueHandling.Ignore)]
        public string HdrShippingBaseCharge { get; set; }

        [JsonProperty("HdrShippingCharges", NullValueHandling = NullValueHandling.Ignore)]
        public string HdrShippingCharges { get; set; }

        [JsonProperty("HdrShippingDiscount", NullValueHandling = NullValueHandling.Ignore)]
        public string HdrShippingDiscount { get; set; }

        [JsonProperty("HdrShippingTotal", NullValueHandling = NullValueHandling.Ignore)]
        public string HdrShippingTotal { get; set; }


        [JsonProperty("HeaderAdjustmentWithoutShipping", NullValueHandling = NullValueHandling.Ignore)]
        public string HeaderAdjustmentWithoutShipping { get; set; }



        [JsonProperty("ManualDiscountPercentage", NullValueHandling = NullValueHandling.Ignore)]
        public string ManualDiscountPercentage { get; set; }

        [JsonProperty("PendingRefundAmount", NullValueHandling = NullValueHandling.Ignore)]
        public string PendingRefundAmount { get; set; }

        [JsonProperty("PercentProfitMargin", NullValueHandling = NullValueHandling.Ignore)]
        public string PercentProfitMargin { get; set; }

        [JsonProperty("RefundedAmount", NullValueHandling = NullValueHandling.Ignore)]
        public string RefundedAmount { get; set; }

        [JsonProperty("OverallChargeTotals", NullValueHandling = NullValueHandling.Ignore)]
        public List<OverallChargeTotal> OverallChargeTotals { get; set; }
    }

    public partial class OverallChargeTotal
    {
        [JsonProperty("ChargeCategory", NullValueHandling = NullValueHandling.Ignore)]
        public string ChargeCategory { get; set; }

        [JsonProperty("ChargeName", NullValueHandling = NullValueHandling.Ignore)]
        public string ChargeName { get; set; }

        [JsonProperty("GrandCharges", NullValueHandling = NullValueHandling.Ignore)]
        public string GrandCharges { get; set; }

        [JsonProperty("GrandDiscount", NullValueHandling = NullValueHandling.Ignore)]
        public string GrandDiscount { get; set; }

        [JsonProperty("IsBillable", NullValueHandling = NullValueHandling.Ignore)]
        public string IsBillable { get; set; }

        [JsonProperty("IsDiscount", NullValueHandling = NullValueHandling.Ignore)]
        public string IsDiscount { get; set; }

        [JsonProperty("IsManual", NullValueHandling = NullValueHandling.Ignore)]
        public string IsManual { get; set; }

        [JsonProperty("IsShippingCharge", NullValueHandling = NullValueHandling.Ignore)]
        public string IsShippingCharge { get; set; }
    }

    public partial class PaymentMethod
    {
        [JsonProperty("CreditCardType", NullValueHandling = NullValueHandling.Ignore)]
        public string CreditCardType { get; set; }

        [JsonProperty("DisplayCreditCardNo", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? DisplayCreditCardNo { get; set; }

        [JsonProperty("PaymentReference2", NullValueHandling = NullValueHandling.Ignore)]
        public string PaymentReference2 { get; set; }

        [JsonProperty("TotalCharged", NullValueHandling = NullValueHandling.Ignore)]
        public string TotalCharged { get; set; }

        [JsonProperty("PersonInfoBillTo", NullValueHandling = NullValueHandling.Ignore)]
        public PersonInfoTo PersonInfoBillTo { get; set; }

        [JsonProperty("DisplayDebitCardNo", NullValueHandling = NullValueHandling.Ignore)]
        public string DisplayDebitCardNo { get; set; }

        [JsonProperty("PaymentType", NullValueHandling = NullValueHandling.Ignore)]
        public string PaymentType { get; set; }
    }

    public partial class PersonInfoBillTo
    {
        [JsonProperty("AddressLine1", NullValueHandling = NullValueHandling.Ignore)]
        public string AddressLine1 { get; set; }

        [JsonProperty("AddressLine2", NullValueHandling = NullValueHandling.Ignore)]
        public string AddressLine2 { get; set; }

        [JsonProperty("City", NullValueHandling = NullValueHandling.Ignore)]
        public string City { get; set; }

        [JsonProperty("State", NullValueHandling = NullValueHandling.Ignore)]
        public string State { get; set; }

        [JsonProperty("ZipCode", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? ZipCode { get; set; }
    }

    public partial class BjsTrackingOrderDto
    {
        public static BjsTrackingOrderDto FromJson(string json) => JsonConvert.DeserializeObject<BjsTrackingOrderDto>(json, Converter.Settings);
    } 
}
