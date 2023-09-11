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
    public class CargoRepository : ICargoRepository
    {
        private readonly DapperContext _context;
        public CargoRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<(IEnumerable<ReportDataTable>, int, double?)> GetAllByPrincipal(ParamRequestForCargos paramRequest)
        {
           
            var procedureName = "GetCargoDetailsByPrincipal";
            var parameters = new DynamicParameters();
            parameters.Add("PrincipalId", paramRequest.PrincipalId, DbType.Int64, ParameterDirection.Input);
            parameters.Add("Search", paramRequest.Search, DbType.String, ParameterDirection.Input);
            parameters.Add("Sku", paramRequest.Sku, DbType.String, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var cargos = await connection.QueryAsync<ReportDataTable>(procedureName, parameters, commandTimeout: 120,
             commandType: CommandType.StoredProcedure);

                var totalCount = cargos.Count();
                var totalQuantity = cargos.Select(a => a.Qty).Sum();

                if (paramRequest.RowSkip != null && paramRequest.RowTake != null)
                {
                    int customRowSkip = ((int)paramRequest.RowSkip - 1) * 8;
                    cargos = cargos.Skip(customRowSkip).Take((int)paramRequest.RowTake);
                }
                
                return (cargos.ToList(), totalCount, totalQuantity);
            }
        }

        public async Task<IEnumerable<string>> GetSKUNamesByPrincipalId(int principalId)
        {
            var procedureName = "GetSKUNamesByPrincipalId";
            var parameters = new DynamicParameters();
            parameters.Add("PrincipalId", principalId, DbType.Int64, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var skus = await connection.QueryAsync<string>(procedureName,parameters, commandTimeout: 120,
             commandType: CommandType.StoredProcedure);
                return skus;
            }
        }
    }
}
