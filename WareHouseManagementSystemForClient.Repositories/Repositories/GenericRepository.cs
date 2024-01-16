using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHousemanagementSystemForClient.Interfaces.Interfaces;
using WareHouseManagementSystemForClient.DbContext.Context;

namespace WareHouseManagementSystemForClient.Repositories.Repositories
{
    public class GenericRepository : IGenericRepository
    {
        private readonly DapperContext _context;
        public GenericRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<T>> GetAllAsync<T>(string procedureName, DynamicParameters parameters)
        {
            using (var connection = _context.CreateConnection())
            {

                var result = await connection.QueryAsync<T>(procedureName, parameters, commandTimeout: 120,
                commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
        }

        public async Task<T> GetFirstOrDefaultAsync<T>(string procedureName, DynamicParameters parameters)
        {
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync<T>
                   (procedureName, parameters, commandType: CommandType.StoredProcedure, commandTimeout: 120);

                return result;
            }
        }
    }
}
