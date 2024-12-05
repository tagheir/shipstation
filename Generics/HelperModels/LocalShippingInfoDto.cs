using System;
using System.Collections.Generic;
using System.Text;

namespace Generics.HelperModels
{
    public enum ShippingCarier
    {
        Fedex = 0,
        USPS = 1,
        UPSN = 2,
        Arc = 3,
    }
    public enum ShippingStatus{
        LabelsPrinted   = 0,    
        Shipped = 1,
    }
    public class LocalShippingInfoDto
    {
        public ShippingCarier Carrier { get; set; }    
        public string TrackingNumber { get; set; }    
        public DateTime LastUpdated { get; set; }

    }


    public class ExternalShippingStatus
    {
        public DateTime LastUpdated { get; set; }
        public ShippingStatus ShippingStatus { get; set; }
        public ShippingCarier Carrier { get; set; }
       
    }
}
