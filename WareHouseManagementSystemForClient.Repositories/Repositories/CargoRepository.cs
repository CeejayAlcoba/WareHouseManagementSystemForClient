﻿using Dapper;
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
        public async Task<(IEnumerable<CargoDetails>,int)> GetAllByPrincipal(int principalId,int rowSkip,int rowTake)
        {
            int customRowSkip = (rowSkip - 1) * 10;
            var procedureName = "GetCargoDetailsByPrincipal";
            var parameters = new DynamicParameters();
            parameters.Add("Id", principalId, DbType.Int64, ParameterDirection.Input);
            parameters.Add("RowSkip", customRowSkip, DbType.Int64, ParameterDirection.Input);
            parameters.Add("RowTake", rowTake, DbType.Int64, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var multiResult = await connection.QueryMultipleAsync(procedureName, parameters,
             commandType: CommandType.StoredProcedure);
                var totalCount = await multiResult.ReadSingleOrDefaultAsync<int>();
                var cargoDetails = await multiResult.ReadAsync<CargoDetails>();

                

                return (cargoDetails.ToList(), totalCount);
            }
        }
        public async Task<double> GetTotalItems(int principalId)
        {
            var procedureName = "GetCargoDetailsTotalItems";
            var parameters = new DynamicParameters();
            parameters.Add("PrincipalId", principalId, DbType.Int64, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var Result = await connection.QuerySingleAsync<double>(procedureName, parameters,
             commandType: CommandType.StoredProcedure);
                return Result;
            }
        }
        public async Task<double> GetTotalHandlingIn(int principalId)
        {
            var procedureName = "GetCargoDetailsTotalHandlingIn";
            var parameters = new DynamicParameters();
            parameters.Add("PrincipalId", principalId, DbType.Int64, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var Result = await connection.QuerySingleAsync<double>(procedureName, parameters,
             commandType: CommandType.StoredProcedure);
                return Result;
            }
        }
        public async Task<double> GetTotalHandlingOut(int principalId)
        {
            var procedureName = "GetCargoDetailsTotalHandlingOut";
            var parameters = new DynamicParameters();
            parameters.Add("PrincipalId", principalId, DbType.Int64, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var Result = await connection.QuerySingleAsync<double>(procedureName, parameters,
             commandType: CommandType.StoredProcedure);
                return Result;
            }
        }

        public async Task<double> GetCBM(int principalId)
        {
            var procedureName = "GetCargoDetailsCBM";
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
