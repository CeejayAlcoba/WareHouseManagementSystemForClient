using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using WareHousemanagementSystemForClient.Interfaces.Interfaces;
using WareHouseManagementSystemForClient.Model.DTOModels.AccountModels;
using WareHouseManagementSystemForClient.Model.DTOModels.ClientModels;
using WareHouseManagementSystemForClient.Model.DTOModels.SecurityModels;
using WareHouseManagementSystemForClient.Model.ResponseModels;
using WareHouseManagementSystemForClient.Repositories.Repositories;

namespace WareHouseManagementSystemForClient.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepo;
        private readonly ISecurityRepository _securityRepo;
        private readonly IClientRepository _clientRepo;
        private readonly IMapper _mapper;
        public AccountController(IAccountRepository accountRepo, ISecurityRepository securityRepository, IClientRepository clientRepo,IMapper mapper)
        {
            _accountRepo = accountRepo;
            _securityRepo = securityRepository;
            _clientRepo = clientRepo;
            _mapper = mapper;
        }
        [HttpPost("login")]
        public async Task<IActionResult> ClientLogin([FromBody] LoginCredential credential)
        {
            try
            {
                var client = await _accountRepo.ClientLogin(credential);
                if (client != null)
                {
                    var token = await _securityRepo.GenerateJSONWebToken(client);
                    return Ok(new OkResponse
                    {
                        Data = token
                    });
                }
                else
                {
                    return BadRequest(new BadRequestResponse
                    {
                        Message = "Invalid username and password"
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse
                {
                    Message = ex.Message
                });
            }
        }
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] AccountChangePassword accountChange)
        {
            try
            {
                if (accountChange.Id == 0 ) return BadRequest(new BadRequestResponse
                {
                    Message = "Error occured."
                });
                if (accountChange.RetypedPassword != accountChange.NewPassword) return BadRequest(new BadRequestResponse
                {
                    Message = "New password doesn't match with retype password."
                });

                var credential = _mapper.Map<LoginCredential>(accountChange);
                var clientLogged = await _accountRepo.ClientLogin(credential);
                if (clientLogged == null || clientLogged.ID != accountChange.Id) return BadRequest(new BadRequestResponse
                {
                    Message = "Invalid username and password"
                });
                var clientSalt = await _securityRepo.GetClientSaltById(clientLogged.ID);

                var clientForSave = new ClientForSave()
                {
                    ID = accountChange.Id,
                    Password = _securityRepo.ToHash(accountChange.NewPassword, clientSalt).Hash
                };
             
                await _clientRepo.SaveClientCredential(clientForSave);

                return Ok( new OkResponse { Message = "Successfully saved!" });

            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse
                {
                    Message = ex.Message
                });
            }
        }
    }
}
