using Generics.Common;
using Generics.HelperModels;
using GmailHelper;
using HtmlAgilityPack;
using ShipStationApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShipStationApi
{
    public class OrderEmailProcessing
    {
        private const string EmailPurposeOutOfStock = "OutOfStock";
        private const string OrderDelay = "OrderDelay";
        private const string ShipDelay = "ShipDelay";

        public static string EmailPurposeOutOfStock1 => EmailPurposeOutOfStock;

        public static string OrderDelay1 => OrderDelay;

        public static string ShipDelay1 => ShipDelay;

        private static string BackOrderedTemplate(string FirstName)
        {
            return $"<h3>  Hello {FirstName}, </h3>"
+ @"<p> Thank you in advance for your patience. Rest assured, we are working hard to get your 
                                                    item(s) as quickly possible. Your backordered item(s) are expected to be available 
                                                    within 30 days. If your item(s) are not available within 30 days, your order will be 
                                                    canceled automatically, and you will be notified by email. </p>
                                                    <p> If you'd rather not wait for the items to come back in stock, you can cancel now them by 
                                                    replying this message,</p>
                                                    <p> Full refund will be issued within 48 hours. </p>
                                                    <p>  - Shopimo | Customer Support </p>
                                                    ";
        }

        private static string OrderDelayTemplate(string FirstName)
        {
            return $"<h3> Hello {FirstName}, </h3>" +
@" <p> Thank you for your recent order from Shopimo. It has come to our attention that due to 
                                                    an unforeseen circumstance, we are unable to meet the original estimated delivery 
                                                    date for your item mentioned above. </p>
                                                    
                                                    <p> We appreciate your patience and will do our best to have your order delivered without 
                                                    further delay. </p>
                                                    <p> If you have additional questions and/or concerns, please reply to this email.</p>
                                                    Shopimo | Customer support
                                                    ";
        }

        private static string ShipDelayTemplate(string FirstName, string TrackingLink, string TrackingId)
        {
            return $"<h3>  Hello {FirstName}, </h3>" +
$"<p> Thank you for your recent order with Shopimo." +
$" Please Note: Tracking on your order may not show any movement or initial scan for several days. " +
$"There’s no need to worry! Your order is secure and will move through the carrier network soon. </p>" +
$" <p> You can continue to track your order via the following link - <a href='{TrackingLink}'> {TrackingId} </a> online shopping and shipping volumes are expected to be at an all-time high. " +
$"As a result, small parcel delivery delays are expected. " +
$"We are working closely with our small parcel shipping providers daily. Our goal, and theirs, is to deliver your items as quickly and efficiently as possible." +
$" We apologize for the inconvenience this short term shipping capacity may cause you. </p>" +
$"<p> Thank you for your understanding,  </p>" +
$"Shopimo | Customer support";
        }

        public static async Task<bool> MuliTaskEmailerAsync()
        {
            Console.WriteLine("Choose Which Email Job");
            var output = Console.ReadLine();


            while (true)
            {
                var Awaitingorders = await ShipStationHandler.GetOrdererdOrders(1);
                switch (int.Parse(output))
                {
                    case 1:
                        var OrderedOrders = Awaitingorders.Orders.Where(e => e.TagIds.Contains(ShipTag.Ordered.GetEnumValue().ToString())
                   && (DateTime.Now - e.OrderDate.Value).TotalDays >= 7).ToList();
                        await ProcessOrderedDelayedOrders(OrderedOrders);
                        break;

                    case 2:
                        var Shippedorders = await ShipStationHandler.GetShippedOrders(DateTime.Now.AddDays(-6));
                        await ProcessShippedOrders(Shippedorders.Orders);
                        break;
                }
                System.Threading.Thread.Sleep(TimeSpan.FromHours(2));
            }






            //var OutOfStockOrders = Awaitingorders.Orders.Where(e => e.TagIds.Contains(ShipTag.OutOfStock.GetEnumValue().ToString())
            //    && (DateTime.Now - e.OrderDate.Value).TotalDays >= 9).ToList();

            //await ProcessOutOfStockOrders(OutOfStockOrders);








            return true;

        }
        public class OrderEmailSenderDto
        {
            public string CustomerEmail { get; set; }   
            public string OrderId { get; set; }
            public string OrderNumber { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
            public string Purpose { get; set; }

        }
        public static async Task ProcessOutOfStockOrders(List<Order> shipStationOrders)
        {
            foreach (var order in shipStationOrders)
            {
                var record = LayerDao.EmailRecorderDAO.GetEmailRecorder(order.OrderId.Value.ToString(), EmailPurposeOutOfStock1);
                if (record == null && (!order.CustomerEmail.Contains("nocxemail")))
                {
                    var status = GmailHelper.Mailer.SendEmail($" Your order {order.OrderNumber} is backordered",
                        BackOrderedTemplate(order.BillTo.Name.Split(" ")[0]),
                        order.CustomerEmail, new List<string>() { "Lior@shopimo.co", GmailHelper.GoogleBase.StatusOfficialEmail },
                        GmailHelper.GoogleBase.StatusOfficialEmail);



                    Console.WriteLine("Out Of Stock Email | " + order.OrderNumber);
                    LayerDao.EmailRecorderDAO.AddEmailRecord(new Generics.Db.EmailRecorderDto()
                    {
                        EmailPurpose = EmailPurposeOutOfStock1,
                        EmailStatus = "SENT",
                        OnCreated = DateTime.Now,
                        OrderId = order.OrderId.Value.ToString(),
                        OrderNumber = order.OrderNumber,
                    });
                }
            }
        }

        public static bool SendEmail(OrderEmailSenderDto emailSenderDto)
        {
            var record = LayerDao.EmailRecorderDAO.GetEmailRecorder(emailSenderDto.OrderId, emailSenderDto.Purpose);
            if (record == null && (!emailSenderDto.CustomerEmail.Contains("nocxemail")))
            {
                try
                {
                    var status = GmailHelper.Mailer.SendEmail(emailSenderDto.Subject, emailSenderDto.Body, emailSenderDto.CustomerEmail);
                }
                catch
                { 
                
                }
                
                Console.WriteLine($" {emailSenderDto.Purpose} | " + emailSenderDto.OrderNumber);
                LayerDao.EmailRecorderDAO.AddEmailRecord(new Generics.Db.EmailRecorderDto()
                {
                    EmailPurpose = emailSenderDto.Purpose,
                    EmailStatus = "SENT",
                    OnCreated = DateTime.Now,
                    OrderId = emailSenderDto.OrderId,
                    OrderNumber = emailSenderDto.OrderNumber,
                });
                return true;
            }
            return false;
        }
        public static async Task ProcessOrderedDelayedOrders(List<Order> shipStationOrders)
        {
            foreach (var order in shipStationOrders)
            {
                var record = LayerDao.EmailRecorderDAO.GetEmailRecorder(order.OrderId.Value.ToString(), EmailPurposeOutOfStock1);
                if (record == null && (!order.CustomerEmail.Contains("nocxemail")))
                {
                    var status = GmailHelper.Mailer.SendEmail($"Your order {order.OrderNumber} is delayed", OrderDelayTemplate(order.BillTo.Name.Split(" ")[0]), GmailHelper.GoogleBase.StatusOfficialEmail);
                    Console.WriteLine("Out Of Stock Email | " + order.OrderNumber);
                    LayerDao.EmailRecorderDAO.AddEmailRecord(new Generics.Db.EmailRecorderDto()
                    {
                        EmailPurpose = EmailPurposeOutOfStock1,
                        EmailStatus = "SENT",
                        OnCreated = DateTime.Now,
                        OrderId = order.OrderId.Value.ToString(),
                        OrderNumber = order.OrderNumber,
                    });
                }
            }
        }
        public static async Task ProcessShippedOrders(List<Order> shipStationOrders)
        {
            foreach (var order in shipStationOrders)
            {
                Console.WriteLine(order.OrderNumber);
                //var shipmentDetails = await ShipStationHandler.GetOrderShipmentDetails(order.OrderId.Value);
                if (order.CustomerEmail != null &&  (!order.CustomerEmail.Contains("nocxemail")))
                {
                    var shippingInfo = await GetLocalShippingInfo(order.OrderId.Value, order.OrderNumber);
                    if (shippingInfo.Carrier == ShippingCarier.USPS)
                    {
                        var link = $"https://tools.usps.com/go/TrackConfirmAction.action?tLabels={shippingInfo.TrackingNumber}";
                        var resp = await HttpRequester.HttpHandler.Request(new HttpRequester.HttpRequester()
                        {
                            Url = $"https://tools.usps.com/go/TrackConfirmAction.action?tLabels={shippingInfo.TrackingNumber}",
                            Method = HttpRequester.HttpMethod.GET,
                            IsAuthorized = false

                        });
                        var doc = new HtmlDocument();
                        doc.LoadHtml(resp.Response);
                        var dd = doc.DocumentNode.SelectSingleNode("//span[@class='tracking-number']");
                        var status = doc.DocumentNode.SelectSingleNode("//div[@class='delivery_status']//strong");
                        if (status != null && status.InnerText.Contains("Label"))
                        {
                            //GmailHelper.Mailer.SendEmail("Ship Delay",
                                //ShipDelayTemplate(order.BillTo.Name.Split(" ")[0], link, shippingInfo.TrackingNumber),
                                //GmailHelper.GoogleBase.StatusOfficialEmail);

                            SendEmail(new OrderEmailSenderDto()
                            {
                                Body = ShipDelayTemplate(order.BillTo.Name.Split(" ")[0], link, shippingInfo.TrackingNumber),
                                Subject = $"Important Update regarding your order {order.OrderNumber}",
                                Purpose =  ShipDelay,
                                OrderNumber = order.OrderNumber,
                                OrderId = order.OrderId.Value.ToString(),
                                CustomerEmail = order.CustomerEmail

                            });

                        }
                    }
                    else
                    if (shippingInfo.Carrier == ShippingCarier.Fedex)
                    {

                        continue;
                        var fed = await TrackingHandlers.FedExFetch(shippingInfo.TrackingNumber);
                        if (fed.Output.Packages.FirstOrDefault().KeyStatus != null && fed.Output.Packages.FirstOrDefault().KeyStatus.Equals("Label created"))
                        {
                            var link = "https://www.fedex.com/fedextrack/?trknbr=" + shippingInfo.TrackingNumber;

                            SendEmail(new OrderEmailSenderDto()
                            {
                                Body = ShipDelayTemplate(order.BillTo.Name.Split(" ")[0], link, shippingInfo.TrackingNumber),
                                Subject = $"Important Update regarding your order {order.OrderNumber}",
                                Purpose = ShipDelay,
                                OrderNumber = order.OrderNumber,
                                OrderId = order.OrderId.Value.ToString(),
                                CustomerEmail = order.CustomerEmail

                            });
                        }
                        System.Threading.Thread.Sleep(TimeSpan.FromMinutes(20));
                    }
                    else
                    if (shippingInfo.Carrier == ShippingCarier.UPSN)
                    {
                        var trackerInfo = await ShipStationApi.ShipStationHandler.GetUpsTrackingInfo(shippingInfo.TrackingNumber);
                        if (trackerInfo != null &&
                        trackerInfo.TrackDetails.FirstOrDefault().Milestones.FirstOrDefault().IsCurrent)
                        {

                            var link = $"https://wwwapps.ups.com/WebTracking/processRequest?HTMLVersion=5.0&Requester=NES&AgreeToTermsAndConditions=yes&loc=en_US&tracknum={shippingInfo.TrackingNumber}/trackdetails";
                              
                            SendEmail(new OrderEmailSenderDto()
                            {
                                Body = ShipDelayTemplate(order.BillTo.Name.Split(" ")[0], link, shippingInfo.TrackingNumber),
                                Subject = $"Important Update regarding your order {order.OrderNumber}",
                                Purpose = ShipDelay,
                                OrderNumber = order.OrderNumber,
                                OrderId = order.OrderId.Value.ToString(),
                                CustomerEmail = order.CustomerEmail

                            });

                        }
                    }
                }

            }

        }
        public static ShippingCarier GetShippingCarierFromServiceCode(string code)
        {

            if (code.Contains("usps"))
            {
                return ShippingCarier.USPS;
            }
            else
            if (code.Contains("fedex"))
            {
                return ShippingCarier.Fedex;
            }
            else
                if (code.Contains("ups"))
            {
                return ShippingCarier.UPSN;
            }
            else
            {
                return 0;
            }

        }
        public static async Task<LocalShippingInfoDto> GetLocalShippingInfo(long OrderId, string OrderNumber)
        {
            var shipmentDetails = await ShipStationHandler.GetOrderShipmentDetails(OrderId);
            var shippingInfo = shipmentDetails.Shipments.FirstOrDefault(s => s.OrderId == OrderId);
            if (shippingInfo == null)
            {
                var externalShippingInfo = await ShipStationHandler.GetExternalShipmentDto(OrderNumber);
                return new LocalShippingInfoDto()
                {
                    LastUpdated = externalShippingInfo.Fulfillments.FirstOrDefault().ShipDate.Value.DateTime,
                    Carrier = GetShippingCarierFromServiceCode(externalShippingInfo.Fulfillments.FirstOrDefault().CarrierCode.ToString().ToLower()),
                    TrackingNumber = externalShippingInfo.Fulfillments.FirstOrDefault().TrackingNumber
                };
            }
            else
            {
                return new LocalShippingInfoDto()
                {
                    LastUpdated = shippingInfo.ShipDate.Value.DateTime,
                    Carrier = GetShippingCarierFromServiceCode(shippingInfo.ServiceCode),
                    TrackingNumber = shippingInfo.TrackingNumber
                };
            }
        }


    }
}
