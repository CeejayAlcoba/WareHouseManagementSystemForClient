using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHousemanagementSystemForClient.Interfaces.Interfaces;
using WareHouseManagementSystemForClient.DbContext.Context;
using WareHouseManagementSystemForClient.Model.ClientModels;
using WareHouseManagementSystemForClient.Model.SecurityModels;

namespace WareHouseManagementSystemForClient.Repositories.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly DapperContext _context;
        public ClientRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<Client> GetClientByUsername(string username)
        {
            var procedureName = "GetClientByUsername";
            var parameters = new DynamicParameters();
            parameters.Add("Username", username, DbType.String, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var client = await connection.QueryFirstOrDefaultAsync<Client>
                   (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return client;
            }
        }
    }
}
