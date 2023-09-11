using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WareHousemanagementSystemForClient.Interfaces.Interfaces;
using WareHouseManagementSystemForClient.Model.ParamRequestModels;

namespace WareHouseManagementSystemForClient.Controllers
{
    [Route("api/inventory")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryRepository _inventoryRepository;
        public InventoryController(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }
        [HttpPost]
        public async Task<IActionResult> GetInventoryList(ParamRequestForReports paramRequest)
        {
            try
            {
                var inventories = await _inventoryRepository.GetInventoryList(paramRequest);
                return Ok(new {
                    Inventories= inventories.Item1,
                    Count = inventories.Item2,
                    TotalQuantity=inventories.Item3
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
