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
        private readonly IInventoryRepository _inventoryRepository;
        public BillingController(IBillingRepository billingRepository, IInventoryRepository inventoryRepository)
        {
            _billingRepository = billingRepository;
            _inventoryRepository = inventoryRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetBillingReport([FromQuery] BillingURLSearch billing)
        {
            try
            {
              
                var handlingIn = await _billingRepository.GetHandlingIn(billing);
                var handlingOut = await _billingRepository.GetHandlingOut(billing);
                var storage = await _billingRepository.GetStorageBill(billing, handlingIn, handlingOut);
                var total = handlingIn.TotalCharge + handlingOut.TotalCharge + storage.Select(c=>c.StorageCharge).Sum();
                var vat =await _billingRepository.GetVatbyPrincipal(billing.PrincipalId) * total;
                return Ok(new OkResponse
                {
                    Data = new
                    {
                        handlingIn,
                        handlingOut,
                        storage,
                        total = Math.Round((decimal)total,2),
                        vat = Math.Round((decimal)vat, 2),
                        amount = Math.Round((decimal)(vat + total), 2)

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
