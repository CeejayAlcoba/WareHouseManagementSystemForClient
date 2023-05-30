using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WareHousemanagementSystemForClient.Interfaces.Interfaces;
using WareHouseManagementSystemForClient.Model.SecurityModels;

namespace WareHouseManagementSystemForClient.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepo;
        private readonly ISecurityRepository _securityRepo;
        public AccountController(IAccountRepository accountRepo,ISecurityRepository securityRepository)
        {
            _accountRepo = accountRepo;
            _securityRepo = securityRepository;
        }
        [HttpPost("login")]
        public async Task<IActionResult> ClientLogin([FromBody]LoginCredential credential)
        {
            try
            {
               var client = await _accountRepo.ClientLogin(credential);
               if(client != null)
                {
                    var token = await _securityRepo.GenerateJSONWebToken(client);
                    return Ok(token);
                }
                else
                {
                    return BadRequest("Invalid username and password");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
