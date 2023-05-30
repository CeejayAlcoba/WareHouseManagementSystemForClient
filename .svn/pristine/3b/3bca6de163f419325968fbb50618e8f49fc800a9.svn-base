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
    public class AccountRepository : IAccountRepository
    {
        private readonly DapperContext _context;
        private readonly IClientRepository _clientRepository;
        private readonly ISecurityRepository _securityRepository;
        public AccountRepository(DapperContext context, IClientRepository clientRepository, ISecurityRepository securityRepository)
        {
            _context = context;
            _clientRepository = clientRepository;
            _securityRepository = securityRepository;
        }
        public async Task<Client> ClientLogin(LoginCredential credential)
        {
            Client getClientByUsername = await _clientRepository.GetClientByUsername(credential.Username);
            
            if (getClientByUsername != null)
            {
                var clientSalt = await _securityRepository.GetClientSaltById(getClientByUsername.ID);
                var procedureName = "ValidateClient";

                var Encrypted = _securityRepository.ToHash(credential.Password,clientSalt);
                var parameters = new DynamicParameters();
                parameters.Add("Username", credential.Username, DbType.String, ParameterDirection.Input);
                parameters.Add("Password", Encrypted.Hash, DbType.String, ParameterDirection.Input);
                using (var connection = _context.CreateConnection())
                {
                    var clients = await connection.QueryFirstOrDefaultAsync<Client>
                       (procedureName, parameters, commandType: CommandType.StoredProcedure);
                    return clients;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
