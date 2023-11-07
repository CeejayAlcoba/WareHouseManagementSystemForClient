using AutoMapper;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using WareHousemanagementSystemForClient.Interfaces.Interfaces;
using WareHouseManagementSystemForClient.DbContext.Context;
using WareHouseManagementSystemForClient.Model.DTOModels.AccountModels;
using WareHouseManagementSystemForClient.Model.DTOModels.ClientModels;
using WareHouseManagementSystemForClient.Model.DTOModels.SecurityModels;

namespace WareHouseManagementSystemForClient.Repositories.Repositories
{
    public class SecurityRepository : ISecurityRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IPrincipalRepository _principalRepository;
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;
        public SecurityRepository(IConfiguration configuration, IPrincipalRepository principalRepository, IGenericRepository genericRepository, IMapper mapper)
        {

            _configuration = configuration;
            _principalRepository = principalRepository;
            _genericRepository = genericRepository;
            _mapper = mapper;
        }
        public async Task<byte[]> GetClientSaltById(int id)
        {
            var procedureName = "CLIENT_GetClientSaltById";
            var parameters = new DynamicParameters();
            parameters.Add("ID", id, DbType.String, ParameterDirection.Input);

            var clientSalt = await _genericRepository.GetFirstOrDefaultAsync<string>(procedureName, parameters);

            byte[] saltBytes = Convert.FromBase64String(clientSalt);
            return saltBytes;
        }
        public Security ToHash(string password, byte[] salt)
        {
            var passwordBytes = new Rfc2898DeriveBytes(password, salt, 10000);
            var passwordHashed = Convert.ToBase64String(passwordBytes.GetBytes(256));

            Security hashSalt = new Security { Hash = passwordHashed, Salt = salt };
            return hashSalt;
        }
        public async Task<string> GenerateJSONWebToken(ClientDTO client)
        {
            var principals = await _principalRepository.GetClientPrincipalsByClientId(client.ID);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var account = new AccountDTO
            {
                Username = client.Username,
                Id = client.ID,
                Firstname = client.Firstname,
                Lastname = client.Lastname,
                IsActive = client.IsActive,
                Principals = principals.ToList(),
            };

            var claims = new[] {
               new Claim("data", JsonSerializer.Serialize(account))
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
