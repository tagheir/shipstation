using System;
using Generics.Db;
using Generics.Services.DatabaseService.AdoNet;

namespace LayerDao
{
    public class ProductStatusDAO
    {
        public ProductStatusDAO()
        {
        }
        public static long InsertIfNotFound(ProductStatusDto productStatus)
        {
            var dt = GetProductStatus(productStatus.ItemSku, productStatus.Category);
            if(dt == null)
            {
                return GenericExecutor.Insert<ProductStatusDto>(productStatus, "ProductStatus");
            }
            else
            {
                productStatus.Id = dt.Id;
                return UpdateProductStatus(productStatus);
            }
            return 0;
        }
        public static long UpdateProductStatus(ProductStatusDto productStatusDto)
        {
            return GenericExecutor.Update<ProductStatusDto>(productStatusDto, "ProductStatus", productStatusDto.Id);
        }
        public static ProductStatusDto GetProductStatus (string Sku,string Category)
        {
            var query = $"SELECT * FROM dbo.ProductStatus WHERE ItemSku = '{Sku}' AND Category = '{Category}'";
            return QueryExecutor.FirstOrDefault<ProductStatusDto>(query);
        }

    }
}
