using Dapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using WareHousemanagementSystemForClient.Interfaces.Interfaces;
using WareHouseManagementSystemForClient.DbContext.Context;
using WareHouseManagementSystemForClient.Model.ParamRequestModels;
using WareHouseManagementSystemForClient.Model.ReportModels;

namespace WareHouseManagementSystemForClient.Repositories.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly DapperContext _context;
        public InventoryRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<(IEnumerable<ReportDataTable>, int, double?)> GetInventoryList(ParamRequestForReports paramRequest)
        {

            var procedureName = "GetInventoryList";
            var parameters = new DynamicParameters();
            parameters.Add("PrincipalId", paramRequest.PrincipalId, DbType.Int64, ParameterDirection.Input);
            parameters.Add("DateFrom", paramRequest.DateFrom, DbType.DateTime, ParameterDirection.Input);
            parameters.Add("DateTo", paramRequest.DateTo, DbType.DateTime, ParameterDirection.Input);
            parameters.Add("Sku", paramRequest.Sku, DbType.String, ParameterDirection.Input);
            parameters.Add("Search", paramRequest.Search, DbType.String, ParameterDirection.Input);
            parameters.Add("CargoType", paramRequest.CargoType, DbType.Int64, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {

                var inventories = await connection.QueryAsync<ReportDataTable>(procedureName, parameters, commandTimeout: 120,
       commandType: CommandType.StoredProcedure);


                var totalQuantity = inventories.Select(a => a.Qty).Sum();
                if (paramRequest.RowSkip != null && paramRequest.RowTake != null)
                {
                    int customRowSkip = ((int)paramRequest.RowSkip - 1) * (int)paramRequest.RowTake;

                    return (inventories.ToList().Skip(customRowSkip).Take((int)paramRequest.RowTake), inventories.Count(), totalQuantity);
                }
                else
                {
                    return (inventories.ToList(), inventories.Count(), totalQuantity);
                }
            }

        }
    }
}
