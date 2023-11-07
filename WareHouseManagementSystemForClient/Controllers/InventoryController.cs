using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WareHousemanagementSystemForClient.Interfaces.Interfaces;
using WareHouseManagementSystemForClient.Model.ResponseModels;
using WareHouseManagementSystemForClient.Model.URLSearchParameterModels;

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
        [HttpGet]
        public async Task<IActionResult> GetInventoryList([FromQuery]ReportsURLSearch urlSearch)
        {
            try
            {
                var inventories = await _inventoryRepository.GetInventoryList(urlSearch);
                return Ok(new OkResponse
                {
                    Data = inventories,
                    Message="Ok",
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
