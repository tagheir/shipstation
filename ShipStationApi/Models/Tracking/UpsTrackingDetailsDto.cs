﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ShipStationApi.Models.Tracking
{
    // <auto-generated />
    //
    // To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
    //
    //    using QuickType;
    //
    //    var upsTrackingDetailsDto = UpsTrackingDetailsDto.FromJson(jsonString);



    public partial class UpsTrackingDetailsDto
    {
        [JsonProperty("statusCode")]
        public string StatusCode { get; set; }

        [JsonProperty("statusText")]
        public string StatusText { get; set; }

        [JsonProperty("isLoggedInUser")]
        public bool IsLoggedInUser { get; set; }

        [JsonProperty("trackedDateTime")]
        public string TrackedDateTime { get; set; }

        [JsonProperty("isBcdnMultiView")]
        public bool IsBcdnMultiView { get; set; }

        [JsonProperty("returnToDetails")]
        public ReturnToDetails ReturnToDetails { get; set; }

        [JsonProperty("trackDetails")]
        public List<TrackDetail> TrackDetails { get; set; }
    }

    public partial class ReturnToDetails
    {
        [JsonProperty("returnToURL")]
        public dynamic ReturnToUrl { get; set; }

        [JsonProperty("returnToApp")]
        public dynamic ReturnToApp { get; set; }
    }

    public partial class TrackDetail
    {
        [JsonProperty("errorCode")]
        public dynamic ErrorCode { get; set; }

        [JsonProperty("errorText")]
        public dynamic ErrorText { get; set; }

        [JsonProperty("requestedTrackingNumber")]
        public string RequestedTrackingNumber { get; set; }

        [JsonProperty("trackingNumber")]
        public string TrackingNumber { get; set; }

        [JsonProperty("isMobileDevice")]
        public bool IsMobileDevice { get; set; }

        [JsonProperty("packageStatus")]
        public string PackageStatus { get; set; }

        [JsonProperty("packageStatusType")]
        public string PackageStatusType { get; set; }

        [JsonProperty("packageStatusCode")]
        public string PackageStatusCode { get; set; }

        [JsonProperty("progressBarType")]
        public string ProgressBarType { get; set; }

        [JsonProperty("progressBarPercentage")]
        public dynamic ProgressBarPercentage { get; set; }

        [JsonProperty("simplifiedText")]
        public string SimplifiedText { get; set; }

        [JsonProperty("scheduledDeliveryDayCMSKey")]
        public string ScheduledDeliveryDayCmsKey { get; set; }

        [JsonProperty("scheduledDeliveryDate")]
        public string ScheduledDeliveryDate { get; set; }

        [JsonProperty("scheduledDeliverDateDetail")]
        public dynamic ScheduledDeliverDateDetail { get; set; }

        [JsonProperty("noEstimatedDeliveryDateLabel")]
        public string NoEstimatedDeliveryDateLabel { get; set; }

        [JsonProperty("scheduledDeliveryTime")]
        public dynamic ScheduledDeliveryTime { get; set; }

        [JsonProperty("scheduledDeliveryTimeEODLabel")]
        public dynamic ScheduledDeliveryTimeEodLabel { get; set; }

        [JsonProperty("packageCommitedTime")]
        public string PackageCommitedTime { get; set; }

        [JsonProperty("endOfDayResCMSKey")]
        public dynamic EndOfDayResCmsKey { get; set; }

        [JsonProperty("deliveredDayCMSKey")]
        public string DeliveredDayCmsKey { get; set; }

        [JsonProperty("deliveredDate")]
        public string DeliveredDate { get; set; }

        [JsonProperty("deliveredDateDetail")]
        public dynamic DeliveredDateDetail { get; set; }

        [JsonProperty("deliveredTime")]
        public string DeliveredTime { get; set; }

        [JsonProperty("receivedBy")]
        public string ReceivedBy { get; set; }

        [JsonProperty("leaveAt")]
        public dynamic LeaveAt { get; set; }

        [JsonProperty("milestones")]
        public List<Milestone> Milestones { get; set; }

        [JsonProperty("alertCount")]
        public long AlertCount { get; set; }

        [JsonProperty("isEligibleViewMoreAlerts")]
        public bool IsEligibleViewMoreAlerts { get; set; }

        [JsonProperty("cdiLeaveAt")]
        public dynamic CdiLeaveAt { get; set; }

        [JsonProperty("leftAt")]
        public string LeftAt { get; set; }

        [JsonProperty("showNoInfoDate")]
        public bool ShowNoInfoDate { get; set; }

        [JsonProperty("showPickupByDate")]
        public bool ShowPickupByDate { get; set; }

        [JsonProperty("shipToAddress")]
        public ShipToAddress ShipToAddress { get; set; }

        [JsonProperty("shipFromAddress")]
        public dynamic ShipFromAddress { get; set; }

        [JsonProperty("consigneeAddress")]
        public dynamic ConsigneeAddress { get; set; }

        [JsonProperty("signatureTrackingUrl")]
        public dynamic SignatureTrackingUrl { get; set; }

        [JsonProperty("trackHistoryDescription")]
        public dynamic TrackHistoryDescription { get; set; }

        [JsonProperty("additionalInformation")]
        public AdditionalInformation AdditionalInformation { get; set; }

        [JsonProperty("specialInstructions")]
        public dynamic SpecialInstructions { get; set; }

        [JsonProperty("proofOfDeliveryUrl")]
        public dynamic ProofOfDeliveryUrl { get; set; }

        [JsonProperty("upsAccessPoint")]
        public dynamic UpsAccessPoint { get; set; }

        [JsonProperty("additionalPackagesCount")]
        public dynamic AdditionalPackagesCount { get; set; }

        [JsonProperty("attentionNeeded")]
        public AttentionNeeded AttentionNeeded { get; set; }

        [JsonProperty("shipmentProgressActivities")]
        public List<ShipmentProgressActivity> ShipmentProgressActivities { get; set; }

        [JsonProperty("trackingNumberType")]
        public string TrackingNumberType { get; set; }

        [JsonProperty("preAuthorizedForReturnData")]
        public dynamic PreAuthorizedForReturnData { get; set; }

        [JsonProperty("shipToAddressLblKey")]
        public string ShipToAddressLblKey { get; set; }

        [JsonProperty("isShipToAddressChangePending")]
        public bool IsShipToAddressChangePending { get; set; }

        [JsonProperty("trackSummaryView")]
        public dynamic TrackSummaryView { get; set; }

        [JsonProperty("senderShipperNumber")]
        public string SenderShipperNumber { get; set; }

        [JsonProperty("internalKey")]
        public string InternalKey { get; set; }

        [JsonProperty("userOptions")]
        public UserOptions UserOptions { get; set; }

        [JsonProperty("sendUpdatesOptions")]
        public SendUpdatesOptions SendUpdatesOptions { get; set; }

        [JsonProperty("myChoiceUpSellLink")]
        public string MyChoiceUpSellLink { get; set; }

        [JsonProperty("bcdnNumber")]
        public dynamic BcdnNumber { get; set; }

        [JsonProperty("promo")]
        public Promo Promo { get; set; }

        [JsonProperty("whatsNextText")]
        public dynamic WhatsNextText { get; set; }

        [JsonProperty("packageStatusTimeLbl")]
        public dynamic PackageStatusTimeLbl { get; set; }

        [JsonProperty("deSepcialTranslation")]
        public bool DeSepcialTranslation { get; set; }

        [JsonProperty("packageStatusTime")]
        public dynamic PackageStatusTime { get; set; }

        [JsonProperty("myChoiceToken")]
        public dynamic MyChoiceToken { get; set; }

        [JsonProperty("showMycTerms")]
        public bool ShowMycTerms { get; set; }

        [JsonProperty("enrollNum")]
        public string EnrollNum { get; set; }

        [JsonProperty("showConfirmWindow")]
        public bool ShowConfirmWindow { get; set; }

        [JsonProperty("confirmWindowLbl")]
        public dynamic ConfirmWindowLbl { get; set; }

        [JsonProperty("confirmWindowLink")]
        public dynamic ConfirmWindowLink { get; set; }

        [JsonProperty("followMyDelivery")]
        public dynamic FollowMyDelivery { get; set; }

        [JsonProperty("fileClaim")]
        public dynamic FileClaim { get; set; }

        [JsonProperty("viewClaim")]
        public dynamic ViewClaim { get; set; }

        [JsonProperty("flightInformation")]
        public dynamic FlightInformation { get; set; }

        [JsonProperty("voyageInformation")]
        public dynamic VoyageInformation { get; set; }

        [JsonProperty("viewDeliveryReceipt")]
        public dynamic ViewDeliveryReceipt { get; set; }

        [JsonProperty("isInWatchList")]
        public bool IsInWatchList { get; set; }

        [JsonProperty("isHistoryUpdateRequire")]
        public bool IsHistoryUpdateRequire { get; set; }

        [JsonProperty("consumerHub")]
        public string ConsumerHub { get; set; }

        [JsonProperty("campusShip")]
        public dynamic CampusShip { get; set; }

        [JsonProperty("asrInformation")]
        public AsrInformation AsrInformation { get; set; }

        [JsonProperty("isSuppressDetailTab")]
        public bool IsSuppressDetailTab { get; set; }

        [JsonProperty("isUpsPremierPackage")]
        public bool IsUpsPremierPackage { get; set; }

        [JsonProperty("lastSensorLocation")]
        public dynamic LastSensorLocation { get; set; }

        [JsonProperty("lastSensorEnvInfo")]
        public dynamic LastSensorEnvInfo { get; set; }

        [JsonProperty("isPremierStyleEligible")]
        public bool IsPremierStyleEligible { get; set; }

        [JsonProperty("isEDW")]
        public bool IsEdw { get; set; }

        [JsonProperty("disableSDDSection")]
        public bool DisableSddSection { get; set; }

        [JsonProperty("shipmentUpsellEligible")]
        public bool ShipmentUpsellEligible { get; set; }
    }

    public partial class AdditionalInformation
    {
        [JsonProperty("serviceInformation")]
        public ServiceInformation ServiceInformation { get; set; }

        [JsonProperty("weight")]
        public string Weight { get; set; }

        [JsonProperty("weightUnit")]
        public string WeightUnit { get; set; }

        [JsonProperty("codInformation")]
        public dynamic CodInformation { get; set; }

        [JsonProperty("shippedOrBilledDate")]
        public string ShippedOrBilledDate { get; set; }

        [JsonProperty("referenceNumbers")]
        public dynamic ReferenceNumbers { get; set; }

        [JsonProperty("postalServiceTrackingID")]
        public string PostalServiceTrackingId { get; set; }

        [JsonProperty("alternateTrackingNumbers")]
        public dynamic AlternateTrackingNumbers { get; set; }

        [JsonProperty("otherRequestedServices")]
        public dynamic OtherRequestedServices { get; set; }

        [JsonProperty("descriptionOfGood")]
        public string DescriptionOfGood { get; set; }

        [JsonProperty("cargoReady")]
        public string CargoReady { get; set; }

        [JsonProperty("fileNumber")]
        public string FileNumber { get; set; }

        [JsonProperty("originPort")]
        public string OriginPort { get; set; }

        [JsonProperty("destinationPort")]
        public string DestinationPort { get; set; }

        [JsonProperty("estimatedArrival")]
        public string EstimatedArrival { get; set; }

        [JsonProperty("estimatedDeparture")]
        public string EstimatedDeparture { get; set; }

        [JsonProperty("poNumber")]
        public string PoNumber { get; set; }

        [JsonProperty("blNumber")]
        public string BlNumber { get; set; }

        [JsonProperty("appointmentMade")]
        public dynamic AppointmentMade { get; set; }

        [JsonProperty("appointmentRequested")]
        public dynamic AppointmentRequested { get; set; }

        [JsonProperty("appointmentRequestedRange")]
        public dynamic AppointmentRequestedRange { get; set; }

        [JsonProperty("manifest")]
        public string Manifest { get; set; }

        [JsonProperty("isSmallPackage")]
        public bool IsSmallPackage { get; set; }

        [JsonProperty("shipmentVolume")]
        public dynamic ShipmentVolume { get; set; }

        [JsonProperty("numberOfPallets")]
        public dynamic NumberOfPallets { get; set; }

        [JsonProperty("shipmentCategory")]
        public string ShipmentCategory { get; set; }

        [JsonProperty("pkgSequenceNum")]
        public dynamic PkgSequenceNum { get; set; }

        [JsonProperty("pkgIdentificationCode")]
        public dynamic PkgIdentificationCode { get; set; }

        [JsonProperty("pkgID")]
        public dynamic PkgId { get; set; }

        [JsonProperty("product")]
        public dynamic Product { get; set; }

        [JsonProperty("numberOfPieces")]
        public dynamic NumberOfPieces { get; set; }

        [JsonProperty("wwef")]
        public bool Wwef { get; set; }

        [JsonProperty("wwePostal")]
        public bool WwePostal { get; set; }

        [JsonProperty("showAltTrkLink")]
        public bool ShowAltTrkLink { get; set; }

        [JsonProperty("wweParcel")]
        public bool WweParcel { get; set; }

        [JsonProperty("deliveryPreference")]
        public dynamic DeliveryPreference { get; set; }

        [JsonProperty("liftGateOnDelivery")]
        public dynamic LiftGateOnDelivery { get; set; }

        [JsonProperty("liftGateOnPickup")]
        public dynamic LiftGateOnPickup { get; set; }

        [JsonProperty("pickupDropOffDate")]
        public dynamic PickupDropOffDate { get; set; }

        [JsonProperty("pickupPreference")]
        public dynamic PickupPreference { get; set; }
    }

    public partial class ServiceInformation
    {
        [JsonProperty("serviceName")]
        public string ServiceName { get; set; }

        [JsonProperty("serviceLink")]
        public string ServiceLink { get; set; }

        [JsonProperty("serviceAttribute")]
        public dynamic ServiceAttribute { get; set; }
    }

    public partial class AsrInformation
    {
        [JsonProperty("allowDriverRelease")]
        public dynamic AllowDriverRelease { get; set; }

        [JsonProperty("processEDN")]
        public dynamic ProcessEdn { get; set; }
    }

    public partial class AttentionNeeded
    {
        [JsonProperty("actions")]
        public List<dynamic> Actions { get; set; }

        [JsonProperty("isCorrectMyAddress")]
        public bool IsCorrectMyAddress { get; set; }
    }

    public partial class Milestone
    {
        [JsonProperty("isCurrent")]
        public bool IsCurrent { get; set; }

        [JsonProperty("isCompleted")]
        public bool IsCompleted { get; set; }

        [JsonProperty("isFuture")]
        public bool IsFuture { get; set; }

        [JsonProperty("isRFIDIcon")]
        public bool IsRfidIcon { get; set; }

        [JsonProperty("category")]
        public dynamic Category { get; set; }

        [JsonProperty("subMilestone")]
        public dynamic SubMilestone { get; set; }

        [JsonProperty("returnTrackingNumber")]
        public dynamic ReturnTrackingNumber { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("nameKey")]
        public string NameKey { get; set; }
    }

    public partial class Promo
    {
        [JsonProperty("isPackagePromotion")]
        public bool IsPackagePromotion { get; set; }

        [JsonProperty("isShipperPromotion")]
        public bool IsShipperPromotion { get; set; }

        [JsonProperty("productImage")]
        public dynamic ProductImage { get; set; }

        [JsonProperty("title")]
        public dynamic Title { get; set; }

        [JsonProperty("description")]
        public dynamic Description { get; set; }

        [JsonProperty("shipperURL")]
        public Uri ShipperUrl { get; set; }

        [JsonProperty("shipperLogoURL")]
        public Uri ShipperLogoUrl { get; set; }
    }

    public partial class SendUpdatesOptions
    {
        [JsonProperty("bridgePageType")]
        public string BridgePageType { get; set; }

        [JsonProperty("myChoicePreferencesLink")]
        public string MyChoicePreferencesLink { get; set; }

        [JsonProperty("isSMSSupported")]
        public bool IsSmsSupported { get; set; }

        [JsonProperty("isEligibleToRetrieveDelAlerts")]
        public bool IsEligibleToRetrieveDelAlerts { get; set; }

        [JsonProperty("qvnNotificationsInfo")]
        public QvnNotificationsInfo QvnNotificationsInfo { get; set; }

        [JsonProperty("text")]
        public dynamic Text { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public dynamic Url { get; set; }
    }

    public partial class QvnNotificationsInfo
    {
        [JsonProperty("isDisplayCurrentStatus")]
        public bool IsDisplayCurrentStatus { get; set; }

        [JsonProperty("isDisplayUnforeseenEventsOrDelays")]
        public bool IsDisplayUnforeseenEventsOrDelays { get; set; }

        [JsonProperty("isDisplayShipmentDelivered")]
        public bool IsDisplayShipmentDelivered { get; set; }

        [JsonProperty("isDisplayPackageReadyForPickup")]
        public bool IsDisplayPackageReadyForPickup { get; set; }

        [JsonProperty("isPreCheckedCurrentStatus")]
        public bool IsPreCheckedCurrentStatus { get; set; }

        [JsonProperty("isPreCheckedUnforeseenEventsOrDelays")]
        public bool IsPreCheckedUnforeseenEventsOrDelays { get; set; }

        [JsonProperty("isPreCheckedShipmentDelivered")]
        public bool IsPreCheckedShipmentDelivered { get; set; }

        [JsonProperty("isPreCheckedPackageReadyForPickup")]
        public bool IsPreCheckedPackageReadyForPickup { get; set; }

        [JsonProperty("languageOptions")]
        public List<LanguageOption> LanguageOptions { get; set; }

        [JsonProperty("defaultSelectedLanguage")]
        public string DefaultSelectedLanguage { get; set; }

        [JsonProperty("email")]
        public dynamic Email { get; set; }

        [JsonProperty("phoneNumber")]
        public dynamic PhoneNumber { get; set; }
    }

    public partial class LanguageOption
    {
        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }
    }

    public partial class ShipToAddress
    {
        [JsonProperty("streetAddress1")]
        public string StreetAddress1 { get; set; }

        [JsonProperty("streetAddress2")]
        public string StreetAddress2 { get; set; }

        [JsonProperty("streetAddress3")]
        public string StreetAddress3 { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("province")]
        public dynamic Province { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("zipCode")]
        public string ZipCode { get; set; }

        [JsonProperty("companyName")]
        public string CompanyName { get; set; }

        [JsonProperty("attentionName")]
        public string AttentionName { get; set; }

        [JsonProperty("isAddressCorrected")]
        public bool IsAddressCorrected { get; set; }

        [JsonProperty("isReturnAddress")]
        public bool IsReturnAddress { get; set; }

        [JsonProperty("isHoldAddress")]
        public bool IsHoldAddress { get; set; }
    }

    public partial class ShipmentProgressActivity
    {
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("activityScan")]
        public string ActivityScan { get; set; }

        [JsonProperty("milestoneName")]
        public MilestoneName MilestoneName { get; set; }

        [JsonProperty("isInOverViewTable")]
        public bool IsInOverViewTable { get; set; }

        [JsonProperty("activityAdditionalDescription")]
        public dynamic ActivityAdditionalDescription { get; set; }

        [JsonProperty("trailer")]
        public string Trailer { get; set; }

        [JsonProperty("isDisplayPodLink")]
        public bool IsDisplayPodLink { get; set; }

        [JsonProperty("isRFIDIconEvent")]
        public bool IsRfidIconEvent { get; set; }

        [JsonProperty("actCode")]
        public string ActCode { get; set; }
    }

    public partial class MilestoneName
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("nameKey")]
        public string NameKey { get; set; }
    }

    public partial class UserOptions
    {
        [JsonProperty("deliveryOptions")]
        public DeliveryOptions DeliveryOptions { get; set; }
    }

    public partial class DeliveryOptions
    {
        [JsonProperty("isNotLoggedInMyChoicePage")]
        public bool IsNotLoggedInMyChoicePage { get; set; }

        [JsonProperty("isInfoNoticePage")]
        public bool IsInfoNoticePage { get; set; }

        [JsonProperty("isLoggedInNoneMyChoicePage")]
        public bool IsLoggedInNoneMyChoicePage { get; set; }

        [JsonProperty("isContactOnlyPage")]
        public bool IsContactOnlyPage { get; set; }

        [JsonProperty("isRedirect")]
        public bool IsRedirect { get; set; }

        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("signUp")]
        public string SignUp { get; set; }

        [JsonProperty("deliveryChanges")]
        public dynamic DeliveryChanges { get; set; }

        [JsonProperty("siiEligible")]
        public dynamic SiiEligible { get; set; }

        [JsonProperty("upgradeToUpsGround")]
        public dynamic UpgradeToUpsGround { get; set; }

        [JsonProperty("contactUps")]
        public dynamic ContactUps { get; set; }

        [JsonProperty("redirect")]
        public dynamic Redirect { get; set; }

        [JsonProperty("myChoiceTandCUrl")]
        public dynamic MyChoiceTandCUrl { get; set; }

        [JsonProperty("doappUrl")]
        public string DoappUrl { get; set; }

        [JsonProperty("dcrEligible")]
        public bool DcrEligible { get; set; }

        [JsonProperty("notLoggedInMyChoicePage")]
        public NotLoggedInMyChoicePage NotLoggedInMyChoicePage { get; set; }

        [JsonProperty("isIdentityVerification")]
        public bool IsIdentityVerification { get; set; }

        [JsonProperty("isRedirectToUAPPage")]
        public bool IsRedirectToUapPage { get; set; }

        [JsonProperty("changeDeliveryUrl")]
        public dynamic ChangeDeliveryUrl { get; set; }

        [JsonProperty("text")]
        public dynamic Text { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public dynamic Url { get; set; }
    }

    public partial class NotLoggedInMyChoicePage
    {
        [JsonProperty("deliveryChangesOptions")]
        public List<string> DeliveryChangesOptions { get; set; }

        [JsonProperty("isDriverInstructions")]
        public bool IsDriverInstructions { get; set; }

        [JsonProperty("isDeliveryChanges")]
        public bool IsDeliveryChanges { get; set; }

        [JsonProperty("isSignUpLogin")]
        public bool IsSignUpLogin { get; set; }

        [JsonProperty("isInfonoticeNote")]
        public bool IsInfonoticeNote { get; set; }
    }




}


