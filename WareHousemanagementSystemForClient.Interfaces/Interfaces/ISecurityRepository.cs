using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WareHouseManagementSystemForClient.Model.DTOModels.ClientModels;
using WareHouseManagementSystemForClient.Model.DTOModels.SecurityModels;

namespace WareHousemanagementSystemForClient.Interfaces.Interfaces
{
    public interface ISecurityRepository
    {
        Security ToHash(string password, byte[] salt);
        Task<byte[]> GetClientSaltById(int id);
        Task<string> GenerateJSONWebToken(ClientDTO client);
    }
}
