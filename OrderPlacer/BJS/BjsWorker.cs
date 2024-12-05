using Generics;
using Generics.HelperModels;
using HttpRequester;
using OrderPlacer.Bjs.Models;
using OrderPlacer.BJS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OrderPlacer.BJS
{
    public static class BjsWorker
    {
        public static CookieContainer cookie;
        static readonly string ACCEPTLANGUAGE = "en-US,en;q=0.9";
        static readonly string REFERER = "https://www.bjs.com/";
        static readonly string USERAGENT = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/99.0.4844.51 Safari/537.36";

        static readonly int StoreId = 10201;
        static readonly int PhysicalStoreId = 0152;
        static readonly int LangId = -1;
        static readonly int CatalogId = 10201;
        static readonly int CalculateOrder = 1;

        // URLS

        static readonly string SEARCH_PRODUCT = "https://bjswholesale-cors.groupbycloud.com/api/v1/search";
        static readonly string DELETE_ITEM_FROM_CART = "https://api.bjs.com/digital/live/api/v1.1/storeId/10201/shoppingCartItem";
        static string CURRENT_USER_CART = "https://api.bjs.com/digital/live/api/v2.0/order?orderId=.&action=qtyupd&storeId=10201&zipCode=30093&physicalStoreId=0152";
        static string GET_ORDERS = "https://api.bjs.com/digital/live/api/v1.2/orderhistorylist";
        static string ORDER_DETAIL = "https://api.bjs.com/digital/live/api/v1.3/orderdetails";
        public static HttpRequester.HttpRequester GetAuthorizedRequest<T>(string url, HttpMethod httpMethod, T obj, bool isAuthorized = true) where T : class
        {
            return new HttpRequester.HttpRequester()
            {
                Url = url,
                IsAuthorized = isAuthorized,
                Params = new System.Collections.Generic.List<HeaderParams>()
                {
                    HeaderParams.GetHeaderParam(HeaderType.useragent,USERAGENT),
                    HeaderParams.ContentApplicationJson(),
                    HeaderParams.GetHeaderParam(HeaderType.acceptlanguage,ACCEPTLANGUAGE),
                    HeaderParams.GetHeaderParam(HeaderType.referer,REFERER),
                },
                Body = Generics.Functions.ToJson(obj),
                Method = httpMethod,
                CookieContainer = cookie
            };
        }
        public static HttpRequester.HttpRequester GetAuthorizedRequest(string url, HttpMethod httpMethod, string body = null, bool isAuthorized = true)
        {
            var request = new HttpRequester.HttpRequester()
            {
                Url = url,
                IsAuthorized = isAuthorized,
                Params = new System.Collections.Generic.List<HeaderParams>()
                {
                    HeaderParams.GetHeaderParam(HeaderType.useragent,USERAGENT),
                    HeaderParams.ContentApplicationJson(),
                    HeaderParams.GetHeaderParam(HeaderType.acceptlanguage,ACCEPTLANGUAGE),
                    HeaderParams.GetHeaderParam(HeaderType.referer,REFERER),
                },

                Method = httpMethod,
                CookieContainer = cookie
            };
            if (body != null)
            {
                request.Body = body;
            }
            return request;
        }
        public static async Task<T> InvokeRequestAsync<T>(HttpRequester.HttpRequester requester) where T : class
        {
            try
            {
                var result = await HttpHandler.Request(requester);
                if (result.StatusCode.Equals("OK"))
                {
                    return result.Response.FromJson<T>();
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }

        }
        public static async Task<bool> InvokeRequestAsync(HttpRequester.HttpRequester requester)
        {
            try
            {
                var result = await HttpHandler.Request(requester);
                if (result.StatusCode.Equals("OK"))
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static async Task<string> InvokeRequest(HttpRequester.HttpRequester requester)
        {
            try
            {
                var result = await HttpHandler.Request(requester);
                if (result == null)
                {
                    return null; ;
                }
                if (result.StatusCode.Equals("OK"))
                {
                    return result.Response;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }

        }
        public static Dictionary<long, OrderHistoryDetailDto> BjsOrdersHistory = new Dictionary<long, OrderHistoryDetailDto>();
        public static async Task<OrderHistoryDetailDto> GetBjsOrderHistoryAsync(string orderId, int pageNo = 1)
        {
            var bjsOrderHistory = new BjsOrderHistoryQueryDto(pageNo);
            OrderHistoryDetailDto order = new OrderHistoryDetailDto();
            if (BjsOrdersHistory.ContainsKey(bjsOrderHistory.Page.PageNo ?? 0))
            {
                order = BjsOrdersHistory.GetValueOrDefault(bjsOrderHistory.Page.PageNo ?? 0);
            }
            else
            {
                var request = GetAuthorizedRequest(GET_ORDERS, HttpMethod.Post, bjsOrderHistory);
                var resp = await InvokeRequest(request);
                if (resp == null)
                {
                    return null;
                }
                order = OrderHistoryDetailDto.FromJson(resp);
            }
            return order;
        }
        public static async Task<OrderShipperDto> GetOrderShippingInfo(string orderId)
        {
            var order = await GetBjsOrderHistoryAsync(orderId);
            var output = order.Output.OrderList.Order.FirstOrDefault(o => o.OrderNo.ToString().Equals(orderId.Trim()));
            if (output == null || output.OrderStatusDesc == "" || output.OrderStatusDesc.ToLower().Equals("processing"))
            {
                while (order.IsLastPage.Equals("N") && output == null)
                {
                    int pgno = Convert.ToInt32(order.PageNumber);
                    order = await GetBjsOrderHistoryAsync(orderId, ++pgno);
                    output = order.Output.OrderList.Order.FirstOrDefault(o => o.OrderNo.ToString().Equals(orderId.Trim()));
                }
                if (output == null)
                {
                    return null;
                }
            }
            if (output == null || output.OrderStatusDesc == "" || output.OrderStatusDesc.ToLower().Equals("processing"))
            {
                return null;
            }
            var bjsOrderDetailQuery = new BjsOrderQueryDto(output.OrderHeaderKey);
            var request = GetAuthorizedRequest(ORDER_DETAIL, HttpMethod.Post, bjsOrderDetailQuery);
            var orderDetail = await InvokeRequestAsync<BjsTrackingOrderDto>(request);
            if (order != null && orderDetail != null)
            {
                return new OrderShipperDto()
                {
                    CarrierCode = orderDetail.Order.OrderLines.FirstOrDefault().ShipmentLines.FirstOrDefault().Shipment
                    .Scac,
                    PlatformOrderId = orderDetail.Order.OrderNo.ToString(),
                    TrackingId = orderDetail.Order.OrderLines.FirstOrDefault().ShipmentLines.FirstOrDefault().Shipment
                    .Containers.FirstOrDefault().TrackingNo,
                    TrackingDate = DateTimeOffset.Parse( output
                   .OrderLines.FirstOrDefault().OrderStatuses.OrderStatus.Details.ExpectedShipmentDate.Value.DateTime.ToString())
                };
            }
            return null;
        }
        public static async Task AddItemToCart(string itemId, int quantity)
        {
            var addToCart = new BjsAddToCartDto()
            {
                ActiveChoice = "",
                CatalogId = "",
                Comment = "",
                ErrorView = "",
                IsExpired = "",
                ItemsCheckInCartReq = new System.Collections.Generic.List<object>(),
                ItemType = "",
                OrderId = ".",
                PurchaseFlow = "",
                QofferId = new System.Collections.Generic.List<object>(),
                Url = "ViewMiniCart",
                ItemList = new System.Collections.Generic.List<ItemList>()
                {
                    new ItemList()
                    {
                        CatEntryId =  itemId,
                        PartNumber = "",
                        PhysicalStoreId = "0152",
                        PickUpInStore = "N",
                        Quantity =quantity,
                        ServiceZipcode = 30344
                    }
                }
            };
            var rq = GetAuthorizedRequest<BjsAddToCartDto>("https://api.bjs.com/digital/live/api/v1.2/storeId/10201/shoppingCartItem", HttpMethod.Post, addToCart);
            await InvokeRequest(rq);

        }
        public static async Task<BjsProductInfoDto> GetProductInfo(string id)
        {
            var url = $"https://api.bjs.com/digital/live/api/v1.0/product/price/10201?productId={id}&pageName=PDP&clubId=0152";
            return await InvokeRequestAsync<BjsProductInfoDto>(GetAuthorizedRequest(url, HttpMethod.GET));
        }
        public static async Task<bool> RemoveItemFromCart(long ItemId)
        {
            var cartDelete = new BjsDeleteItemFromCartDto
            {
                CalculateOrder = CalculateOrder,
                CatalogId = CatalogId,
                LangId = LangId,
                StoreId = StoreId,
                OrderItemId = ItemId,
                OrderId = "."
            };
            var request = GetAuthorizedRequest(DELETE_ITEM_FROM_CART, HttpMethod.Delete, cartDelete);
            return await InvokeRequestAsync(request);
        }
        public static async Task<BjsCartResponseDto> GetCurrentCart()
        {
            var request = GetAuthorizedRequest(CURRENT_USER_CART, HttpMethod.GET);
            return await InvokeRequestAsync<BjsCartResponseDto>(request);

        }
        public static async Task<BjsSearchProductResponseDto> SearchProductAsync(string itemId)
        {
            var dto = new BjsSearchProductQueryDto()
            {
                PageSize = 120,
                Query = itemId,
                Collection = "productionB2CProducts",
                Skip = 0,
                Fields = new System.Collections.Generic.List<string>()
                {
                    "*"
                },
                Area = "BCProduction"

            };
            var request = GetAuthorizedRequest(SEARCH_PRODUCT, HttpMethod.Post, dto);
            return await InvokeRequestAsync<BjsSearchProductResponseDto>(request);
        }
        public static async Task<OrderResponseDto> Do(PlaceOrderDto orderDto)
        {
            OrderResponseDto orderResponseDto = new OrderResponseDto();
            orderResponseDto.IsAddressError = false;
            orderResponseDto.IsAuthorizationError = false;
            orderResponseDto.IsSuccessfull = false;

            var cart = await GetCurrentCart();
            if (cart != null && cart.RaOrderDataBeanOutput != null && cart.RaOrderDataBeanOutput.CustomOrderItems != null)
            {
                foreach (var ck in cart.RaOrderDataBeanOutput.CustomOrderItems.Online)
                {
                    await RemoveItemFromCart(ck.OrderItem.OrderItemId ?? 0);
                }
            }
            foreach (var items in orderDto.Items)
            {
                var resp = await SearchProductAsync(items.Sku);
                if (resp != null && resp.Records.Count > 0)
                {
                    string id = null;
                    try{
                        id = resp.Records.FirstOrDefault(f => f.AllMeta.VisualVariant.
                    FirstOrDefault(g => g.NonvisualVariant.FirstOrDefault(h => h.ItemPartnumber.Contains(long.Parse(items.Sku))) != null) != null)
                         .AllMeta.Id.ToString();
                    }
                    catch(Exception ex)
                    {
                        orderResponseDto.ErrorType = OrderResponseErrorType.OutOfStock;
                        return orderResponseDto;
                    }

                    var pr = await GetProductInfo(id);
                    //var bj = pr.BjsClubProduct.FirstOrDefault(bj => bj.CatentryId.Equals(items.Sku));
                    //if(bj == null)
                    //{
                    //    orderResponseDto.ErrorType = OrderResponseErrorType.OutOfStock;
                    //    break;
                    //}
                    await AddItemToCart(pr.BjsClubProduct.FirstOrDefault().CatentryId, items.Quantity);
                   
                    //var record = resp.Records.FirstOrDefault(f => f.Id.Equals(items.Sku));
                }
                else
                {
                    orderResponseDto.ErrorType = OrderResponseErrorType.OutOfStock;
                    return orderResponseDto;
                }
            }
            return orderResponseDto;

        }
        public static async Task<bool> Verifyoutofstock(PlaceOrderDto orderDto)
        {

            bool outOfStock = true;
            foreach (var items in orderDto.Items)
            {
                var resp = await SearchProductAsync(items.Sku);
                if (resp != null && resp.Records.Count > 0)
                {
                    try
                    {
                        var id = resp.Records.FirstOrDefault(f => f.AllMeta.VisualVariant.
                    FirstOrDefault(g => g.NonvisualVariant.FirstOrDefault(h => h.ItemPartnumber.Contains(long.Parse(items.Sku))) != null) != null)?.AllMeta?.Id.ToString() ?? null;
                        if(id != null)
                        {
                            var pr = await GetProductInfo(id);
                            var prr = await GetDetailProductInfo(id);
                            var item = prr.BjsItemsInventory.FirstOrDefault(f => f.ItemId.Equals(id));
                            OrderPlaceHelper.NotifyEmail(new InventoryStatusDto()
                            {
                                Category = "BJS",
                                ProductId = id,
                                Name = prr.Description.Name,
                                Quantity = (prr.BjsItemsInventory.FirstOrDefault().AvailInventory ?? false) ? 99 : 0,
                                Sku = items.Sku,
                                Status = (prr.BjsItemsInventory.FirstOrDefault().AvailInventory ?? false) ? "Available" : "Out Of Stock"
                            });
                            outOfStock = outOfStock && ((prr.BjsItemsInventory.FirstOrDefault().AvailInventory ?? false));
                        }

                        
                        
                        Console.WriteLine("Order Number: " + orderDto.OrderId + " | Status: " + outOfStock);
                    }
                    catch (Exception e)
                    {

                    }
                    
                }
                else
                {
                    Console.WriteLine("Order Number: " + orderDto.OrderId + " | Status: " + outOfStock);
                    return false;
                }
                
            }
            return outOfStock;
        }

      

        public static async Task<BjsProductDetailInfoDto> GetDetailProductInfo(string Id)
        {
            var url = $"https://api.bjs.com/digital/live/api/v1.2/pdp/10201?productId={Id}&pageName=PDP";
            return await InvokeRequestAsync<BjsProductDetailInfoDto>(GetAuthorizedRequest(url, HttpMethod.GET, null, false));
                
        }

    }
}
