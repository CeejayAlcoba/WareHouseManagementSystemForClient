using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using WareHousemanagementSystemForClient.Interfaces.Interfaces;
using WareHouseManagementSystemForClient.DbContext.Context;
using WareHouseManagementSystemForClient.Model.CargoModels;

namespace WareHouseManagementSystemForClient.Repositories.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly DapperContext _context;
        private readonly IInboundRepository _inboundRepository;
        private readonly IOutboundRepository _outboundRepository;
        public InventoryRepository(DapperContext context, IInboundRepository inboundRepository, IOutboundRepository outboundRepository)
        {
            _context = context;
            _inboundRepository = inboundRepository;
            _outboundRepository = outboundRepository;
        }
        public async Task<(IEnumerable<Inbound>, int, double?, double?)> GetInventoryList(DateTime? asOfDate, string? search, string? sku, int principalId, int? cargoType, int? rowSkip, int? rowTake)
        {

            var inventories = await GetInventoriesByPrincipalId(principalId);
            var inbounds = await _inboundRepository.GetAllInboundByPrincipal(principalId);
            //overwriting
            foreach (var inbound in inbounds.Item1)
            {
                foreach (var inventory in inventories)
                {
                    if(inventory.CargoDetailsId == inbound.Id)
                    {
                        inbound.DateReleased = inventory.DateReleased;
                        inbound.Volume = inventory.Volume;
                        inbound.Balance = inventory.Balance;
                        inbound.InboundQuantity = inventory.InboundQuantity;
                        inbound.OutboundQuantity = inventory.OutboundQuantity;
                    }
                 
                }
            }
            if (asOfDate != null)
            {
                inbounds.Item1 = inbounds.Item1.Where(c => c.ActualCheckinDate <= asOfDate).ToList();
            }
            if (principalId != 0)
            {
                inbounds.Item1 = inbounds.Item1.Where(a => a.PrincipalId == principalId).ToList();
            }
            if (cargoType != 3 && cargoType != null)
            {
                inbounds.Item1 = inbounds.Item1.Where(a => a.CargoType == cargoType).ToList();

            }
            if (sku != null)
            {
                inbounds.Item1 = inbounds.Item1.Where(a => a.CargoName == sku).ToList();
            }

            if (search != null)
            {
                inbounds.Item1 = inbounds.Item1
                    .Where(item => item.GetType().GetProperties().Any(property =>
                    {
                        var value = property.GetValue(item)?.ToString();
                        return value != null && value == search;
                    }))
                    .ToList();
            }


            var filteredInbounds = inbounds.Item1.Where(a => a.Balance > 0).ToList();
            var totalQuantity = filteredInbounds.Select(a => a.Balance).Sum();
            var totalVolume = filteredInbounds.Select(a => a.Volume).Sum();

            if (rowSkip != null && rowTake != null)
            {
                int customRowSkip = ((int)rowSkip - 1) * 8;
                return (filteredInbounds.Skip(customRowSkip).Take((int)rowTake), filteredInbounds.Count(), totalQuantity, totalVolume);
            }
            else
            {
                return (filteredInbounds, filteredInbounds.Count(), totalQuantity, totalVolume);
            }

        }
        public async Task<IEnumerable<Inventory>> GetInventoriesByPrincipalId(int principalId)
        {
            var procedureName = "GetInventoryList";
            var parameters = new DynamicParameters();
            parameters.Add("PrincipalId", principalId, DbType.Int64, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var inventories = await connection.QueryAsync<Inventory>(procedureName, parameters, commandTimeout: 120,
             commandType: CommandType.StoredProcedure);

                return inventories.ToList();
            }
        }
    }
}
