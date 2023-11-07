using Dapper;
using System.Data;
using WareHousemanagementSystemForClient.Interfaces.Interfaces;
using WareHouseManagementSystemForClient.DbContext.Context;
using WareHouseManagementSystemForClient.Model.DTOModels.PrincipalModels;

namespace WareHouseManagementSystemForClient.Repositories.Repositories
{
    public class PrincipalRepository : IPrincipalRepository
    {
        private readonly DapperContext _context;
        public PrincipalRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<PrincipalDTO>> GetClientPrincipalsByClientId(int clienId)
        {
            var procedureName = "CLIENT_GetClientPrincipalsByClientId";
            var parameters = new DynamicParameters();
            parameters.Add("ClientId", clienId, DbType.Int64, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var principals = await connection.QueryAsync<PrincipalDTO>
                   (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return principals.ToList();
            }
        }
    }
}
