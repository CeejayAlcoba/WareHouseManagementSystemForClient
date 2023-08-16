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
using WareHouseManagementSystemForClient.Model.ClientModels;

namespace WareHouseManagementSystemForClient.Repositories.Repositories
{
    public class CargoRepository : ICargoRepository
    {
        private readonly DapperContext _context;
        public CargoRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<(IEnumerable<CargoDetails>,int,double?)> GetAllByPrincipal(int principalId,int rowSkip,int rowTake)
        {
            int customRowSkip = (rowSkip - 1) * 8;
            var procedureName = "GetCargoDetailsByPrincipal";
            var parameters = new DynamicParameters();
            parameters.Add("Id", principalId, DbType.Int64, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var multiResult = await connection.QueryMultipleAsync(procedureName, parameters, commandTimeout:120,
             commandType: CommandType.StoredProcedure);
                var totalCount = await multiResult.ReadSingleOrDefaultAsync<int>();
                var cargoDetails = await multiResult.ReadAsync<CargoDetails>();


                var totalQuantity = cargoDetails.Select(a=>a.TotalQuantity).Sum();
                return (cargoDetails.ToList().Skip(customRowSkip).Take(rowTake), totalCount, totalQuantity);
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
    }
}
