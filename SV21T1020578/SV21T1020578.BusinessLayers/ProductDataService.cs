using SV21T1020578.DataLayers;
using SV21T1020578.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV21T1020578.BusinessLayers
{
    public class ProductDataService
    {
        static ProductDataService()
        {
            string connectionString = Configuration.ConnectionString;
            ProductDB = new ProductDAL(connectionString);
        }
        private static ProductDAL ProductDB { get; set; }
        public static List<Product> ListProducts(string searchValue = "")
        {
            return ProductDB.List(searchValue: searchValue);
        }

        public static List<Product> ListProducts(out int rowCount, int page = 1, int pageSize = 0, string searchValue = "", int categoryId = 0, int supplierId = 0, decimal minPrice = 0, decimal maxPrice = 0)
        {
            rowCount = ProductDB.Count(searchValue, categoryId, supplierId, minPrice, maxPrice);
            return ProductDB.List(page, pageSize, searchValue, categoryId, supplierId, minPrice, maxPrice);
        }
    }
}
