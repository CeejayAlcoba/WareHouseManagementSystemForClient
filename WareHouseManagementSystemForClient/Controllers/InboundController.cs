﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WareHousemanagementSystemForClient.Interfaces.Interfaces;

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
        public async Task<IActionResult> GetInventoryList(DateTime? asOfDate, string? search, string?  sku, int principalId, int? cargoType, int? rowSkip, int? rowTake)
        {
            try
            {
                var inbounds = await _inboundRepository.GetInboundList(asOfDate, search, sku, principalId, cargoType, rowSkip, rowTake);
                return Ok(new { 
                    Inbounds = inbounds.Item1,
                    Count = inbounds.Item2,
                    TotalQuantity = inbounds.Item3,
                    TotalVolume = inbounds.Item4

                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
