using System;
namespace Generics.HelperModels
{
    public class InventoryStatusDto
    {
        public InventoryStatusDto()
        {
        }
        public string Name { get; set; }
        public string Category { get; set; }
        public long? Quantity { get; set; }
        public string Status { get; set; }
        public string ProductId { get; set; }
        public string Link { get; set; }
        public string Sku { get; set; }
        public string ShipStationOrderNumber { get; set; }
        public int ShipStationOrderId { get; set; }
    }
}
