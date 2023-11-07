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
        Task<IEnumerable<T>> GetAllAsync<T>(string procedureName, DynamicParameters? parameters);
        Task<T> GetFirstOrDefaultAsync<T>(string procedureName, DynamicParameters? parameters);
    }
}
