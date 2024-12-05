using Generics.Common;
using HttpRequester;
using ShipStationApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipStationApi
{
    public class CarrierChecker
    {
        public string  CarrierCode { get; set; }
        public List<string> ServiceCode { get; set; }
    }
    public static class RateGeneratorHelper
    {

        static List<CarrierChecker> carrierCheckers = new List<CarrierChecker>()
        {
            new CarrierChecker()
            {
                CarrierCode = "stamps_com",
                ServiceCode = new List<string>()
                {
                    "usps_priority_mail"
                }
            },
            new CarrierChecker()
            {
                CarrierCode = "ups",
                ServiceCode = new List<string>()
                {
                    "ups_ground",
                    "ups_surepost_1_lb_or_greater"
                }
            },
            new CarrierChecker()
            {
                CarrierCode = "fedex",
                ServiceCode = new List<string>()
                {
                    "fedex_home_delivery",
                    "fedex_smartpost_parcel_select",
                    "fedex_ground"

                }
            },

        };

        static List<string> GeneralCarriers = new List<string>()
        {
            "stamps_com",
            "ups",
            "fedex"
        };
        static List<string> GeneralServices = new List<string>()
        {
            "USPS Priority Mail - Package",
            "USPS Priority Mail - Regional Rate Box A",
            "UPS® Ground",
            "FedEx Home Delivery®",
            "FedEx SmartPost parcel select",
            "FedEx Ground®"

        };
        static List<string> AmazonServices = new List<string>()
        {

        };
        static List<string> AmazonCarriers = new List<string>()
        {
            "amazon_shipping"
        };
        public static async Task GetAllRateOrdersAsync()
        {
            var orders = await ShipStationHandler.GetRateOrders(0);
            var aorders = orders.Orders.Where(o => o.TagIds == null || (!o.TagIds.Contains("130119")) ).ToList();
            foreach(var order in aorders)
            {
                if(order.PackageCode != null && order.PackageCode.Equals("flat_rate_padded_envelope"))
                {
                    continue;

                }
                
               
                if(order.Weight != null && order.Weight.Value < 15.15)
                {
                    continue;
                }
               
                    await DoForOrder(order);
                
                    
               
            }
            var carriers = ShipStationHandler.GetAllCarriers();
        }
        public static async Task DoForOrder(Order order)
        {

            //if (!order.OrderNumber.Equals("200010624191704"))
            //{
            //    return;
            //}

            List<ShipStationRateInfoDto> shipStationRateInfoDtos = new List<ShipStationRateInfoDto>();
            foreach (var carrier in carrierCheckers)
            {
                foreach (var carr in carrier.ServiceCode)
                {

                    

                    if (!carrier.CarrierCode.ToLower().Contains("fedex"))
                    {
                        shipStationRateInfoDtos.AddRange(await GetShippingRateOfOrderAsync(order, carrier.CarrierCode, ""));
                        break;
                    }
                    else
                    {
                        shipStationRateInfoDtos.AddRange(await GetShippingRateOfOrderAsync(order, carrier.CarrierCode, carr));
                    }

                }

            }

            if( shipStationRateInfoDtos.FirstOrDefault(f => f.ServiceName.Equals("USPS Priority Mail - Regional Rate Box A")) != null)
            {
                shipStationRateInfoDtos.FirstOrDefault(f => f.ServiceName.Equals("USPS Priority Mail - Regional Rate Box A")).OtherCost -= 0.5; 

            }

            var element = shipStationRateInfoDtos.OrderBy(f => f.OtherCost + f.ShipmentCost).FirstOrDefault();

            if (element != null)
            {
                Console.WriteLine(order.OrderNumber);
                if (element.CarrierName.Equals("fedex") && element.ServiceCode.Equals("fedex_smartpost_parcel_select"))
                {
                    element.CarrierName = "ups";
                    element.ServiceCode = "ups_surepost_1_lb_or_greater";

                    element.PackageName = "Package";

                }

                order.CarrierCode = element.CarrierName;
                order.ServiceCode = element.ServiceCode;
                if(string.IsNullOrWhiteSpace(order.PackageCode))
                {
                    order.PackageCode = "package";
                }
                bool addPackage = false;
                if (element.ServiceName.Equals("USPS Priority Mail - Regional Rate Box A"))
                {
                    order.PackageCode = "regional_rate_box_a";
                    
                }
                //if(order.PackageCode.Equals("regional_rate_box_a"))
                //{
                //    order.Dimensions = new Dimensions();
                //    order.Dimensions.Length = 0;
                //    order.Dimensions.Width = 0;
                //    order.Dimensions.Height = 0;
                //    addPackage = true;
                //}
                order.ShipByDate = null;
                order.CreateDate = null;
                order.ModifyDate = null;
                order.PaymentDate = null;
                order.ShipDate = null;
                //order.PackageCode = "gray_poly_bag";
                var ordd = await ShipStationHandler.UpdateOrder(order);
                if(ordd == null)
                {
                    return;
                }
                if( order.Dimensions != null || order.PackageCode.ToLower().Equals("mi_bpm_flat"))
                {
                    //order.PackageCode = "gray_poly_bag";
                    await ShipStationHandler.AddTagToOrder(order.OrderId.Value,
                                    ShipTag.R2P.GetEnumValue());
                }
            }
        }
        public static async Task<List<ShipStationRateInfoDto>> GetShippingRateOfOrderAsync(Order order,string carrier,string servicecode)
        {
            if (order.Weight != null && order.Weight.Value ==  0)
            {
                return new List<ShipStationRateInfoDto>();
            }
            var rateDto = new ShipStationRateInquiryDto()
            {
                CarrierCode = carrier,
                ToState = order.ShipTo.State,
                ToCountry = order.ShipTo.Country,
                ToPostalCode = order.ShipTo.PostalCode,
                ToCity = order.ShipTo.City,
                Weight = order.Weight,
                Residential = order.ShipTo.Residential,
                Confirmation = "delivery",
                FromPostalCode = "30093"
            };
            if(order.PackageCode != null && order.PackageCode.Equals("regional_rate_box_a"))
            {
                order.Dimensions = new Dimensions();
                order.Dimensions.Length = 10;
                order.Dimensions.Width = 7;
                order.Dimensions.Height = 5;
            }
           
            //if(order.Dimensions != null && order.Dimensions.Length == 10 && 
            //    order.Dimensions.Width == 7 && order.Dimensions.Height == 5 && carrier.Equals("stamps_com"))
            //{
                
            //    rateDto.PackageCode = "regional_rate_box_a";
            //}
            //rateDto.PackageCode = "";
            if(order.Dimensions != null)
            {
                rateDto.Dimensions = order.Dimensions;
                
            }
            if(!string.IsNullOrWhiteSpace(servicecode))
            {
                rateDto.ServiceCode = servicecode;
            }
            //if(carrier.ToLower().Contains("fedex"))
            //{
            //    rateDto.ServiceCode = "fedex_ground";
            //}
            
            var info = await ShipStationHandler.GetRates(rateDto);
            if(info != null)
            {
                info = info.Where(i => GeneralServices.Contains(i.ServiceName)).ToList();
                if(rateDto.Dimensions == null || (rateDto.Dimensions.Length != 10 || rateDto.Dimensions.Width != 7 || rateDto.Dimensions.Height != 5)  )
                {
                    info = info.Where(i => !i.ServiceName.Equals("USPS Priority Mail - Regional Rate Box A")).ToList();
                }
                info.ForEach(e => e.CarrierName = carrier);
                info.ForEach(e => e.PackageName = rateDto.PackageCode != null ? rateDto.PackageCode.ToString() : "");
            }
            else
            {
                info = new List<ShipStationRateInfoDto>();
            }
            if(order.ShipTo.Residential != null && order.ShipTo.Residential.HasValue && order.ShipTo.Residential.Value)
            {
                info = info.Where(f => !f.ServiceName.Equals("FedEx Ground")).ToList();
            }
            else
            {
                info = info.Where(f => !f.ServiceName.Equals("FedEx Home Delivery®")).ToList();
            }
            return info;
        }
    }
}
