﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WareHousemanagementSystemForClient.Interfaces.Interfaces;
using WareHouseManagementSystemForClient.Model.ResponseModels;
using WareHouseManagementSystemForClient.Model.URLSearchParameterModels;

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
        [HttpGet]
        public async Task<IActionResult> GetOutboundList([FromQuery]ReportsURLSearch urlSearch)
        {
            try
            {
                var outbounds = await _outboundRepository.GetOutboundList(urlSearch);
                return Ok(new OkResponse
                {
                    Data = outbounds
                });
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
