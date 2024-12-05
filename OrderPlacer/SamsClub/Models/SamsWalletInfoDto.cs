using System;
using System.Collections.Generic;
using System.Text;



    namespace OrderPlacer.SamsClub.Models
    {
        using System;
        using System.Collections.Generic;

        using System.Globalization;
        using Newtonsoft.Json;
        using Newtonsoft.Json.Converters;

        public partial class SamsWalletInfoDto
        {
            [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
            public string Status { get; set; }

            [JsonProperty("payload", NullValueHandling = NullValueHandling.Ignore)]
            public Payload Payload { get; set; }
        }

        public partial class Payload
        {
            [JsonProperty("paymentCards", NullValueHandling = NullValueHandling.Ignore)]
            public PaymentCards PaymentCards { get; set; }

            [JsonProperty("storedValueCards", NullValueHandling = NullValueHandling.Ignore)]
            public StoredValueCards StoredValueCards { get; set; }
        }

        public partial class PaymentCards
        {
            [JsonProperty("creditCards", NullValueHandling = NullValueHandling.Ignore)]
            public List<CreditCard> CreditCards { get; set; }
        }

        public partial class CreditCard
        {
            [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
            public Guid? Id { get; set; }

            [JsonProperty("nameOnCard", NullValueHandling = NullValueHandling.Ignore)]
            public string NameOnCard { get; set; }

            [JsonProperty("lastFour", NullValueHandling = NullValueHandling.Ignore)]
            [JsonConverter(typeof(ParseStringConverter))]
            public long? LastFour { get; set; }

            [JsonProperty("cardProduct", NullValueHandling = NullValueHandling.Ignore)]
            public string CardProduct { get; set; }

            [JsonProperty("cvvRequired", NullValueHandling = NullValueHandling.Ignore)]
            public bool? CvvRequired { get; set; }

            [JsonProperty("isDefault", NullValueHandling = NullValueHandling.Ignore)]
            public bool? IsDefault { get; set; }

            [JsonProperty("isExpired", NullValueHandling = NullValueHandling.Ignore)]
            public bool? IsExpired { get; set; }

            [JsonProperty("expMonth", NullValueHandling = NullValueHandling.Ignore)]
            [JsonConverter(typeof(ParseStringConverter))]
            public long? ExpMonth { get; set; }

            [JsonProperty("expYear", NullValueHandling = NullValueHandling.Ignore)]
            [JsonConverter(typeof(ParseStringConverter))]
            public long? ExpYear { get; set; }

            [JsonProperty("lastUpdateDate", NullValueHandling = NullValueHandling.Ignore)]
            public string LastUpdateDate { get; set; }

            [JsonProperty("billingAddress", NullValueHandling = NullValueHandling.Ignore)]
            public BillingAddress BillingAddress { get; set; }
        }

        public partial class BillingAddress
        {
            [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
            public Guid? Id { get; set; }

            [JsonProperty("lineOne", NullValueHandling = NullValueHandling.Ignore)]
            public string LineOne { get; set; }

            [JsonProperty("city", NullValueHandling = NullValueHandling.Ignore)]
            public string City { get; set; }

            [JsonProperty("state", NullValueHandling = NullValueHandling.Ignore)]
            public string State { get; set; }

            [JsonProperty("stateCode", NullValueHandling = NullValueHandling.Ignore)]
            public string StateCode { get; set; }

            [JsonProperty("country", NullValueHandling = NullValueHandling.Ignore)]
            public string Country { get; set; }

            [JsonProperty("postalCode", NullValueHandling = NullValueHandling.Ignore)]
            [JsonConverter(typeof(ParseStringConverter))]
            public long? PostalCode { get; set; }

            [JsonProperty("addressType", NullValueHandling = NullValueHandling.Ignore)]
            public string AddressType { get; set; }

            [JsonProperty("contactPhone", NullValueHandling = NullValueHandling.Ignore)]
            public string ContactPhone { get; set; }

            [JsonProperty("contactPhoneType", NullValueHandling = NullValueHandling.Ignore)]
            public string ContactPhoneType { get; set; }

            [JsonProperty("contactFirstName", NullValueHandling = NullValueHandling.Ignore)]
            public string ContactFirstName { get; set; }

            [JsonProperty("contactLastName", NullValueHandling = NullValueHandling.Ignore)]
            public string ContactLastName { get; set; }
        }

        public partial class StoredValueCards
        {
            [JsonProperty("samsRewards", NullValueHandling = NullValueHandling.Ignore)]
            public SamsRewards SamsRewards { get; set; }

            [JsonProperty("samsCash", NullValueHandling = NullValueHandling.Ignore)]
            public SamsCash SamsCash { get; set; }
        }

        public partial class SamsCash
        {
            [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
            public Guid? Id { get; set; }

            [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
            public string Type { get; set; }

            [JsonProperty("balance", NullValueHandling = NullValueHandling.Ignore)]
            public Balance Balance { get; set; }
        }

        public partial class Balance
        {
            [JsonProperty("currency", NullValueHandling = NullValueHandling.Ignore)]
            public string Currency { get; set; }

            [JsonProperty("amount", NullValueHandling = NullValueHandling.Ignore)]
            public string Amount { get; set; }
        }

        public partial class SamsRewards
        {
            [JsonProperty("showAsSamsCash", NullValueHandling = NullValueHandling.Ignore)]
            public bool? ShowAsSamsCash { get; set; }

            [JsonProperty("balance", NullValueHandling = NullValueHandling.Ignore)]
            public Balance Balance { get; set; }

            [JsonProperty("rewards", NullValueHandling = NullValueHandling.Ignore)]
            public List<Reward> Rewards { get; set; }
        }

        public partial class Reward
        {
            [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
            public Guid? Id { get; set; }

            [JsonProperty("source", NullValueHandling = NullValueHandling.Ignore)]
            public string Source { get; set; }

            [JsonProperty("shouldVerifyMember", NullValueHandling = NullValueHandling.Ignore)]
            public bool? ShouldVerifyMember { get; set; }

            [JsonProperty("verificationType", NullValueHandling = NullValueHandling.Ignore)]
            public string VerificationType { get; set; }

            [JsonProperty("thresholdAmount", NullValueHandling = NullValueHandling.Ignore)]
            public Balance ThresholdAmount { get; set; }

            [JsonProperty("sequence", NullValueHandling = NullValueHandling.Ignore)]
            public long? Sequence { get; set; }

            [JsonProperty("balance", NullValueHandling = NullValueHandling.Ignore)]
            public Balance Balance { get; set; }
    }

       


    public partial class LocationInfoDto
    {
        [JsonProperty("address")]
        public Address Address { get; set; }

        [JsonProperty("clubAttributes")]
        public ClubAttributes ClubAttributes { get; set; }

        [JsonProperty("distance")]
        public double Distance { get; set; }

        [JsonProperty("features")]
        public Features Features { get; set; }

        [JsonProperty("gasPrices")]
        public GasPrice[] GasPrices { get; set; }

        [JsonProperty("geoPoint")]
        public GeoPoint GeoPoint { get; set; }



        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }



        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("services")]
        public string[] Services { get; set; }

        [JsonProperty("timeZone")]
        public string TimeZone { get; set; }

        [JsonProperty("expiresAt")]
        public long ExpiresAt { get; set; }
    }

    
    public partial class ClubAttributes
    {
        [JsonProperty("isPrepayCNP")]
        public bool IsPrepayCnp { get; set; }

        [JsonProperty("deliveryGSCCode")]
        public long DeliveryGscCode { get; set; }

        [JsonProperty("isDriveThroughPickup")]
        public bool IsDriveThroughPickup { get; set; }

        [JsonProperty("isMandatoryPrepay")]
        public bool IsMandatoryPrepay { get; set; }

        [JsonProperty("isClubEnabledForRegularDelivery")]
        public bool IsClubEnabledForRegularDelivery { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("atdLocationNumber")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long AtdLocationNumber { get; set; }

        [JsonProperty("isTemporarilyClosed")]
        public bool IsTemporarilyClosed { get; set; }

        [JsonProperty("isExitKiosk")]
        public bool IsExitKiosk { get; set; }

        [JsonProperty("isPrepayTires")]
        public bool IsPrepayTires { get; set; }

        [JsonProperty("globalLocationNumber")]
        public string GlobalLocationNumber { get; set; }

        [JsonProperty("isClubEnabledForCorporateDelivery")]
        public bool IsClubEnabledForCorporateDelivery { get; set; }

        [JsonProperty("isCNPClub")]
        public bool IsCnpClub { get; set; }

        [JsonProperty("isWarpEnabledClub")]
        public bool IsWarpEnabledClub { get; set; }

        [JsonProperty("isEntryBleBeacon")]
        public bool IsEntryBleBeacon { get; set; }

        [JsonProperty("minimumPickupTimeBuffer")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long MinimumPickupTimeBuffer { get; set; }

        [JsonProperty("isGIFEnabled")]
        public bool IsGifEnabled { get; set; }

        [JsonProperty("noOfParkingSpots")]
        public long NoOfParkingSpots { get; set; }

        [JsonProperty("isInstacartEnabled")]
        public bool IsInstacartEnabled { get; set; }

        [JsonProperty("isMobileCheckinEnabled")]
        public bool IsMobileCheckinEnabled { get; set; }

        [JsonProperty("isScanNGo")]
        public bool IsScanNGo { get; set; }

        [JsonProperty("isSameDayPickup")]
        public bool IsSameDayPickup { get; set; }

        [JsonProperty("clubMessage")]
        public string ClubMessage { get; set; }

        [JsonProperty("isCurbSideClub")]
        public bool IsCurbSideClub { get; set; }

        [JsonProperty("isPickUpThresholdEnabled")]
        public bool IsPickUpThresholdEnabled { get; set; }
    }

    public partial class Features
    {
        [JsonProperty("IsTemporarilyClosed")]
        public bool IsTemporarilyClosed { get; set; }

        [JsonProperty("IsDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("isInstacartEnabled")]
        public bool IsInstacartEnabled { get; set; }

        [JsonProperty("isClubPickupEnabled")]
        public bool IsClubPickupEnabled { get; set; }
    }

    public partial class GasPrice
    {
        [JsonProperty("gradeId")]
        public long GradeId { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public partial class GeoPoint
    {
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }
    }




    

        public partial class CartStoreDto
        {
            [JsonProperty("payload", NullValueHandling = NullValueHandling.Ignore)]
            public PayloadCustomer Payload { get; set; }
        }

        public partial class PayloadCustomer
        {
            [JsonProperty("customerId", NullValueHandling = NullValueHandling.Ignore)]
            public string CustomerId { get; set; }

            [JsonProperty("postalCode", NullValueHandling = NullValueHandling.Ignore)]
            [JsonConverter(typeof(ParseStringConverter))]
            public long? PostalCode { get; set; }

            [JsonProperty("clubId", NullValueHandling = NullValueHandling.Ignore)]
            [JsonConverter(typeof(ParseStringConverter))]
            public long? ClubId { get; set; }

            [JsonProperty("delAddrId", NullValueHandling = NullValueHandling.Ignore)]
            public string DelAddrId { get; set; }
        }

    }


