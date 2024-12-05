using Generics.Common;
using Generics.HelperModels;
using Microsoft.AspNetCore.Mvc;
using ShipStationApi;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Admin.Controllers
{
    public class Orders : Controller
    {
        public bool UpdateToken(string token)
        {
            return LayerBao.SiteMetaBAO.InsertIfNotFound(new Generics.Db.SiteMetaDto()
            {
                KEY = Generics.SiteMetaConstants.ACCESS_TOKEN_SAMSCLUB,
                VALUE = token
            });
        }
        public Orders()
        {
            ShipStationApi.ShipStationHandler.GetAllTags();
        }
        public IActionResult Index(string url)
        {
            var val = ShipTag.Ordered.GetEnumValue<ShipStationApi.ShipTag>();
            return View("Search");
        }
        public async Task<IActionResult> SearchAsync(string orderId)
        {

            return View(await ShipStationApi.ShipStationHandler.FetchOrdersByOrderNumber(orderId));
        }
        //public async Task<IActionResult> PlaceOrder(PlaceOrderDto orderDto)
        //{
        //    var latestOrder = await ShipStationHandler.FetchOrder(orderDto.OrderId);
        //    orderDto.Address = new PlaceOrderAddress()
        //    {
        //        FirstName = latestOrder.ShipTo.Name.Split(" ")[0],
        //        LastName = latestOrder.ShipTo.Name.Split(" ")[1],
        //        City = latestOrder.ShipTo.City,
        //        State = latestOrder.ShipTo.State,
        //        LineOne = latestOrder.ShipTo.Street1,
        //        LineTwo = latestOrder.ShipTo.Street2,
        //        Country = latestOrder.ShipTo.Country,
        //        IsResidential = latestOrder.ShipTo.Residential ?? true,
        //        PhoneNumber = latestOrder.ShipTo.Phone,
        //        IsDockDoorPresent = true,
        //        PostalCode = latestOrder.ShipTo.PostalCode
        //    };
        //    orderDto.Items.ForEach(e =>
        //    {

        //        if (e.Sku.Contains("X"))
        //        {
        //            var splitting = e.Sku.Split("X");
        //            if (splitting.Length > 1)
        //            {
        //                e.Sku = splitting[0];
        //                e.Quantity = int.Parse(splitting[1]);
        //            }
        //        }

        //    });
        //    var orderResponse = await OrderPlacer.SamsClub.SamsClubWorker.DoWork(orderDto);
        //    if (orderResponse.IsSuccessfull)
        //    {
               
        //        await ShipStationHandler.UpdateOrderItemsQuantity()

        //        await ShipStationHandler.RemoveTagToOrder(long.Parse(orderDto.OrderId),
        //            ShipTag.DsProblems.GetEnumValue());
        //        await ShipStationHandler.RemoveTagToOrder(long.Parse(orderDto.OrderId),
        //        ShipTag.OutOfStock.GetEnumValue());
        //        await ShipStationHandler.RemoveTagToOrder(long.Parse(orderDto.OrderId),
        //                ShipTag.FulfillmentSKU.GetEnumValue());
        //        await ShipStationHandler.AddTagToOrder(long.Parse(orderDto.OrderId),
        //        ShipTag.SystemGeneratedOrder.GetEnumValue());
        //        await ShipStationHandler.AddTagToOrder(long.Parse(orderDto.OrderId),
        //            ShipTag.Ordered.GetEnumValue());
        //    }
        //    else
        //    {
        //        if (orderResponse.ErrorType == OrderResponseErrorType.OutOfStock)
        //        {
        //            await ShipStationHandler.AddTagToOrder(long.Parse(orderDto.OrderId),
        //                    ShipTag.OutOfStock.GetEnumValue());
        //        }
        //        else
        //        {
        //            await ShipStationHandler.AddTagToOrder(long.Parse(orderDto.OrderId),
        //                    ShipTag.DsProblems.GetEnumValue());
        //        }
        //    }


        //    return View("Search", await ShipStationHandler.FetchOrdersByOrderNumber(orderDto.OrderId));
        //}

        //public async Task<IActionResult> PikcupPlaceOrder(List<PlaceOrderDto> orders)
        //{
        //    foreach(var orderDto in orders)
        //    {
        //        var latestOrder = await ShipStationHandler.FetchOrder(orderDto.OrderId);
        //        orderDto.Address = new PlaceOrderAddress()
        //        {
        //            FirstName = latestOrder.ShipTo.Name.Split(" ")[0],
        //            LastName = latestOrder.ShipTo.Name.Split(" ")[1],
        //            City = latestOrder.ShipTo.City,
        //            State = latestOrder.ShipTo.State,
        //            LineOne = latestOrder.ShipTo.Street1,
        //            LineTwo = latestOrder.ShipTo.Street2,
        //            Country = latestOrder.ShipTo.Country,
        //            IsResidential = latestOrder.ShipTo.Residential ?? true,
        //            PhoneNumber = latestOrder.ShipTo.Phone,
        //            IsDockDoorPresent = true,
        //            PostalCode = latestOrder.ShipTo.PostalCode
        //        };
        //        orderDto.Items.ForEach(e =>
        //        {

        //            if (e.Sku.Contains("X"))
        //            {
        //                var splitting = e.Sku.Split("X");
        //                if (splitting.Length > 1)
        //                {
        //                    e.Sku = splitting[0];
        //                    e.Quantity = int.Parse(splitting[1]);
        //                }
        //            }

        //        });
               
        //    }
        //    var orderResponse = await OrderPlacer.SamsClub.SamsClubWorker.DoWork(orderDto);
        //    if (orderResponse.IsSuccessfull)
        //    {

        //        await ShipStationHandler.UpdateOrderItemsQuantity()


        //        await ShipStationHandler.RemoveTagToOrder(long.Parse(orderDto.OrderId),
        //            ShipTag.DsProblems.GetEnumValue());
        //        await ShipStationHandler.RemoveTagToOrder(long.Parse(orderDto.OrderId),
        //        ShipTag.OutOfStock.GetEnumValue());
        //        await ShipStationHandler.RemoveTagToOrder(long.Parse(orderDto.OrderId),
        //                ShipTag.FulfillmentSKU.GetEnumValue());
        //        await ShipStationHandler.AddTagToOrder(long.Parse(orderDto.OrderId),
        //        ShipTag.SystemGeneratedOrder.GetEnumValue());
        //        await ShipStationHandler.AddTagToOrder(long.Parse(orderDto.OrderId),
        //            ShipTag.Ordered.GetEnumValue());
        //    }
        //    else
        //    {
        //        if (orderResponse.ErrorType == OrderResponseErrorType.OutOfStock)
        //        {
        //            await ShipStationHandler.AddTagToOrder(long.Parse(orderDto.OrderId),
        //                    ShipTag.OutOfStock.GetEnumValue());
        //        }
        //        else
        //        {
        //            await ShipStationHandler.AddTagToOrder(long.Parse(orderDto.OrderId),
        //                    ShipTag.DsProblems.GetEnumValue());
        //        }
        //    }


        //    return View("Search", await ShipStationHandler.FetchOrdersByOrderNumber(orderDto.OrderId));
        //}
        public async Task<IActionResult> AddTagToOrder(long OrderId, long tagId)
        {

            return View("Search", await ShipStationHandler.AddTagToOrder(OrderId, tagId));
        }
        public async Task<IActionResult> RemoveToOrder(long OrderId, long tagId)
        {

            return View("Search", await ShipStationHandler.RemoveTagToOrder(OrderId, tagId));
        }

    }
}
