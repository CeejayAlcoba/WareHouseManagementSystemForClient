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
using WareHouseManagementSystemForClient.Model.DTOModels.SecurityModels;

namespace WareHouseManagementSystemForClient.Repositories.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IClientRepository _clientRepository;
        private readonly ISecurityRepository _securityRepository;
        private readonly IGenericRepository _genericRepository;
        public AccountRepository(IClientRepository clientRepository, ISecurityRepository securityRepository,IGenericRepository genericRepository)
        {
            _clientRepository = clientRepository;
            _securityRepository = securityRepository;
            _genericRepository = genericRepository;
        }
        public async Task<ClientDTO> ClientLogin(LoginCredential credential)
        {
            ClientDTO getClientByUsername = await _clientRepository.GetClientByUsername(credential.Username);
            
            if (getClientByUsername != null)
            {
                var clientSalt = await _securityRepository.GetClientSaltById(getClientByUsername.ID);
                var Encrypted = _securityRepository.ToHash(credential.Password,clientSalt);

                var procedureName = "CLIENT_ValidateClient";

                var parameters = new DynamicParameters();
                parameters.Add("Username", credential.Username, DbType.String, ParameterDirection.Input);
                parameters.Add("Password", Encrypted.Hash, DbType.String, ParameterDirection.Input);
                
                var client = await _genericRepository.GetFirstOrDefaultAsync<ClientDTO>(procedureName, parameters);

                return client;
            }
            return new ClientDTO();
        }
    }
}
