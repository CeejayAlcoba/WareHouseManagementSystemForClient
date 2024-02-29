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
        private readonly IBillingDetailRepositoy _billingDetailRepositoy;
        public BillingController(IBillingRepository billingRepository, IBillingDetailRepositoy billingDetailRepositoy)
        {
            _billingRepository = billingRepository;
            _billingDetailRepositoy = billingDetailRepositoy;
        }
        [HttpGet]
        public async Task<IActionResult> GetBillingReport([FromQuery] BillingURLSearch billing)
        {
            try
            {
              
                var handlingIn = await _billingRepository.GetHandlingIn(billing);
                var handlingOut = await _billingRepository.GetHandlingOut(billing);
                var storage = await _billingRepository.GetStorageBillReport(billing, handlingIn, handlingOut);
                var total = handlingIn.TotalCharge + handlingOut.TotalCharge + storage.TotalCharge;
                var vat =  _billingDetailRepositoy.GetBillingDetailByPrincipalId((int)billing.PrincipalId).Result.VAT * total;
                return Ok(new OkResponse
                {
                    Data = new
                    {
                        handlingIn,
                        handlingOut,
                        storage,
                        Total = Math.Round((decimal)total,2),
                        Vat = Math.Round((decimal)vat, 2),
                        Amount = Math.Round((decimal)(vat + total), 2)

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
