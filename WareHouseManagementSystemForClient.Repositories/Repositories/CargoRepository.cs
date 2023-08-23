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
using WareHouseManagementSystemForClient.Model.CargoModelsForTable;

namespace WareHouseManagementSystemForClient.Repositories.Repositories
{
    public class CargoRepository : ICargoRepository
    {
        private readonly DapperContext _context;
        private readonly IInventoryRepository _inventoryRepository;
        public CargoRepository(DapperContext context, IInventoryRepository inventoryRepository)
        {
            _context = context;
            _inventoryRepository = inventoryRepository;
        }
        public async Task<(IEnumerable<CargoDetailsForTable>, int, double?)> GetAllByPrincipal(int principalId, int? rowSkip, int? rowTake,string? search)
        {
           
            var procedureName = "GetCargoDetailsByPrincipal";
            var parameters = new DynamicParameters();
            parameters.Add("Id", principalId, DbType.Int64, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var multiResult = await connection.QueryMultipleAsync(procedureName, parameters, commandTimeout: 120,
             commandType: CommandType.StoredProcedure);
                var totalCount = await multiResult.ReadSingleOrDefaultAsync<int>();
                var cargoDetails = await multiResult.ReadAsync<CargoDetailsForTable>();

                if(search != null) {
                    cargoDetails=cargoDetails.Where(
                        c=>c.UnitOfMeasurement.ToString() == search ||
                        c.CargoName == search ||
                        c.TotalVolume.ToString() == search||
                        c.TotalQuantity.ToString() == search
                    );
                }

                totalCount = cargoDetails.Count();
                var totalQuantity = cargoDetails.Select(a => a.TotalQuantity).Sum();

                if (rowSkip!=null && rowTake!=null)
                {
                    int customRowSkip = ((int)rowSkip - 1) * 8;
                    cargoDetails = cargoDetails.Skip(customRowSkip).Take((int)rowTake);
                }
                
                return (cargoDetails.ToList(), totalCount, totalQuantity);
            }
        }

        public async Task<double> GetCargoDetailsTotalQuantity(int principalId)
        {
            var procedureName = "GetCargoDetailsTotalQuantity";
            var parameters = new DynamicParameters();
            parameters.Add("PrincipalId", principalId, DbType.Int64, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var Result = await connection.QuerySingleAsync<double>(procedureName, parameters,
             commandType: CommandType.StoredProcedure);
                return Result;
            }
        }


        public async Task<double> GetCargoDetailsTotalVolume(int principalId)
        {
            var procedureName = "GetCargoDetailsTotalVolume";
            var parameters = new DynamicParameters();
            parameters.Add("PrincipalId", principalId, DbType.Int64, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var Result = await connection.QuerySingleAsync<double>(procedureName, parameters,
             commandType: CommandType.StoredProcedure);
                return Result;
            }
        }
        public async Task<double> GetCargoDetailsTotalSKU(int principalId)
        {

            var procedureName = "GetCargoDetailsTotalSKU";
            var parameters = new DynamicParameters();
            parameters.Add("PrincipalId", principalId, DbType.Int64, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var Result = await connection.QuerySingleAsync<double>(procedureName, parameters,
             commandType: CommandType.StoredProcedure);
                return Result;
            }
        }
        public async Task<(double,IEnumerable<CargoDetailsForSKURecords>)> GetCargoDetailsSKURecords(int principalId, int? rowSkip, int? rowTake,string? search)
        {

            var inventories = await _inventoryRepository.GetInventoryList(null, null, null, null, principalId, null, null, null);

            var inboundsList = inventories.Item1.Select(inventory => new CargoDetailsForSKURecords
            {
                CargoName = inventory.CargoName,
                ICRReferenceNo = inventory.ICRReferenceNo,
                Quantity = inventory.Quantity,
                Uom = inventory.Uom,
                Volume = inventory.Volume
            }).OrderBy(c => c.ICRReferenceNo);

            if (search != null)
            {
                inboundsList = inboundsList.Where(c =>
                    c.Quantity.ToString() == search ||
                    c.ICRReferenceNo == search ||
                    c.CargoName == search ||
                    c.Volume.ToString() == search ||
                    c.Uom == search
                ).OrderBy(c => c.ICRReferenceNo);
            }
            if (rowTake != null && rowSkip !=null)
            {
                int customRowSkip = ((int)rowSkip - 1) * 8;
                return (inboundsList.Count(), inboundsList.Skip(customRowSkip).Take((int)rowTake));
            }
            else
            {
                return (inboundsList.Count(), inboundsList);
            }
           
        }
    }
}
