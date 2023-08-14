using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WareHousemanagementSystemForClient.Interfaces.Interfaces;
using WareHouseManagementSystemForClient.DbContext.Context;
using WareHouseManagementSystemForClient.Model.ClientModels;
using WareHouseManagementSystemForClient.Model.SecurityModels;

namespace WareHouseManagementSystemForClient.Repositories.Repositories
{
    public class SecurityRepository : ISecurityRepository
    {
        private readonly DapperContext _context;
        private readonly IConfiguration _configuration;
        private readonly IPrincipalRepository _principalRepository;
        public SecurityRepository(DapperContext context, IConfiguration configuration, IPrincipalRepository principalRepository)
        {
            _context = context;
            _configuration = configuration;
            _principalRepository = principalRepository;
        }
        public async Task<byte[]> GetClientSaltById(int id)
        {
            var procedureName = "GetClientSaltById";
            var parameters = new DynamicParameters();
            parameters.Add("ID", id, DbType.String, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var clientSalt = await connection.QueryFirstOrDefaultAsync<string>
                   (procedureName, parameters, commandType: CommandType.StoredProcedure);
                byte[] saltBytes = Convert.FromBase64String(clientSalt);
                return saltBytes;
            }
        }
        public Security ToHash(string password,byte[] salt)
        {
            var passwordBytes = new Rfc2898DeriveBytes(password, salt, 10000);
            var passwordHashed = Convert.ToBase64String(passwordBytes.GetBytes(256));

            Security hashSalt = new Security { Hash = passwordHashed, Salt = salt };
            return hashSalt;
        }
        public async Task<string> GenerateJSONWebToken(Client client)
        {
            var principals =  await _principalRepository.GetClientPrincipalsByClientId(client.ID);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var principalsJson = JsonSerializer.Serialize(principals);
            var claims = new[] {
               new Claim("id",client.ID.ToString()),
                new Claim("username", client.Username),
                 new Claim("firstname", client.Firstname),
                new Claim("lastname", client.Lastname),
                new Claim("isActive", client.IsActive.ToString()),
                 new Claim("principals", principalsJson),
    };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.UtcNow.AddDays(31),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
