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
        public async Task<(IEnumerable<Inbound>, int,double?,double?)> GetInventoryList(DateTime? AcceptedDateFrom, DateTime? AcceptedDateTo, string? searchRep, int? categoryId, int principalId, int? cargoType, int rowSkip, int rowTake)
        {
            int customRowSkip = (rowSkip - 1) * 8;
            var inventories = await GetInventoriesByPrincipalId(principalId);
            var inbounds = await _inboundRepository.GetAllInboundByPrincipal(principalId);

            foreach (var inbound in inbounds.Item1)
            {
                foreach(var inventory in inventories)
                {
                    if(inventory.BookingId == inbound.BookingId)
                    {
                        inbound.Quantity = inventory.Quantity;
                        inbound.Volume = inventory.Volume;
                        inbound.DateReleased = inventory.DateReleased;
                    }
                }
            }
            if (AcceptedDateFrom != null && AcceptedDateTo != null)
            {
                inbounds.Item1 = inbounds.Item1.Where(c => c.ActualCheckinDate >= AcceptedDateFrom && c.ActualCheckinDate <= AcceptedDateTo).ToList();
            }
            if (principalId != 0)
            {
                inbounds.Item1 = inbounds.Item1.Where(a => a.PrincipalId == principalId).ToList();
            }
            if (cargoType != 3)
            {
                inbounds.Item1 = inbounds.Item1.Where(a => a.CargoType == cargoType).ToList();

            }
            if (categoryId != 0 && categoryId != null)
            {
                inbounds.Item1 = inbounds.Item1.Where(a => a.ProductCategoryId == categoryId).ToList();
            }

            var filteredInbounds = inbounds.Item1.Where(a => a.Quantity > 0).ToList();
            var totalQuantity = filteredInbounds.Select(a=>a.Quantity).Sum();
            var totalVolume = filteredInbounds.Select(a => a.Volume).Sum();
            return (filteredInbounds.Skip(customRowSkip).Take(rowTake), filteredInbounds.Count(),totalQuantity,totalVolume);

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
