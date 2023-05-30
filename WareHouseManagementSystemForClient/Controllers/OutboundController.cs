using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WareHousemanagementSystemForClient.Interfaces.Interfaces;
using WareHouseManagementSystemForClient.Model.CargoModels;

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
        public async Task<IActionResult> GetInventoryList(DateTime? AcceptedDateFrom, DateTime? AcceptedDateTo, string? searchRep, int? categoryId, int principalId, int? cargoType, int rowSkip, int rowTake)
        {
            try
            {
                var outbounds = await _outboundRepository.GetOutboundList(AcceptedDateFrom, AcceptedDateTo, searchRep, categoryId, principalId, cargoType, rowSkip, rowTake);
                return Ok(new {
                    Outbounds = outbounds.Item1,
                    Count = outbounds.Item2,
                    TotalQuantity = outbounds.Item3,
                    TotalVolume = outbounds.Item4
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
