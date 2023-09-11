using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WareHousemanagementSystemForClient.Interfaces.Interfaces;
using WareHouseManagementSystemForClient.Model.ParamRequestModels;

namespace WareHouseManagementSystemForClient.Controllers
{
    [Route("api/outbound")]
    [ApiController]
    public class OutboundController : ControllerBase
    {
        private readonly IOutboundRepository _outboundRepository;
        public OutboundController(IOutboundRepository outboundRepository)
        {
            _outboundRepository = outboundRepository;
        }
        [HttpPost]
        public async Task<IActionResult> GetOutboundList(ParamRequestForReports paramRequest)
        {
            try
            {
                var outbounds = await _outboundRepository.GetOutboundList(paramRequest);
                return Ok(new {
                    Outbounds = outbounds.Item1,
                    Count = outbounds.Item2,
                    TotalQuantity = outbounds.Item3
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
