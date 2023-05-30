using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHousemanagementSystemForClient.Interfaces.Interfaces;
using WareHouseManagementSystemForClient.DbContext.Context;
using WareHouseManagementSystemForClient.Model.CargoModels;

namespace WareHouseManagementSystemForClient.Repositories.Repositories
{
    public class InboundRepository : IInboundRepository
    {
        private readonly DapperContext _context;
        public InboundRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<(IEnumerable<Inbound>, int,double?,double?)> GetInboundList(DateTime? AcceptedDateFrom, DateTime? AcceptedDateTo, string? searchRep, int? categoryId, int principalId, int? cargoType, int rowSkip, int rowTake)
        {
            int customRowSkip = (rowSkip - 1) * rowTake;
            var procedureName = "GetInboundList";
            var parameters = new DynamicParameters();
            parameters.Add("PrincipalId", principalId, DbType.Int64, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
            
                    var inbounds = await connection.QueryAsync<Inbound>(procedureName, parameters, commandTimeout: 120,
           commandType: CommandType.StoredProcedure);
                    
                    if (AcceptedDateFrom != null && AcceptedDateTo != null)
                    {
                    inbounds = inbounds.Where(c => c.ActualCheckinDate >= AcceptedDateFrom && c.ActualCheckinDate <= AcceptedDateTo).ToList();
                    }
                    if (principalId != 0)
                    {
                    inbounds = inbounds.Where(a => a.PrincipalId == principalId).ToList();

                    }
                    if (cargoType != 3)
                    {
                    inbounds = inbounds.Where(a => a.CargoType == cargoType).ToList();

                    }
                    if (categoryId != 0 && categoryId != null)
                    {
                    inbounds = inbounds.Where(a => a.ProductCategoryId == categoryId).ToList();
                    }
                    var totalQuantity= inbounds.Select(a => a.Quantity).Sum();
                    var totalVolume = inbounds.Select(a=>a.Volume).Sum();
           
                return (inbounds.ToList().Skip(customRowSkip).Take(rowTake), inbounds.Count(), totalQuantity, totalVolume);
            }
        }
        public async Task<(IEnumerable<Inbound>, int)> GetAllInboundByPrincipal(int principalId)
        {
            var procedureName = "GetInboundList";
            var parameters = new DynamicParameters();
            parameters.Add("PrincipalId", principalId, DbType.Int64, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var inbounds = await connection.QueryAsync<Inbound>(procedureName, parameters, commandTimeout: 120,
             commandType: CommandType.StoredProcedure);

              
                return (inbounds.ToList(), inbounds.Count());


            }
        }
    }
}
