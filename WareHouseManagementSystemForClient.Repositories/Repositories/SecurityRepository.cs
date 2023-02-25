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
        private readonly IConfiguration _config;
        public SecurityRepository(DapperContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
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
        public string GenerateToken(Client client)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("id",client.ID.ToString()),
                new Claim("username", client.Username),
                new Claim("lastname", client.Lastname),
                new Claim("principal", client.Principal.ToString()),
                new Claim("IsActive", client.IsActive.ToString()),

            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
