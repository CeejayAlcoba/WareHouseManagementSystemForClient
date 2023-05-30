using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHousemanagementSystemForClient.Interfaces.Interfaces;
using WareHouseManagementSystemForClient.DbContext.Context;
using WareHouseManagementSystemForClient.Model.ProductModels;

namespace WareHouseManagementSystemForClient.Repositories.Repositories
{
    public  class ProductRepository : IProductRepository
    {
        private readonly DapperContext _context;
        public ProductRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ProductCategory>> GetAllProductCategory()
        {
            var procedureName = "GetAllProductCategory";
            using (var connection = _context.CreateConnection())
            {
                var productCategories = await connection.QueryAsync<ProductCategory>(procedureName, commandTimeout: 120,
             commandType: CommandType.StoredProcedure);
                return productCategories.ToList();
            }
        }
    }
}
