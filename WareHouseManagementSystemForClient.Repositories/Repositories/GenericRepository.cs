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
        private readonly int _commandTimeout;
        public GenericRepository(DapperContext context)
        {
            _commandTimeout = 120;
            _context = context;
        }
        public async Task<IEnumerable<T>> GetAllAsync<T>(string procedureName, DynamicParameters parameters)
        {
            using (var connection = _context.CreateConnection())    
            {

                var result = await connection.QueryAsync<T>(procedureName, parameters, commandTimeout: _commandTimeout,
                commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
        }
        public async Task<(IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>)> QueryMultipleList<T1,T2,T3,T4,T5>(string procedureName, DynamicParameters parameters)
        {
            using (var connection = _context.CreateConnection())
            {

                var mult = await connection.QueryMultipleAsync(procedureName, parameters, commandTimeout: _commandTimeout,
                commandType: CommandType.StoredProcedure);

                var result1 = IsEqualToObject<T1>() ? new List<T1>() : await mult.ReadAsync<T1>();
                var result2 = IsEqualToObject<T2>() ? new List<T2>() : await mult.ReadAsync<T2>();
                var result3 = IsEqualToObject<T3>() ? new List<T3>() : await mult.ReadAsync<T3>();
                var result4 = IsEqualToObject<T4>() ? new List<T4>() : await mult.ReadAsync<T4>();
                var result5 = IsEqualToObject<T5>() ? new List<T5>() : await mult.ReadAsync<T5>();

                return (result1, result2, result3, result4, result5);
            }
        }
        private bool IsEqualToObject<T>()
        {
            return typeof(T) == typeof(object) ;
        } 
        public async Task<T> GetFirstOrDefaultAsync<T>(string procedureName, DynamicParameters parameters)
        {
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync<T>
                   (procedureName, parameters, commandType: CommandType.StoredProcedure, commandTimeout: _commandTimeout);

                return result;
            }
        }
        public async Task<int> ExecuteAsync(string procedureName, DynamicParameters parameters)
        {
            using (var connection = _context.CreateConnection())
            {
                var result =  await connection.ExecuteAsync
                   (procedureName, parameters, commandType: CommandType.StoredProcedure, commandTimeout: _commandTimeout);
                return 0;
            }
        }
    }
}
