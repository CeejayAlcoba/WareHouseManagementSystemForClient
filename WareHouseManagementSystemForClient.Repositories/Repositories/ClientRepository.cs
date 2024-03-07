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
        public async Task<int> SaveClientCredential(ClientForSave client)
        {
            var procedureName = "WMS_SAVE_CLIENT_CREDENTIAL";
            var parameters = new DynamicParameters();
            parameters.Add("ID", client.ID, DbType.Int64, ParameterDirection.Input);
            parameters.Add("Username", client.Username, DbType.String, ParameterDirection.Input);
            parameters.Add("Password", client.Password, DbType.String, ParameterDirection.Input);
            parameters.Add("Firstname", client.Firstname, DbType.String, ParameterDirection.Input);
            parameters.Add("Lastname", client.Lastname, DbType.String, ParameterDirection.Input);
            parameters.Add("Salt", client.Salt, DbType.String, ParameterDirection.Input);
            parameters.Add("IsActive", client.IsActive, DbType.Boolean, ParameterDirection.Input);
            return await _genericRepository.ExecuteAsync(procedureName, parameters);
           

        }
    }
}
