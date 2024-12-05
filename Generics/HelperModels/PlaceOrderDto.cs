using System;
using System.Collections.Generic;

namespace Generics.HelperModels
{
    public class PlaceOrderDto
    {
        public List<PlaceOrderItems> Items { get; set; }
        public string OrderId { get; set; }
        public bool IsOrdered { get; set; }
        public PlaceOrderAddress Address { get; set; }

        public PlaceOrderDto()
        {
        }

    }
    public class PlaceOrderItems
    {
        public string Sku { get; set; }
        public int Quantity { get; set; }
        public string OrderItemId { get; set; }
    }

    public class PlaceOrderAddress
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LineOne { get; set; }
        public string LineTwo { get; set; }
        public string LineThree { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsResidential { get; set; }
        public bool IsDockDoorPresent { get; set; }


    }

    public enum OrderResponseErrorType
    {
        OutOfStock = 2,
        DSProble = 3

    }
    public class OrderResponseDto
    {
        public DateTime OnCreated { get; set; }
        public DateTime OnUpdated { get; set; }
        public bool IsAuthorizationError { get; set; }
        public bool IsAddressError { get; set; }
        public bool IsSuccessfull { get; set; }
        public string Message { get; set; }
        public string OrderNumber { get; set; }
        public double TotalAmount { get; set; }
        public OrderResponseErrorType ErrorType { get; set; }
        public List<OrderProductResponseDto> Products { get; set; }
    }
    public class OrderProductResponseDto
    {

        public bool IsAvailable { get; set; }
        public bool IsQuantityIssue { get; set; }
        public string ProductSku { get; set; }
        public string PrdouctSearchedUrl { get; set; }
        public string Message { get; set; }
    }
}
