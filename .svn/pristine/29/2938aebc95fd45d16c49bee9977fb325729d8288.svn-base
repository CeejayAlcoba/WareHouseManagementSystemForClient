using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHousemanagementSystemForClient.Interfaces.Interfaces;
using WareHouseManagementSystemForClient.DbContext.Context;
using WareHouseManagementSystemForClient.Model.PrincipalModels;

namespace WareHouseManagementSystemForClient.Repositories.Repositories
{
    public class PrincipalRepository : IPrincipalRepository
    {
        private readonly DapperContext _context;
        public PrincipalRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ClientPrincipal>> GetClientPrincipalsByClientId(int clienId)
        {
            var procedureName = "GetClientPrincipalsByClientId";
            var parameters = new DynamicParameters();
            parameters.Add("ClientId", clienId, DbType.Int64, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var principals = await connection.QueryAsync<ClientPrincipal>
                   (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return principals.ToList();
            }
        }
    }
}
