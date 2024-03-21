using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHousemanagementSystemForClient.Interfaces.Interfaces
{
    public interface IGenericRepository
    {
        Task<IEnumerable<T>> GetAllAsync<T>(string procedureName, object? parameters);
        Task<T> GetFirstOrDefaultAsync<T>(string procedureName, object? parameters);
        Task<(IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>)> QueryMultipleList<T1, T2, T3, T4, T5>(string procedureName, object parameters);
        Task<int> ExecuteAsync(string procedureName, object parameters);
    }
}
