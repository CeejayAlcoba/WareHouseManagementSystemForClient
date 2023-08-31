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
    public class OutboundRepository : IOutboundRepository
    {
        private readonly DapperContext _context;
        public OutboundRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<(IEnumerable<Inbound>, int)> GetAllOutboundByPrincipal(int principalId)
        {
            var procedureName = "GetOutboundList";
            var parameters = new DynamicParameters();
            parameters.Add("PrincipalId", principalId, DbType.Int64, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var inbounds = await connection.QueryAsync<Inbound>(procedureName, parameters, commandTimeout: 120,
             commandType: CommandType.StoredProcedure);

                return (inbounds.ToList(), inbounds.Count());


            }
        }

        public async Task<(IEnumerable<Outbound>, int, double?, double?)> GetOutboundList(DateTime? asOfDate, string? search, string? sku, int principalId, int? cargoType, int? rowSkip, int? rowTake)
        {

            var procedureName = "GetOutboundList";
            var parameters = new DynamicParameters();
            parameters.Add("PrincipalId", principalId, DbType.Int64, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var outbounds = await connection.QueryAsync<Outbound>(procedureName, parameters, commandTimeout: 120,
             commandType: CommandType.StoredProcedure);


                if (asOfDate != null)
                {
                    outbounds = outbounds.Where(c => c.ActualCheckinDate <= asOfDate).ToList();
                }
                if (principalId != 0)
                {
                    outbounds = outbounds.Where(a => a.PrincipalId == principalId).ToList();
                }
                if (cargoType != 3)
                {
                    outbounds = outbounds.Where(a => a.CargoType == cargoType).ToList();
                }
                if (sku != null)
                {
                    outbounds = outbounds.Where(a => a.CargoName == sku).ToList();
                }
                if (search != null)
                {
                    outbounds = outbounds
                        .Where(item => item.GetType().GetProperties().Any(property =>
                        {
                            var value = property.GetValue(item)?.ToString();
                            return value != null && value == search;
                        }))
                        .ToList();
                }
                var totalQuantity = outbounds.Select(a => a.Quantity).Sum();
                var totalVolume = outbounds.Select(a => a.Volume).Sum();
                if (rowSkip != null && rowTake != null)
                {
                    int customRowSkip = ((int)rowSkip - 1) * (int)rowTake;
                    return (outbounds.ToList().Skip(customRowSkip).Take((int)rowTake), outbounds.Count(), totalQuantity, totalVolume);
                }
                else
                {
                    return (outbounds.ToList(), outbounds.Count(), totalQuantity, totalVolume);
                }
            }
        }
    }
}

