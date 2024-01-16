using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WareHousemanagementSystemForClient.Interfaces.Interfaces;
using WareHouseManagementSystemForClient.Model.ResponseModels;
using WareHouseManagementSystemForClient.Model.URLSearchParameterModels;

namespace WareHouseManagementSystemForClient.Controllers
{
    [Route("api/billing")]
    [ApiController]
    public class BillingController : ControllerBase
    {
        private readonly IBillingRepository _billingRepository;
        public BillingController(IBillingRepository billingRepository)
        {
            _billingRepository = billingRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetBillingReport([FromQuery] BillingURLSearch billing)
        {
            try
            {
                var handlingIn = await _billingRepository.GetHandlingIn(billing);
                var handlingOut = await _billingRepository.GetHandlingOut(billing);
                var storage = await _billingRepository.GetStorageBill(billing, handlingIn, handlingOut);

                return Ok(new OkResponse
                {
                    Data = new
                    {
                        handlingIn,
                        handlingOut,
                        storage
                    }

                });
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse
                {
                    Message= ex.Message,
                });
            }
          
        }
    }
}
