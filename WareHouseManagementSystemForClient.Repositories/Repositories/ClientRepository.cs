using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHousemanagementSystemForClient.Interfaces.Interfaces;
using WareHouseManagementSystemForClient.DbContext.Context;
using WareHouseManagementSystemForClient.Model.DTOModels.ClientModels;

namespace WareHouseManagementSystemForClient.Repositories.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly IGenericRepository _genericRepository;
        public ClientRepository(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public async Task<ClientDTO> GetClientByUsername(string username)
        {
            var procedureName = "CLIENT_GetClientByUsername";
            var parameters = new DynamicParameters();
            parameters.Add("Username", username, DbType.String, ParameterDirection.Input);
           
            var client = await _genericRepository.GetFirstOrDefaultAsync<ClientDTO>(procedureName, parameters);

            return client;
        }
    }
}
