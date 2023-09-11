using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WareHousemanagementSystemForClient.Interfaces.Interfaces;
using WareHouseManagementSystemForClient.Model.ParamRequestModels;

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
        [HttpPost]
        public async Task<IActionResult> GetInventoryList(ParamRequestForReports paramRequest)
        {
            try
            {
                var inbounds = await _inboundRepository.GetInboundList(paramRequest);
                return Ok(new { 
                    Inbounds = inbounds.Item1,
                    Count = inbounds.Item2,
                    TotalQuantity = inbounds.Item3,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
