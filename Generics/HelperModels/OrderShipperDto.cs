using System;

namespace Generics.HelperModels
{
    public class OrderShipperDto
    {
        public string TrackingId { get; set; }
        public DateTimeOffset TrackingDate { get; set; }
        public string ShipstationOrderId { get; set; }
        public string PlatformOrderId { get; set; }
        public string CarrierCode { get; set; }
        public string TrackingUrl { get; set; }

    }
}
