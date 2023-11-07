using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WareHousemanagementSystemForClient.Interfaces.Interfaces;
using WareHouseManagementSystemForClient.Model.ResponseModels;
using WareHouseManagementSystemForClient.Model.URLSearchParameterModels;

namespace WareHouseManagementSystemForClient.Controllers
{
    [Route("api/inbound")]
    [ApiController]
    public class InboundController : ControllerBase
    {
        private readonly IInboundRepository _inboundRepository;
        public InboundController(IInboundRepository inboundRepository)
        {
            _inboundRepository = inboundRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetInventoryList([FromQuery]ReportsURLSearch urlSearch)
        {
            try
            {
                var inbounds = await _inboundRepository.GetInboundList(urlSearch);
                return Ok(new OkResponse
                { 
                   Data = inbounds,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse
                {
                    Message=ex.Message,
                });
            }

        }
    }
}
