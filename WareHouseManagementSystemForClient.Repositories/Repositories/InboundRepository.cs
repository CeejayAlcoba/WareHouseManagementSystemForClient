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
        public async Task<(IEnumerable<Inbound>, int, double?, double?)> GetInboundList(DateTime? asOfDate, string? search, string? sku, int principalId, int? cargoType, int? rowSkip, int? rowTake)
        {

            var procedureName = "GetInboundList";
            var parameters = new DynamicParameters();
            parameters.Add("PrincipalId", principalId, DbType.Int64, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {

                var inbounds = await connection.QueryAsync<Inbound>(procedureName, parameters, commandTimeout: 120,
       commandType: CommandType.StoredProcedure);

                if (asOfDate != null)
                {
                    inbounds = inbounds.Where(c => c.ActualCheckinDate <= asOfDate).ToList();
                }
                if (principalId != 0)
                {
                    inbounds = inbounds.Where(a => a.PrincipalId == principalId).ToList();

                }
                if (cargoType != 3)
                {
                    inbounds = inbounds.Where(a => a.CargoType == cargoType).ToList();

                }
                if (sku != null)
                {
                    inbounds = inbounds.Where(a => a.CargoName == sku).ToList();
                }
                if (search != null)
                {
                    inbounds = inbounds
                        .Where(item => item.GetType().GetProperties().Any(property =>
                        {
                            var value = property.GetValue(item)?.ToString();
                            return value != null && value == search;
                        }))
                        .ToList();
                }
                var totalBalance = inbounds.Select(a => a.Balance).Sum();
                var totalVolume = inbounds.Select(a => a.Volume).Sum();
                if (rowSkip != null && rowTake != null)
                {
                    int customRowSkip = ((int)rowSkip - 1) * (int)rowTake;

                    return (inbounds.ToList().Skip(customRowSkip).Take((int)rowTake), inbounds.Count(), totalBalance, totalVolume);
                }
                else
                {
                    return (inbounds.ToList(), inbounds.Count(), totalBalance, totalVolume);
                }
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
