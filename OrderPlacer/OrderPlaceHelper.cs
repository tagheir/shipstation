using Generics.HelperModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderPlacer
{
    internal class OrderPlaceHelper
    {
        public static void NotifyEmail(InventoryStatusDto inventory)
        {
            var itemDetails = LayerDao.ProductStatusDAO.GetProductStatus(inventory.Sku, inventory.Category);
            if (itemDetails == null || (!itemDetails.InStock))
            {
                GmailHelper.Mailer.OutOfStockNotifier(inventory);
                Console.WriteLine("Email Sent");
            }

            LayerDao.ProductStatusDAO.InsertIfNotFound(new Generics.Db.ProductStatusDto()
            {
                Category = inventory.Category,
                InStock = true,
                ItemSku = inventory.Sku,
                LastUpdated = DateTime.UtcNow,
                Quantity = inventory.Quantity.Value

            });
        }
    }
}
