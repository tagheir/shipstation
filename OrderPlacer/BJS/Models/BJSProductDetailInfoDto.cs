﻿
    // <auto-generated />
    //
    // To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
    //
    //    using OrderPlacer.Bjs.Models;
    //
    //    var bjsProductDetailInfoDto = BjsProductDetailInfoDto.FromJson(jsonString);

    namespace OrderPlacer.Bjs.Models
    {
        using System;
        using System.Collections.Generic;

        using System.Globalization;
        using Newtonsoft.Json;
        using Newtonsoft.Json.Converters;

        public partial class BjsProductDetailInfoDto
        {
           




          

            [JsonProperty("bjsitems", NullValueHandling = NullValueHandling.Ignore)]
            public List<Bjsitem> Bjsitems { get; set; }



            [JsonProperty("immediateParentName", NullValueHandling = NullValueHandling.Ignore)]
            public string ImmediateParentName { get; set; }

            [JsonProperty("partNumber", NullValueHandling = NullValueHandling.Ignore)]
            public string PartNumber { get; set; }



            [JsonProperty("productUOM", NullValueHandling = NullValueHandling.Ignore)]
            public ProductUom ProductUom { get; set; }

            [JsonProperty("gtin", NullValueHandling = NullValueHandling.Ignore)]
            public List<object> Gtin { get; set; }

            [JsonProperty("featureNames", NullValueHandling = NullValueHandling.Ignore)]
            public List<object> FeatureNames { get; set; }

            [JsonProperty("itemPrices", NullValueHandling = NullValueHandling.Ignore)]
            public ItemPrices ItemPrices { get; set; }



            [JsonProperty("maximumItemPrice", NullValueHandling = NullValueHandling.Ignore)]
            public ImumItemPrice MaximumItemPrice { get; set; }

            [JsonProperty("minimumItemPrice", NullValueHandling = NullValueHandling.Ignore)]
            public ImumItemPrice MinimumItemPrice { get; set; }

            [JsonProperty("maxItemPrice", NullValueHandling = NullValueHandling.Ignore)]
            public string MaxItemPrice { get; set; }

            [JsonProperty("immediateParentId", NullValueHandling = NullValueHandling.Ignore)]
            public string ImmediateParentId { get; set; }

            [JsonProperty("manufacturerName", NullValueHandling = NullValueHandling.Ignore)]
            public string ManufacturerName { get; set; }

            [JsonProperty("definingAttributes", NullValueHandling = NullValueHandling.Ignore)]
            public List<object> DefiningAttributes { get; set; }

            [JsonProperty("minItemPrice", NullValueHandling = NullValueHandling.Ignore)]
            public string MinItemPrice { get; set; }

            [JsonProperty("itemAvailDetails", NullValueHandling = NullValueHandling.Ignore)]
            public ItemAvailDetails ItemAvailDetails { get; set; }

            [JsonProperty("bjsItemsInventory", NullValueHandling = NullValueHandling.Ignore)]
            public List<BjsItemsInventory> BjsItemsInventory { get; set; }



            [JsonProperty("productType", NullValueHandling = NullValueHandling.Ignore)]
            public string ProductType { get; set; }


        [JsonProperty("description")]
        public BjsProductDetailInfoDtoDescription Description { get; set; }


        [JsonProperty("itemAvailDetailsELIG", NullValueHandling = NullValueHandling.Ignore)]
            public ItemAvailDetailsElig ItemAvailDetailsElig { get; set; }

        }

        public partial class BjsItemsInventory
        {
            [JsonProperty("itemId", NullValueHandling = NullValueHandling.Ignore)]
            public string ItemId { get; set; }

            [JsonProperty("availInventory", NullValueHandling = NullValueHandling.Ignore)]
            public bool? AvailInventory { get; set; }
        }

        public partial class Bjsitem
        {
            [JsonProperty("isSubscriptionEligible", NullValueHandling = NullValueHandling.Ignore)]
            public bool? IsSubscriptionEligible { get; set; }

            [JsonProperty("itemStandardPrice", NullValueHandling = NullValueHandling.Ignore)]
            public Price ItemStandardPrice { get; set; }

            [JsonProperty("offer", NullValueHandling = NullValueHandling.Ignore)]
            public Offer Offer { get; set; }

            [JsonProperty("itemId", NullValueHandling = NullValueHandling.Ignore)]
            public string ItemId { get; set; }



            [JsonProperty("mapPrice", NullValueHandling = NullValueHandling.Ignore)]
            public Price MapPrice { get; set; }



            [JsonProperty("partNumber", NullValueHandling = NullValueHandling.Ignore)]
            
            public long? PartNumber { get; set; }
        }

       

        public partial class Offer
        {
            [JsonProperty("endDate")]
            public object EndDate { get; set; }

            [JsonProperty("precedence", NullValueHandling = NullValueHandling.Ignore)]
            public long? Precedence { get; set; }

            [JsonProperty("startDate")]
            public object StartDate { get; set; }
        }

        public partial class Bjsrating
        {
            [JsonProperty("newestRvwDate", NullValueHandling = NullValueHandling.Ignore)]
            public DateTimeOffset? NewestRvwDate { get; set; }

            [JsonProperty("avgOvrlRating", NullValueHandling = NullValueHandling.Ignore)]
            public long? AvgOvrlRating { get; set; }

            [JsonProperty("largeStarImgLocn")]
            public object LargeStarImgLocn { get; set; }

            [JsonProperty("quickReviews", NullValueHandling = NullValueHandling.Ignore)]
            public long? QuickReviews { get; set; }

            [JsonProperty("customerVideos", NullValueHandling = NullValueHandling.Ignore)]
        
            public bool? CustomerVideos { get; set; }

            [JsonProperty("smallStarImgLocn")]
            public object SmallStarImgLocn { get; set; }

            [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
            public string Name { get; set; }

            [JsonProperty("fullReviews", NullValueHandling = NullValueHandling.Ignore)]
            public long? FullReviews { get; set; }

            [JsonProperty("customerImages", NullValueHandling = NullValueHandling.Ignore)]
        
            public bool? CustomerImages { get; set; }

            [JsonProperty("pageId", NullValueHandling = NullValueHandling.Ignore)]
            public string PageId { get; set; }

            [JsonProperty("oldestRvwDate", NullValueHandling = NullValueHandling.Ignore)]
            public DateTimeOffset? OldestRvwDate { get; set; }
        }

        public partial class BjsProductDetailInfoDtoDescription
        {
            [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
            public string Name { get; set; }
        }

        public partial class DescriptiveAttribute
        {
            [JsonProperty("sequenceNumber", NullValueHandling = NullValueHandling.Ignore)]
            public long? SequenceNumber { get; set; }

            [JsonProperty("usage", NullValueHandling = NullValueHandling.Ignore)]
            [JsonConverter(typeof(PurpleParseStringConverter))]
            public long? Usage { get; set; }

            [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
            public string Name { get; set; }

            [JsonProperty("attributeValueDataBeans", NullValueHandling = NullValueHandling.Ignore)]
            public List<AttributeValueDataBean> AttributeValueDataBeans { get; set; }

            [JsonProperty("attributeReferenceNumber", NullValueHandling = NullValueHandling.Ignore)]
            public string AttributeReferenceNumber { get; set; }

            [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
            public string Value { get; set; }
        }

        public partial class AttributeValueDataBean
        {
            [JsonProperty("sequenceNumber", NullValueHandling = NullValueHandling.Ignore)]
            public long? SequenceNumber { get; set; }

            [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
            public string Name { get; set; }

            [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
            public string Value { get; set; }
        }

        public partial class EntitledItem
        {
            [JsonProperty("definingAttributeValueDataBeans", NullValueHandling = NullValueHandling.Ignore)]
            public List<object> DefiningAttributeValueDataBeans { get; set; }

            [JsonProperty("itemID", NullValueHandling = NullValueHandling.Ignore)]
            public string ItemId { get; set; }

            [JsonProperty("descriptiveAttributeValueDataBeans", NullValueHandling = NullValueHandling.Ignore)]
            public List<DescriptiveAttribute> DescriptiveAttributeValueDataBeans { get; set; }

            [JsonProperty("buyable", NullValueHandling = NullValueHandling.Ignore)]
            public long? Buyable { get; set; }

            [JsonProperty("manufacturerPartNumber", NullValueHandling = NullValueHandling.Ignore)]
            [JsonConverter(typeof(PurpleParseStringConverter))]
            public long? ManufacturerPartNumber { get; set; }

            [JsonProperty("descriptiveAttributeDataBeans", NullValueHandling = NullValueHandling.Ignore)]
            public List<DescriptiveAttribute> DescriptiveAttributeDataBeans { get; set; }

            [JsonProperty("articleId", NullValueHandling = NullValueHandling.Ignore)]
            [JsonConverter(typeof(PurpleParseStringConverter))]
            public long? ArticleId { get; set; }

            [JsonProperty("partNumber", NullValueHandling = NullValueHandling.Ignore)]
            [JsonConverter(typeof(PurpleParseStringConverter))]
            public long? PartNumber { get; set; }

            [JsonProperty("calculatedContractPrice", NullValueHandling = NullValueHandling.Ignore)]
            public Price CalculatedContractPrice { get; set; }

            [JsonProperty("onSpecial", NullValueHandling = NullValueHandling.Ignore)]
            public long? OnSpecial { get; set; }

 
        }

        public partial class EntitledItemDescription
        {
            [JsonProperty("itemId", NullValueHandling = NullValueHandling.Ignore)]
            public string ItemId { get; set; }

            [JsonProperty("available", NullValueHandling = NullValueHandling.Ignore)]
            public long? Available { get; set; }

            [JsonProperty("fullImage", NullValueHandling = NullValueHandling.Ignore)]
            public Uri FullImage { get; set; }

            [JsonProperty("language_id", NullValueHandling = NullValueHandling.Ignore)]
            public long? LanguageId { get; set; }

            [JsonProperty("shortDescription", NullValueHandling = NullValueHandling.Ignore)]
            public string ShortDescription { get; set; }

            [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
            public string Name { get; set; }

            [JsonProperty("published", NullValueHandling = NullValueHandling.Ignore)]
            public long? Published { get; set; }

            [JsonProperty("longDescription", NullValueHandling = NullValueHandling.Ignore)]
            public string LongDescription { get; set; }
        }

        public partial class ItemAvailDetails
        {
            [JsonProperty("3000000000001957284", NullValueHandling = NullValueHandling.Ignore)]
            public ItemAvailDetails3000000000001957284 The3000000000001957284 { get; set; }
        }

        public partial class ItemAvailDetails3000000000001957284
        {
            [JsonProperty("itemAvailableInClub", NullValueHandling = NullValueHandling.Ignore)]
            public string ItemAvailableInClub { get; set; }

            [JsonProperty("itemAvailableOnline", NullValueHandling = NullValueHandling.Ignore)]
            public string ItemAvailableOnline { get; set; }
        }

        public partial class ItemAvailDetailsElig
        {
            [JsonProperty("Eligibility", NullValueHandling = NullValueHandling.Ignore)]
            public List<Eligibility> Eligibility { get; set; }
        }

        public partial class Eligibility
        {
            [JsonProperty("articleId", NullValueHandling = NullValueHandling.Ignore)]
            [JsonConverter(typeof(PurpleParseStringConverter))]
            public long? ArticleId { get; set; }

            [JsonProperty("itemType", NullValueHandling = NullValueHandling.Ignore)]
            public string ItemType { get; set; }

            [JsonProperty("program", NullValueHandling = NullValueHandling.Ignore)]
            public Program Program { get; set; }

            [JsonProperty("articleEBT", NullValueHandling = NullValueHandling.Ignore)]
            public string ArticleEbt { get; set; }

            [JsonProperty("attributes", NullValueHandling = NullValueHandling.Ignore)]
            public Attributes Attributes { get; set; }
        }

        public partial class Attributes
        {
            [JsonProperty("clubEBT", NullValueHandling = NullValueHandling.Ignore)]
            public ClubEbt ClubEbt { get; set; }
        }

        public partial class ClubEbt
        {
            [JsonProperty("Y", NullValueHandling = NullValueHandling.Ignore)]
            public List<object> Y { get; set; }

            [JsonProperty("N", NullValueHandling = NullValueHandling.Ignore)]
            public List<string> N { get; set; }
        }

        public partial class Program
        {
            [JsonProperty("sfc", NullValueHandling = NullValueHandling.Ignore)]
            public Bopic Sfc { get; set; }

            [JsonProperty("sdd", NullValueHandling = NullValueHandling.Ignore)]
            public Bopic Sdd { get; set; }

            [JsonProperty("bopic", NullValueHandling = NullValueHandling.Ignore)]
            public Bopic Bopic { get; set; }
        }

        public partial class Bopic
        {
            [JsonProperty("Y", NullValueHandling = NullValueHandling.Ignore)]
            public List<string> Y { get; set; }
        }

        public partial class ItemPrices
        {
            [JsonProperty("3000000000001957284", NullValueHandling = NullValueHandling.Ignore)]
            public ItemPrices3000000000001957284 The3000000000001957284 { get; set; }
        }

        public partial class ItemPrices3000000000001957284
        {
            [JsonProperty("itemDisc", NullValueHandling = NullValueHandling.Ignore)]
            public long? ItemDisc { get; set; }

            [JsonProperty("priceStrike", NullValueHandling = NullValueHandling.Ignore)]
            public double? PriceStrike { get; set; }

            [JsonProperty("offerPrice", NullValueHandling = NullValueHandling.Ignore)]
            public double? OfferPrice { get; set; }

            [JsonProperty("mapPrice", NullValueHandling = NullValueHandling.Ignore)]
            public double? MapPrice { get; set; }
        }

        public partial class ImumItemPrice
        {
            [JsonProperty("symbol", NullValueHandling = NullValueHandling.Ignore)]
            public string Symbol { get; set; }

            [JsonProperty("amount", NullValueHandling = NullValueHandling.Ignore)]
            public double? Amount { get; set; }

            [JsonProperty("catEntryId")]
            public object CatEntryId { get; set; }
        }

        public partial class ProductImages
        {
            [JsonProperty("imageName", NullValueHandling = NullValueHandling.Ignore)]
            [JsonConverter(typeof(PurpleParseStringConverter))]
            public long? ImageName { get; set; }

            [JsonProperty("watchVideoLink", NullValueHandling = NullValueHandling.Ignore)]
            public string WatchVideoLink { get; set; }

            [JsonProperty("alternateImageThumbnails", NullValueHandling = NullValueHandling.Ignore)]
            public List<object> AlternateImageThumbnails { get; set; }

            [JsonProperty("alternateImages", NullValueHandling = NullValueHandling.Ignore)]
            public List<object> AlternateImages { get; set; }

            [JsonProperty("fullImagePostfix", NullValueHandling = NullValueHandling.Ignore)]
            public string FullImagePostfix { get; set; }

            [JsonProperty("isThumbnailTiny", NullValueHandling = NullValueHandling.Ignore)]
            public bool? IsThumbnailTiny { get; set; }

            [JsonProperty("thumbNailImage", NullValueHandling = NullValueHandling.Ignore)]
            public Uri ThumbNailImage { get; set; }

            [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
            public string Type { get; set; }

            [JsonProperty("isThumbnailSmall", NullValueHandling = NullValueHandling.Ignore)]
            public bool? IsThumbnailSmall { get; set; }

            [JsonProperty("url")]
            public object Url { get; set; }

            [JsonProperty("catalogImageFamilyReceipe", NullValueHandling = NullValueHandling.Ignore)]
            public string CatalogImageFamilyReceipe { get; set; }

            [JsonProperty("fullImageZoomer", NullValueHandling = NullValueHandling.Ignore)]
            public Uri FullImageZoomer { get; set; }

            [JsonProperty("strImagePath")]
            public object StrImagePath { get; set; }

            [JsonProperty("catalogImagePath", NullValueHandling = NullValueHandling.Ignore)]
            public Uri CatalogImagePath { get; set; }

            [JsonProperty("catalogImagePathFamily", NullValueHandling = NullValueHandling.Ignore)]
            public Uri CatalogImagePathFamily { get; set; }

            [JsonProperty("featureThumbnailPostfix", NullValueHandling = NullValueHandling.Ignore)]
            public string FeatureThumbnailPostfix { get; set; }

            [JsonProperty("fullImage", NullValueHandling = NullValueHandling.Ignore)]
            public Uri FullImage { get; set; }

            [JsonProperty("thumbnailPostfix", NullValueHandling = NullValueHandling.Ignore)]
            public string ThumbnailPostfix { get; set; }

            [JsonProperty("productType")]
            public object ProductType { get; set; }
        }

        public partial class ProductUom
        {
            [JsonProperty("3000000000001957284", NullValueHandling = NullValueHandling.Ignore)]
            public string The3000000000001957284 { get; set; }
        }

        public partial class StoreAttributeDetail
        {
            [JsonProperty("Toys", NullValueHandling = NullValueHandling.Ignore)]
            public HandyDetails Toys { get; set; }

            [JsonProperty("MAX_QUANTITY", NullValueHandling = NullValueHandling.Ignore)]
            [JsonConverter(typeof(PurpleParseStringConverter))]
            public long? MaxQuantity { get; set; }

            [JsonProperty("TechAdvisorDetail", NullValueHandling = NullValueHandling.Ignore)]
            public HandyDetails TechAdvisorDetail { get; set; }

            [JsonProperty("WebCollageDetail", NullValueHandling = NullValueHandling.Ignore)]
            public WebCollageDetail WebCollageDetail { get; set; }

            [JsonProperty("HandyDetails", NullValueHandling = NullValueHandling.Ignore)]
            public HandyDetails HandyDetails { get; set; }
        }

        public partial class HandyDetails
        {
        }

        public partial class WebCollageDetail
        {
            [JsonProperty("WEBCOLLAGE_SWITCH", NullValueHandling = NullValueHandling.Ignore)]
            public string WebcollageSwitch { get; set; }

            [JsonProperty("WEBCOLLAGE_URL", NullValueHandling = NullValueHandling.Ignore)]
            public string WebcollageUrl { get; set; }
        }

        public partial class UserData
        {
            [JsonProperty("B2CPdpQAKillSwitch", NullValueHandling = NullValueHandling.Ignore)]
            public string B2CPdpQaKillSwitch { get; set; }

            [JsonProperty("killSwitchCoupon", NullValueHandling = NullValueHandling.Ignore)]
            public string KillSwitchCoupon { get; set; }

            [JsonProperty("killSwitchGC", NullValueHandling = NullValueHandling.Ignore)]
            public string KillSwitchGc { get; set; }

            [JsonProperty("killSwitchMicroService", NullValueHandling = NullValueHandling.Ignore)]
            public string KillSwitchMicroService { get; set; }

            [JsonProperty("killSwitchItemEligibilityEngine", NullValueHandling = NullValueHandling.Ignore)]
            public string KillSwitchItemEligibilityEngine { get; set; }

            [JsonProperty("killSwitchEGC", NullValueHandling = NullValueHandling.Ignore)]
            public string KillSwitchEgc { get; set; }

            [JsonProperty("killSwitchShippingRestriction", NullValueHandling = NullValueHandling.Ignore)]
            public string KillSwitchShippingRestriction { get; set; }

            [JsonProperty("B2CPdpBundlesKillSwitch", NullValueHandling = NullValueHandling.Ignore)]
            public string B2CPdpBundlesKillSwitch { get; set; }

            [JsonProperty("killSwitchBOPIC", NullValueHandling = NullValueHandling.Ignore)]
            public string KillSwitchBopic { get; set; }

            [JsonProperty("killSwitchPdpMagicZoomViewer", NullValueHandling = NullValueHandling.Ignore)]
            public string KillSwitchPdpMagicZoomViewer { get; set; }

            [JsonProperty("B2CPdpWhyDidYouBuyKillSwitch", NullValueHandling = NullValueHandling.Ignore)]
            public string B2CPdpWhyDidYouBuyKillSwitch { get; set; }
        }

        public partial struct BreadCrumbDetail
        {
            public List<object> AnythingArray;
            public string String;

            public static implicit operator BreadCrumbDetail(List<object> AnythingArray) => new BreadCrumbDetail { AnythingArray = AnythingArray };
            public static implicit operator BreadCrumbDetail(string String) => new BreadCrumbDetail { String = String };
        }

        public partial class BjsProductDetailInfoDto
        {
            public static BjsProductDetailInfoDto FromJson(string json) => JsonConvert.DeserializeObject<BjsProductDetailInfoDto>(json, OrderPlacer.Bjs.Models.Converter.Settings);
        }

        
    }


