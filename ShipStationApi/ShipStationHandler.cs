
using Generics.Common;
using Generics.HelperModels;
using HttpRequester;
using ShipStationApi.Models;
using ShipStationApi.Models.Tracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ShipStationApi
{

    public enum ShipTag
    {
        Ordered = 101821,
        OutOfStock = 131765,
        SamsDropShipping = 132576,
        BJS = 111885,
        DsProblems = 114372,
        FulfillmentSKU = 124002,
        SystemGeneratedOrder = 124001,
        SystemGeneratedRate = 128923,
        R2P = 130119,
        INProcess = 132677 
    }

    public class ShipStationHandler
    {

        static string Username = "79f0b8ccab6848e9bf162f26e9bfb543";
        static string Password = "c5335f1db4644f2985212809060ef090";
        const string BASE_URL = "https://ssapi.shipstation.com/";

        static string Get_FulFillmentShippingInfo(string OrderNumber) => BASE_URL +$"fulfillments?orderNumber={OrderNumber}";
        static string GET_ORDERSByOrderNumber(string orderNumber) => BASE_URL + $"orders?orderNumber={orderNumber}";
        static string GET_ORDERS(string orderId) => BASE_URL + $"orders/{orderId}";
        static string UPDATE_ORDERS => BASE_URL + "orders/createorder";
        static string GETTAGS => BASE_URL + $"accounts/listtags";
        static string AddTagToOrderUrl = BASE_URL + "/orders/addtag";
        static string RemoveTagToOrderUrl = BASE_URL + "/orders/removetag";
        static string GET_CARRIERS = BASE_URL + "carriers";
        static string MARKORDER_ASSHIPPED = BASE_URL + "orders/markasshipped";
        static string GET_RATES = BASE_URL + "shipments/getrates";



        public static async Task<List<ShipStationCarriersInfoDto>> GetAllCarriers()
        {
            var resp = await HttpRequester.HttpHandler.Request(new HttpRequester.HttpRequester()
            {
                Url = "https://ss7.shipstation.com/apiV3/integrationsPlatform/displayInfo/providers",
                Method = HttpMethod.GET,
                IsAuthorized = false,


            });
            return Generics.Functions.FromJson<List<ShipStationCarriersInfoDto>>(resp.Response);
        }
        public static async Task<ShipStationOrders> GetShippedOrders(DateTime lastdate)
        {
            
            ShipStationOrders shipOrders = null;
            int pageNo = 1;
            while (true)
            {

                var date = lastdate.ToString("yyyy,MM,dd");
                var startDate = lastdate.AddDays(-3).ToString("yyyy,MM,dd");
                var url = $"https://ssapi.shipstation.com/orders?orderStatus=shipped&" +
                    $"sortBy=OrderDate&" +
                    $"sortDir=DESC&" +
                    $"createDateEnd={date}&" +
                    $"pagesize=500&page={pageNo}&" +
                    $"CreateDateStart={startDate}";

                var rq = GetRequest(url, HttpMethod.GET);
                var rsp = await HttpHandler.Request(rq);
                while (rsp == null || rsp.StatusCode.Contains("Too"))
                {
                    Thread.Sleep(6000);
                    rsp = await HttpHandler.Request(rq);
                }
                var order = rsp.Response.ParseJson<ShipStationOrders>();
                if (shipOrders == null)
                {
                    shipOrders = order;
                }
                else
                {
                    if (order.Orders.Count == 0)
                    {
                        break;
                    }
                    shipOrders.Orders.AddRange(order.Orders);
                }
                pageNo++;
            }
            return shipOrders;
        }
        public static async Task<List<ShipCarriersDto>> GetShippingCarriers()
        {
            var resp = (await HttpHandler.Request(GetRequest(GET_CARRIERS)));
            while (resp == null || resp.StatusCode.Contains("Too"))
            {
                Thread.Sleep(5000);
                resp = (await HttpHandler.Request(GetRequest(GET_CARRIERS)));
            }
            var carriers = ShipCarriersDto.FromJson(resp.Response);
            return carriers;
        }
        public static async Task<List<ShipStationCarrierServiceInfoDto>> GetShippingCarriersService(string carriercode)
        {
            var resp = (await HttpHandler.Request(GetRequest(GET_CARRIERS+"/listservices?carrierCode="+carriercode)));
            while (resp == null || resp.StatusCode.Contains("Too"))
            {
                Thread.Sleep(5000);
                resp = (await HttpHandler.Request(GetRequest(GET_CARRIERS)));
            }
            var carriers = ShipStationCarrierServiceInfoDto.FromJson(resp.Response);
            return carriers;
        }
        public static async Task<List<ShipStationCarrierServiceInfoDto>> GetShippingCarriersPackages(string carriercode)
        {
            var resp = (await HttpHandler.Request(GetRequest(GET_CARRIERS + "/listpackages?carrierCode=" + carriercode)));
            while (resp == null || resp.StatusCode.Contains("Too"))
            {
                Thread.Sleep(5000);
                resp = (await HttpHandler.Request(GetRequest(GET_CARRIERS)));
            }
            var carriers = ShipStationCarrierServiceInfoDto.FromJson(resp.Response);
            return carriers;
        }
        public static async Task<bool> UpdateOrderTracking(Generics.HelperModels.OrderShipperDto OrderShipperDto)
        {
            var resp = (await HttpHandler.Request(GetRequest(GET_CARRIERS)));
            while (resp == null || resp.StatusCode.Contains("Too"))
            {
                Thread.Sleep(5000);
                resp = (await HttpHandler.Request(GetRequest(GET_CARRIERS)));
            }
            var carriers = ShipCarriersDto.FromJson(resp.Response);
            var result = (OrderShipperDto.CarrierCode == null || OrderShipperDto.CarrierCode.IsEmpty()) ?
                carriers.FirstOrDefault(f => OrderShipperDto.TrackingUrl.Contains(f.Code))?.Code : OrderShipperDto.CarrierCode;
            switch (result)
            {
                case "UPSN":
                    result = "ups";
                    break;
            }
            if (result == null)
            {
                var allcarriers = await ShipStationHandler.GetAllCarriers();
                result = allcarriers.FirstOrDefault(f => OrderShipperDto.CarrierCode == null || OrderShipperDto.CarrierCode.IsEmpty() ? OrderShipperDto.TrackingUrl.ToLower().Contains(f.Name.ToLower()) : OrderShipperDto.CarrierCode.ToLower().Contains(f.Name.ToLower())).ApiCode;
            }
            var shipStationShippingInfo = new ShipOrderShipperDto()
            {
                NotifySalesChannel = true,
                NotifyCustomer = false,
                OrderId = long.Parse(OrderShipperDto.ShipstationOrderId),
                ShipDate = OrderShipperDto.TrackingDate,
                TrackingNumber = OrderShipperDto.TrackingId,
                CarrierCode = result

            };
            var rq = GetRequest(MARKORDER_ASSHIPPED, HttpMethod.Post);
            rq.Body = shipStationShippingInfo.ConvertToJson();

            resp = await HttpRequester.HttpHandler.Request(rq);
            while (resp == null || resp.StatusCode.Contains("Too"))
            {
                Thread.Sleep(5000);
                resp = await HttpHandler.Request(rq);
            }
            return true;
        }
        public static async Task<Order> RemoveTagToOrder(long orderId, long tagId)
        {
            var body = $"{{\"orderId\": {orderId}, \"tagId\": {tagId} }}";
            var rq = GetRequest(RemoveTagToOrderUrl, HttpMethod.Post);
            rq.Body = body;

            var resp = await HttpRequester.HttpHandler.Request(rq);
            while (resp == null || resp.StatusCode.Contains("Too"))
            {
                Thread.Sleep(5000);
                resp = await HttpHandler.Request(rq);
            }
            return await FetchOrder(orderId.ToString());
        }
        public static async Task<ShipStationOrders> GetUpcomingOrders(int PageNo = 1)
        {
            var url = $"https://ssapi.shipstation.com/orders/listbytag?tagId=124001&orderStatus=awaiting_shipment&pageSize=500&page={PageNo}";
            var rq = GetRequest(url, HttpMethod.GET);
            var rsp = await HttpHandler.Request(rq);
            var order = rsp.Response.ParseJson<ShipStationOrders>();
            return order;

        }
        public static async Task<UpsTrackingDetailsDto> GetUpsTrackingInfo(string trackingId)
        {
            var query = $"{{\"Locale\":\"en_US\",\"TrackingNumber\":[\"{trackingId}\"],\"Requester\":\"st/trackdetails\",\"returnToValue\":\"\"}}";
            var request = GetRequest("https://wwwapps.ups.com/track/api/Track/GetStatus?loc=en_US", HttpMethod.Post);
            request.Params.Add(new HeaderParams()
            {
                Key = "cookie",
                Value = "X-CSRF-TOKEN=CfDJ8Jcj9GhlwkdBikuRYzfhrpLlvN9uOEmmBKn-1rTKi1psQB81GM-OE23khtT2hwtO10jy2gVruedIGAhEJVfnbSIYZZbjP9SAKgA1GIx_NzW3Ayc6W7Q6zp2pgqV2CVA_ahZVG5vOS1VNdc3221iXPlM; PIM-SESSION-ID=aydgTYk6X0E63875; at_check=true; AMCVS_036784BD57A8BB277F000101%40AdobeOrg=1; CONSENTMGR=consent:true%7Cts:1647596568462; _gcl_au=1.1.257264447.1647596570; s_cc=true; aam_cms=segments%3D22945447; aam_uuid=82797760637126087031986864996273015059; _fbp=fb.1.1647596573043.1983336111; bm_sz=95D17E746E13717373B43B446442AA52~YAAQfWR7XLFjR5t/AQAANqk7nQ+BruN2r/zmtrYuJEZuooLCdmE1x0Zt5KjpJjYvByAm3NwQvqOEsDgyElc2SF5cqvReKh/D3k04xNOUt+Tl641nMFZYn3Dur4P4gVUWzVQ36hc2B86lN5t8ktUzGoW9BlbEP48RfzMnIpj59Hcqa28OTzyDef7Jwhdadh3DdwBVeL2QUYBHVzlMXC0dDlrQHqfBYdW/ok5AQAq0enfYujFMJ+H9y/08jeJR5iYJjdZwixxduybBvxbWmQUyFu+cX/mJ1GWr47m0DfUyG3+5CVJAhdZXzq1oN3zrbzUryGZ8HkOrP5I=~4408369~3748404; mboxEdgeCluster=38; ak_bmsc=5164B115170400A40EB13B4CF1612150~000000000000000000000000000000~YAAQfWR7XCNkR5t/AQAA+bE7nQ/i8xnGg29ctkxtxEg+SH5kXDkfqkNkwe1zC0cFFPanNIANoYS1btREvRy26JRI5huOk9rLJbRcJ2YYf7k3n9e0cd81witB7TpkuXbbyHMnd9h8KuCCgqjH+brQOssSljjSU3Vn6y09OgQnqmq/O/jCUeN78wfVdMqQ/k49JPuNb6/ZTZ7Uqb0U0hztfZ10UtasDTw7BWhgy2CIzT/3CJcNjapWNS37+Vr23mkHY/gXukU6pgGpbFtpO4Sy1xwu2q9HwUsJiOhwZfWYY3tpP0fGfP3NQgssuwUq+UmTADpLvSgwXYPDcua7eUoDjuuVfvlZo2Kh/swK/cj/h8uwF0+KHLagkAYg+xzD0pp3PuPdHFZF1Lu2FGki8Iw4r9dbzFdvB39PXs1VDWSy0Izb8V7xMPsXnYApzcajQa5l5tP2zM4kAXhVACMSydfVU1QModTKbKC7sGgy4RrbCGpV2bjVWDoFnXM=; AMCV_036784BD57A8BB277F000101%40AdobeOrg=-1124106680%7CMCIDTS%7C19070%7CMCMID%7C82327900329973126351960703782303293655%7CMCAAMLH-1648215208%7C3%7CMCAAMB-1648215208%7CRKhpRz8krg2tLO6pguXWp5olkAcUniQYPHaMWWgdJ3xzPWQmdj0y%7CMCOPTOUT-1647617608s%7CNONE%7CMCCIDH%7C-544127062%7CvVersion%7C5.2.0; s_vnum=1648753200947%26vn%3D2; s_invisit=true; dayssincevisit_s=Less%20than%201%20day; s_nr=1647610423444-Repeat; dayssincevisit=1647610423446; _abck=CCB796C27412EE2482D1663C2BA7CEFE~0~YAAQfWR7XDlnR5t/AQAAAfU7nQcy4rQOooJtgjAG4kH3B1IXndeIm9LyWpIKHRXjJ+y85Cj7MjjuGjRKv6z13LKrQZRUG3m3FajDyT7cBuq4Uf4uIJUpvS42lTAXeju8vCd1u+1MwB0v/EcLu1Ur9AHE3ypJnHMYqQ1tBClUxfs4j5sdw83nyLoZMjlrDxEBxl0dgTqoW+qaQsi4xT12TIa6A+jtyqFr4U0VLzNDHyAN3f0/g4d7S2MYB766XIgHBYRve2nedfaqlhWllKPJriLdZHNjJByDwQm0KagB1Zm6XU43qfelzSuH39248AdvN8z6ANaB3/c0WA8U7IEdl54dpStBNcDtrX7huhjosWsv+6kasut+3okQar2/vCuqDAsbMuFLrgZcUyGU5PXcGKSAvnZN~-1~-1~-1; X-XSRF-TOKEN-ST=CfDJ8Jcj9GhlwkdBikuRYzfhrpKPN_v-1orLLi0Vz5ZD95AUtCGSm1KSG3GIZIJIjFd2chiHCH3odMJf_hj-KBehAf6TFYyty5sich_RNPk83-uYknLxJA6piKOd3t0iGqC0mwtoNfTL4eqHJj93bhcUCtU; mbox=PC#ce7dd7e93ae242b588927c90ae52c56b.38_0#1710855230|session#a3b42f00f0fe4f7a9cc4833b4098aede#1647612265;"
            });
            request.Params.Add(new HeaderParams()
            {
                Key = "x-xsrf-token",
                Value = "CfDJ8Jcj9GhlwkdBikuRYzfhrpKPN_v-1orLLi0Vz5ZD95AUtCGSm1KSG3GIZIJIjFd2chiHCH3odMJf_hj-KBehAf6TFYyty5sich_RNPk83-uYknLxJA6piKOd3t0iGqC0mwtoNfTL4eqHJj93bhcUCtU"

            });
            request.Body = query;
            var resp = await HttpHandler.Request(request);
            if (resp != null)
            {
                return resp.Response.ParseJson<UpsTrackingDetailsDto>();
            }
            return null;

        }
        public static async Task<ShipStationExternalShipmentDto> GetExternalShipmentDto(string trackingId)
        {
            
            var request = GetRequest(Get_FulFillmentShippingInfo(trackingId),HttpMethod.GET);
            var response = await HttpRequester.HttpHandler.Request(request);
            return ShipStationExternalShipmentDto.FromJson(response.Response);
        }
        public static async Task<ShipStationShipmentDto> GetOrderShipmentDetails(long orderId)
        {
            var url = $"https://ssapi.shipstation.com/shipments?orderId={orderId}";
            var request = GetRequest(url, HttpMethod.GET);
            var resp = await HttpRequester.HttpHandler.Request(request);
            return ShipStationShipmentDto.FromJson(resp.Response);
        }
        public static async Task GetFedExTrackingApiAsync()
        {

        }
        public static async Task<List<Order>> GetOutOfStockOrders(ShipTag tag)
        {
            var orders = await GetOrdererdOrders(1);
            return orders.Orders.Where(o => o.TagIds.Contains(ShipTag.OutOfStock.GetEnumValue().ToString())
            &&
            o.TagIds.Contains(tag.GetEnumValue().ToString())).ToList();
        }
        public static async Task<ShipStationOrders> GetOrdererdOrders(int pageNo, string OrderStatus = "awaiting_shipment")
        {
            ShipStationOrders shipOrders = null;
            pageNo = 1;
            while (true)
            {
                var url = $"https://ssapi.shipstation.com/orders/listbytag?tagId={ShipTag.SamsDropShipping.GetEnumValue()}&orderStatus={OrderStatus}&pageSize=500&page={pageNo}";
                var rq = GetRequest(url, HttpMethod.GET);
                var rsp = await HttpHandler.Request(rq);
                while (rsp == null || rsp.StatusCode.Contains("Too"))
                {
                    Thread.Sleep(6000);
                    rsp = await HttpHandler.Request(rq);
                }
                var order = rsp.Response.ParseJson<ShipStationOrders>();
                if (shipOrders == null)
                {
                    shipOrders = order;
                }
                else
                {
                    if (order.Orders.Count == 0)
                    {
                        break;
                    }
                    shipOrders.Orders.AddRange(order.Orders);
                }
                pageNo++;
            }

            

            return shipOrders;

        }
        public static async Task<ShipStationOrders> GetRateOrders(int pageNo, string OrderStatus = "awaiting_shipment")
        {
            ShipStationOrders shipOrders = null;
            pageNo = 1;
            while (true)
            {
                var url = $"https://ssapi.shipstation.com/orders?orderStatus={OrderStatus}&pageSize=500&page={pageNo}";
                var rq = GetRequest(url, HttpMethod.GET);
                var rsp = await HttpHandler.Request(rq);
                while (rsp == null || rsp.StatusCode.Contains("Too"))
                {
                    Thread.Sleep(6000);
                    rsp = await HttpHandler.Request(rq);
                }
                var order = rsp.Response.ParseJson<ShipStationOrders>();
                if (shipOrders == null)
                {
                    shipOrders = order;
                }
                else
                {
                    if (order.Orders.Count == 0)
                    {
                        break;
                    }
                    shipOrders.Orders.AddRange(order.Orders);
                }
                pageNo++;
            }



            return shipOrders;

        }
        public static async Task<Order> UpdateOrderNotes(long orderId, string notes)
        {
            var order = await FetchOrder(orderId.ToString());
            if (order == null)
            {
                Thread.Sleep(10000);
                order = await FetchOrder(orderId.ToString());
            }
            order.InternalNotes = notes;
            var rq = GetRequest(UPDATE_ORDERS, HttpMethod.Post);
            rq.Body = order.ConvertToJson();
            var resp = await HttpHandler.Request(rq);
            while (resp.StatusCode.Contains("Too"))
            {
                Thread.Sleep(5000);
                resp = await HttpHandler.Request(rq);
            }
            return await FetchOrder(orderId.ToString());


        }
        public static async Task<Order> UpdateOrderItemsQuantity(long orderId,List<Item> items)
        {
            var order = await FetchOrder(orderId.ToString());
            while(order == null)
            {
                Thread.Sleep(10000);
                order = await FetchOrder(orderId.ToString());
            }
            items.ForEach(e =>
            {
                order.Items.FirstOrDefault(f => f.Sku.Equals(e.Sku)).Quantity = e.Quantity;
            });
            var rq = GetRequest(UPDATE_ORDERS, HttpMethod.Post);
            rq.Body = order.ConvertToJson();
            var resp = await HttpHandler.Request(rq);
            while (resp.StatusCode.Contains("Too"))
            {
                Thread.Sleep(5000);
                resp = await HttpHandler.Request(rq);
            }
            return await FetchOrder(orderId.ToString());
        }
        public static async Task<Order> UpdateOrderPricing(long orderId, double price)
        {
            Console.WriteLine("Price Update: " + price.ToString());
            var order = await FetchOrder(orderId.ToString());
            if (order == null)
            {
                Thread.Sleep(10000);
                order = await FetchOrder(orderId.ToString());
            }
            order.AdvancedOptions.CustomField2 = price;
            var rq = GetRequest(UPDATE_ORDERS, HttpMethod.Post);
            rq.Body = order.ConvertToJson();
            var resp = await HttpHandler.Request(rq);
            while (resp.StatusCode.Contains("Too"))
            {
                Thread.Sleep(5000);
                resp = await HttpHandler.Request(rq);
            }
            return await FetchOrder(orderId.ToString());


        }

        public static async Task<Order> UpdateOrder(Order order)
        {
            
            
            var rq = GetRequest(UPDATE_ORDERS, HttpMethod.Post);
            rq.Body = order.ConvertToJson();
            var resp = await HttpHandler.Request(rq);
            while (resp.StatusCode.Contains("Too"))
            {
                Thread.Sleep(5000);
                resp = await HttpHandler.Request(rq);
            }
            if(resp.StatusCode.Equals("BadRequest"))
            {
                return null;
            }
            return await FetchOrder(order.OrderId.ToString());


        }
        public static async Task<List<ShipStationRateInfoDto>> GetRates(ShipStationRateInquiryDto inquiryDto)
        {
            
            
            var rq = GetRequest(GET_RATES, HttpMethod.Post);
            rq.Body = inquiryDto.ConvertToJson();
            var resp = await HttpHandler.Request(rq);
            while (resp.StatusCode.Contains("Too"))
            {
                Thread.Sleep(5000);
                resp = await HttpHandler.Request(rq);
            }
            return ShipStationRateInfoDto.FromJson(resp.Response);

        }

        public static async Task<Order> AddTagToOrder(long orderId, long tagId)
        {
            var body = $"{{\"orderId\": {orderId}, \"tagId\": {tagId} }}";
            var rq = GetRequest(AddTagToOrderUrl, HttpMethod.Post);
            rq.Body = body;
            var resp = await HttpRequester.HttpHandler.Request(rq);
            while (resp == null || resp.StatusCode.Contains("Too"))
            {
                Thread.Sleep(5000);
                resp = await HttpHandler.Request(rq);
            }
            return await FetchOrder(orderId.ToString());
        }
        public static HttpRequester.HttpRequester GetRequest(string url, HttpMethod httpMethod = HttpMethod.GET)
        {
            var httprequest = new HttpRequester.HttpRequester()
            {
                IsAuthorized = true,
                Method = httpMethod,
                Password = Password,
                UserName = Username,
                Url = url,
                Params = new List<HeaderParams>()
                {
                    HeaderParams.ContentApplicationJson()
                }

            };
            return httprequest;
        }
        public static async Task<Order> FetchOrder(string orderId)
        {
            var url = GET_ORDERS(orderId);
            var httprequest = new HttpRequester.HttpRequester()
            {
                IsAuthorized = true,
                Method = HttpRequester.HttpMethod.GET,
                Password = Password,
                UserName = Username,
                Url = url
            };
            var resposnse = await HttpRequester.HttpHandler.Request(httprequest);
            var order = resposnse.Response.ParseJson<Order>();
            return order;

        }
        public static async Task<Order> FetchOrdersByOrderNumber(string orderNumber)
        {
            var url = GET_ORDERSByOrderNumber(orderNumber);
            var httprequest = new HttpRequester.HttpRequester()
            {
                IsAuthorized = true,
                Method = HttpRequester.HttpMethod.GET,
                Password = Password,
                UserName = Username,
                Url = url
            };
            var resposnse = await HttpHandler.Request(httprequest);
            while (resposnse == null || resposnse.StatusCode.Equals("TooManyRequests"))
            {
                Thread.Sleep(3000);
                resposnse = await HttpHandler.Request(httprequest);
            }
            var order = resposnse.Response.ParseJson<ShipStationOrders>();
            return order.Orders.FirstOrDefault();

        }
        static HttpRequester.HttpRequester GetRequester(string Url)
        {
            return new HttpRequester.HttpRequester()
            {
                IsAuthorized = true,
                Method = HttpRequester.HttpMethod.GET,
                Password = Password,
                UserName = Username,
                Url = Url,
                Params = new List<HeaderParams>()
                {
                    HeaderParams.ContentApplicationJson()
                }
            };
        }
        public static async Task<List<Shipstationtags>> GetAllTags()
        {
            var httpRequest = GetRequester(GETTAGS);
            var resp = await HttpRequester.HttpHandler.Request(httpRequest);
            if (resp.Response != null)
            {
                Shipstationtags.tags = resp.Response.ParseJson<List<Shipstationtags>>();
                return resp.Response.ParseJson<List<Shipstationtags>>();
            }
            return null;
        }
        public static async void FetchAllOrders()
        {

        }
        public static async void RefreshStore()
        {
            var rq = GetRequest("https://ssapi.shipstation.com/stores/refreshstore", HttpMethod.Post);
            rq.Body = "{}";
            var resp = await HttpHandler.Request(rq);
            while (resp.StatusCode.Contains("Too"))
            {
                Thread.Sleep(5000);
                resp = await HttpHandler.Request(rq);
            }
        }

    }
}
