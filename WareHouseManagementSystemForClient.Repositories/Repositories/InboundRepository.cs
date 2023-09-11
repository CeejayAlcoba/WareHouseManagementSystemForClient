using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHousemanagementSystemForClient.Interfaces.Interfaces;
using WareHouseManagementSystemForClient.DbContext.Context;
using WareHouseManagementSystemForClient.Model.ParamRequestModels;
using WareHouseManagementSystemForClient.Model.ReportModels;

namespace WareHouseManagementSystemForClient.Repositories.Repositories
{
    public class InboundRepository : IInboundRepository
    {
        private readonly DapperContext _context;
        public InboundRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<(IEnumerable<ReportDataTable>, int, double?)> GetInboundList(ParamRequestForReports paramRequest)
        {

            var procedureName = "GetInboundList";
            var parameters = new DynamicParameters();
            parameters.Add("PrincipalId", paramRequest.PrincipalId, DbType.Int64, ParameterDirection.Input);
            parameters.Add("DateFrom", paramRequest.DateFrom, DbType.DateTime, ParameterDirection.Input);
            parameters.Add("DateTo", paramRequest.DateTo, DbType.DateTime, ParameterDirection.Input);
            parameters.Add("Sku", paramRequest.Sku, DbType.String, ParameterDirection.Input);
            parameters.Add("Search", paramRequest.Search, DbType.String, ParameterDirection.Input);
            parameters.Add("CargoType", paramRequest.CargoType, DbType.Int64, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {

                var inbounds = await connection.QueryAsync<ReportDataTable>(procedureName, parameters, commandTimeout: 120,
       commandType: CommandType.StoredProcedure);

              
                var totalQuantity = inbounds.Select(a => a.Qty).Sum();
                if (paramRequest.RowSkip != null && paramRequest.RowTake != null)
                {
                    int customRowSkip = ((int)paramRequest.RowSkip - 1) * (int)paramRequest.RowTake;

                    return (inbounds.ToList().Skip(customRowSkip).Take((int)paramRequest.RowTake), inbounds.Count(), totalQuantity);
                }
                else
                {
                    return (inbounds.ToList(), inbounds.Count(), totalQuantity);
                }
            }
        }
    }
}
