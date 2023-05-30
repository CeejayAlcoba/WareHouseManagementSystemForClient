using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WareHousemanagementSystemForClient.Interfaces.Interfaces;
using WareHouseManagementSystemForClient.Model.CargoModels;

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
        public async Task<IActionResult> GetInventoryList(DateTime? AcceptedDateFrom, DateTime? AcceptedDateTo, string? searchRep, int? categoryId, int principalId, int? cargoType, int rowSkip, int rowTake)
        {
            try
            {
                var inventories = await _inventoryRepository.GetInventoryList(AcceptedDateFrom, AcceptedDateTo, searchRep, categoryId, principalId, cargoType, rowSkip, rowTake);
                return Ok(new {
                    Inventories= inventories.Item1,
                    Count = inventories.Item2,
                    TotalQuantity=inventories.Item3,
                    TotalVolume =inventories.Item4
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
