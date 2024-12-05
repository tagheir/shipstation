﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using OrderPlacer.SamsClub.Models;
//
//    var samsProductDetailDto = SamsProductDetailDto.FromJson(jsonString);

namespace OrderPlacer.SamsClub.Models
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class SamsProductDetailDto
    {
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }

        [JsonProperty("payload", NullValueHandling = NullValueHandling.Ignore)]
        public Payload Payload { get; set; }
    }

    public partial class Payload
    {
        [JsonProperty("products", NullValueHandling = NullValueHandling.Ignore)]
        public List<Product> Products { get; set; }
    }

    public partial class Product
    {
        [JsonProperty("productId", NullValueHandling = NullValueHandling.Ignore)]
        public string ProductId { get; set; }

        [JsonProperty("productType", NullValueHandling = NullValueHandling.Ignore)]
        public string ProductType { get; set; }

        [JsonProperty("productBadge", NullValueHandling = NullValueHandling.Ignore)]
        public ProductBadge ProductBadge { get; set; }

        [JsonProperty("category", NullValueHandling = NullValueHandling.Ignore)]
        public Category Category { get; set; }

        [JsonProperty("searchAndSeo", NullValueHandling = NullValueHandling.Ignore)]
        public SearchAndSeo SearchAndSeo { get; set; }

        [JsonProperty("shippingOption", NullValueHandling = NullValueHandling.Ignore)]
        public ProductShippingOption ShippingOption { get; set; }

        [JsonProperty("manufacturingInfo", NullValueHandling = NullValueHandling.Ignore)]
        public ProductManufacturingInfo ManufacturingInfo { get; set; }

        [JsonProperty("descriptors", NullValueHandling = NullValueHandling.Ignore)]
        public ProductDescriptors Descriptors { get; set; }

        [JsonProperty("reviewsAndRatings", NullValueHandling = NullValueHandling.Ignore)]
        public ReviewsAndRatings ReviewsAndRatings { get; set; }

       
        [JsonProperty("variantSummary", NullValueHandling = NullValueHandling.Ignore)]
        public VariantSummary VariantSummary { get; set; }

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }

        [JsonProperty("skus", NullValueHandling = NullValueHandling.Ignore)]
        public List<Skus> Skus { get; set; }

        [JsonProperty("expiresAt", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? ExpiresAt { get; set; }

        [JsonProperty("cached", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Cached { get; set; }

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("prodProperties", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> ProdProperties { get; set; }

        [JsonProperty("legacyProductType", NullValueHandling = NullValueHandling.Ignore)]
        public string LegacyProductType { get; set; }

        [JsonProperty("parentCategories", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(DecodeArrayConverter))]
        public List<long> ParentCategories { get; set; }

        [JsonProperty("lastModifiedAt", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? LastModifiedAt { get; set; }
    }

    

    public partial class Breadcrumb
    {
        [JsonProperty("displayName", NullValueHandling = NullValueHandling.Ignore)]
        public string DisplayName { get; set; }

        [JsonProperty("navId", NullValueHandling = NullValueHandling.Ignore)]
        public long? NavId { get; set; }

        [JsonProperty("seoUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string SeoUrl { get; set; }
    }

    public partial class ProductDescriptors
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("shortDescription", NullValueHandling = NullValueHandling.Ignore)]
        public string ShortDescription { get; set; }

        [JsonProperty("longDescription", NullValueHandling = NullValueHandling.Ignore)]
        public string LongDescription { get; set; }
    }

    public partial class ProductManufacturingInfo
    {
        [JsonProperty("model", NullValueHandling = NullValueHandling.Ignore)]
       
        public string Model { get; set; }

        [JsonProperty("brand", NullValueHandling = NullValueHandling.Ignore)]
        public string Brand { get; set; }

        [JsonProperty("assembledCountry", NullValueHandling = NullValueHandling.Ignore)]
        public string AssembledCountry { get; set; }

        [JsonProperty("componentCountry", NullValueHandling = NullValueHandling.Ignore)]
        public string ComponentCountry { get; set; }

        [JsonProperty("specification", NullValueHandling = NullValueHandling.Ignore)]
        public string Specification { get; set; }

        [JsonProperty("warranty", NullValueHandling = NullValueHandling.Ignore)]
        public string Warranty { get; set; }

        [JsonProperty("salesTaxCode", NullValueHandling = NullValueHandling.Ignore)]
        public long? SalesTaxCode { get; set; }
    }

   



    
    public partial class ProductShippingOption
    {
        [JsonProperty("info", NullValueHandling = NullValueHandling.Ignore)]
        public string Info { get; set; }
    }

    public partial class Skus
    {
        




        [JsonProperty("skuLogistics", NullValueHandling = NullValueHandling.Ignore)]
        public SkuLogistics SkuLogistics { get; set; }



        [JsonProperty("skuProperties", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> SkuProperties { get; set; }
    }

  

    public partial class SkusDescriptors
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
    }

   



    public partial class SkuLogistics
    {
        [JsonProperty("length", NullValueHandling = NullValueHandling.Ignore)]
        public Height Length { get; set; }

        [JsonProperty("width", NullValueHandling = NullValueHandling.Ignore)]
        public Height Width { get; set; }

        [JsonProperty("height", NullValueHandling = NullValueHandling.Ignore)]
        public Height Height { get; set; }

        [JsonProperty("weight", NullValueHandling = NullValueHandling.Ignore)]
        public Height Weight { get; set; }

        [JsonProperty("numberOfBoxes", NullValueHandling = NullValueHandling.Ignore)]
        public long? NumberOfBoxes { get; set; }
    }

    public partial class Height
    {
        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public double? Value { get; set; }

        [JsonProperty("unitOfMeasure", NullValueHandling = NullValueHandling.Ignore)]
        public string UnitOfMeasure { get; set; }
    }

    public partial class VariantSummary
    {
        [JsonProperty("minVariance", NullValueHandling = NullValueHandling.Ignore)]
        public double? MinVariance { get; set; }

        [JsonProperty("maxVariance", NullValueHandling = NullValueHandling.Ignore)]
        public double? MaxVariance { get; set; }

        [JsonProperty("variantSkuIds", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> VariantSkuIds { get; set; }

        [JsonProperty("variantCriteria", NullValueHandling = NullValueHandling.Ignore)]
        public List<VariantCriterion> VariantCriteria { get; set; }

        [JsonProperty("variantInfoMap", NullValueHandling = NullValueHandling.Ignore)]
        public List<VariantInfoMap> VariantInfoMap { get; set; }
    }

    public partial class VariantCriterion
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("values", NullValueHandling = NullValueHandling.Ignore)]
        public List<VariantCriterionValue> Values { get; set; }
    }

    public partial class VariantCriterionValue
    {
        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }

        [JsonProperty("imageUrl", NullValueHandling = NullValueHandling.Ignore)]
        public Uri ImageUrl { get; set; }
    }

    public partial class VariantInfoMap
    {
        [JsonProperty("variantSkuId", NullValueHandling = NullValueHandling.Ignore)]
        public string VariantSkuId { get; set; }

        [JsonProperty("values", NullValueHandling = NullValueHandling.Ignore)]
        public List<VariantInfoMapValue> Values { get; set; }
    }

    public partial class VariantInfoMapValue
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }
    }

    public partial class SamsProductDetailDto
    {
        public static SamsProductDetailDto FromJson(string json) => JsonConvert.DeserializeObject<SamsProductDetailDto>(json, Converter.Settings);
    }

   

   
}
