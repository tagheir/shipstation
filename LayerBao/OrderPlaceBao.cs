using Generics.Common;
using Generics.HelperModels;
using ShipStationApi;
using ShipStationApi.Models;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LayerBao
{
    public class OrderPlaceBao

    {
        static string val = ShipTag.Ordered.GetEnumValue().ToString();
        static string outofstock = ShipTag.OutOfStock.GetEnumValue().ToString();
        static string ds = ShipTag.DsProblems.GetEnumValue().ToString();
        static string bjs = ShipTag.BJS.GetEnumValue().ToString();


        public OrderPlaceBao()
        {
        }
        public static PlaceOrderDto GetPlaceOrderDtoFromOrder(Order latestOrder)
        {
            PlaceOrderDto placeOrderDto = new PlaceOrderDto
            {
                OrderId = latestOrder.OrderId.ToString(),
                Address = new PlaceOrderAddress()
                {
                    FirstName = latestOrder.ShipTo.Name.Split(" ")[0],
                    LastName = latestOrder.ShipTo.Name.Split(" ")[1],
                    City = latestOrder.ShipTo.City,
                    State = latestOrder.ShipTo.State,
                    LineOne = latestOrder.ShipTo.Street1,
                    LineTwo = latestOrder.ShipTo.Street2,
                    Country = latestOrder.ShipTo.Country,
                    IsResidential = latestOrder.ShipTo.Residential ?? true,
                    PhoneNumber = latestOrder.ShipTo.Phone,
                    IsDockDoorPresent = true,
                    PostalCode = latestOrder.ShipTo.PostalCode
                },
                Items = new System.Collections.Generic.List<PlaceOrderItems>()
                {


                }
            };
            latestOrder.Items.ForEach(e =>
            {
                placeOrderDto.Items.Add(new PlaceOrderItems()
                {
                    OrderItemId = e.OrderItemId.ToString(),
                    Quantity = int.Parse(e.Quantity.ToString()),
                    Sku = e?.FulfillmentSku?.ToString() ?? string.Empty,


                });


            });

            var items = placeOrderDto.Items.Where(e => e.Sku.Contains("-"));
            if(items != null)
            {
                foreach (var i in items.ToList())
                {
                    var sku = i.Sku.Split("-");
                    foreach (var sk in sku)
                    {
                        placeOrderDto.Items.Add(new PlaceOrderItems()
                        {
                            Quantity = i.Quantity,
                            Sku = sk

                        });
                    }

                }
            }
           
            placeOrderDto.Items = placeOrderDto.Items.Where(e => !e.Sku.Contains("-")).ToList();
            placeOrderDto.Items.ForEach(e =>
            {
                if (e.Sku.Contains("X"))
                {
                    var splitting = e.Sku.Split("X");
                    if (splitting.Length > 1)
                    {
                        e.Sku = splitting[0];
                        e.Quantity = int.Parse(splitting[1]);
                    }
                }
            });
            placeOrderDto.Items = placeOrderDto.Items
                .GroupBy(e => e.Sku)
                .Select(k => 
                new PlaceOrderItems() 
                { Sku = k.Key, Quantity = k.Sum(e => e.Quantity),
                    OrderItemId = k.FirstOrDefault().OrderItemId 
            }).ToList();
            return placeOrderDto;


        }
        public static async Task<bool> PlaceOrderAsync(long orderId)
        {
            var latestOrder = await ShipStationHandler.FetchOrder(orderId.ToString());
            if (latestOrder.Items.Where(e => e.FulfillmentSku == null || e.FulfillmentSku.Length == 0).ToList().Count > 0)
            {
                await ShipStationHandler.AddTagToOrder(latestOrder?.OrderId ?? 0,
                        ShipTag.FulfillmentSKU.GetEnumValue());
                return false;
            }


            if (latestOrder.TagIds.Contains(val.ToString())

                || latestOrder.TagIds.Contains(ds.ToString())
                )
            {
                return true;
            }
            

            Console.WriteLine("OrderNumber: " + latestOrder.OrderNumber);
            try
            {
                OrderResponseDto orderResponse = null;
                if (latestOrder.TagIds.Contains(bjs.ToString()))
                {
                    //orderResponse = await OrderPlacer.BJS.BjsWorker.Do(orderDto);
                }
                else
                {
                    //orderResponse = await OrderPlacer.SamsClub.SamsClubWorker.DoWork(orderDto);
                }
                if(orderResponse != null)
                {
                    //System.Threading.Thread.Sleep(TimeSpan.FromMinutes(2));
                    Console.WriteLine("Updating on ShipStation");
                    
                    
                    await UpdateOrderReponseOnShipStation(latestOrder, orderResponse);
                }
                
                return true;
            }
            catch (WebException)
            {

            }
            catch (Exception)
            {

            }
            return false;
        }
        public static async Task<bool> UpdateOrderTrackingInformation(Order orderDto)
        {
            if (orderDto.InternalNotes != null && !orderDto.InternalNotes.IsEmpty())
            {
                OrderShipperDto orderShipperDto;
                if (orderDto.TagIds.Contains(bjs))
                {

                }
                else
                {
                    orderShipperDto = await OrderPlacer.SamsClub.SamsClubWorker.GetTrackingInfo(orderDto.InternalNotes);
                    await ShipStationHandler.UpdateOrderTracking(orderShipperDto);
                }

                return true;
            }
            return false;
        }
        public static async Task UpdateOrderReponseOnShipStation(Order orderDto, OrderResponseDto orderResponse)
        {
            if (orderResponse.IsSuccessfull)
            {
                await ShipStationHandler.RemoveTagToOrder(orderDto.OrderId.Value,
                    ShipTag.DsProblems.GetEnumValue());
                await ShipStationHandler.RemoveTagToOrder(orderDto.OrderId.Value,
                ShipTag.OutOfStock.GetEnumValue());
                await ShipStationHandler.RemoveTagToOrder(orderDto.OrderId.Value,
                        ShipTag.FulfillmentSKU.GetEnumValue());
                await ShipStationHandler.AddTagToOrder(orderDto.OrderId.Value,
                ShipTag.SystemGeneratedOrder.GetEnumValue());
                await ShipStationHandler.AddTagToOrder(orderDto.OrderId.Value,
                    ShipTag.Ordered.GetEnumValue());
                await ShipStationHandler.AddTagToOrder(orderDto.OrderId.Value,
                ShipTag.SystemGeneratedOrder.GetEnumValue());
                await ShipStationHandler.AddTagToOrder(orderDto.OrderId.Value,
                    ShipTag.Ordered.GetEnumValue());
               
                await ShipStationHandler.UpdateOrderNotes(orderDto.OrderId ?? 0, orderResponse.OrderNumber);

                try
                {
                    await ShipStationHandler.UpdateOrderPricing(orderDto.OrderId ?? 0, orderResponse.TotalAmount);
                }
                catch(Exception e)
                {
                   
                }
            }
            else
            {
                if (orderResponse.ErrorType == OrderResponseErrorType.OutOfStock)
                {
                    await ShipStationHandler.AddTagToOrder(orderDto.OrderId.Value,
                            ShipTag.OutOfStock.GetEnumValue());
                }
                else
                {
                    await ShipStationHandler.AddTagToOrder(orderDto.OrderId.Value,
                            ShipTag.DsProblems.GetEnumValue());
                }
            }
        }

        public static async Task UpdateOutOfStock(long orderNumber, bool v)
        {
             await ShipStationHandler.RemoveTagToOrder(orderNumber,
                            ShipTag.OutOfStock.GetEnumValue());
        }
    }


}
