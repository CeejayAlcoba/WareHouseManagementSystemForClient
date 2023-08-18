using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WareHousemanagementSystemForClient.Interfaces.Interfaces;
using WareHouseManagementSystemForClient.Model.CargoModels;

namespace WareHouseManagementSystemForClient.Controllers
{

    [Route("api/cargo-details")]
    [ApiController]
    //[Authorize]
    public class CargoDetailsController : ControllerBase
    {

        private readonly ICargoRepository _cargoRepository;
        private readonly IInventoryRepository _inventoryRepository;
        public CargoDetailsController(ICargoRepository cargoRepository, IInventoryRepository inventoryRepository)
        {
            _cargoRepository = cargoRepository;
            _inventoryRepository = inventoryRepository;
        }
        [HttpPost]
        public async Task<IActionResult> GetAllByPrincipal(int principalId, int rowSkip, int rowTake,string? search)
        {
            try
            {
                var cargoDetails = await _cargoRepository.GetAllByPrincipal(principalId, rowSkip, rowTake, search);
                if (cargoDetails.Item1 == null)
                {
                    return NotFound();
                }
                return Ok(new
                {
                    CargoDetails = cargoDetails.Item1,
                    TotalCount = cargoDetails.Item2,
                    TotalQuantity = cargoDetails.Item3
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("CargoDetailsDashboard/{principalId}")]
        public async Task<IActionResult> GetCargoDetailsDashboard(int principalId)
        {
            try
            {
                var totalQuantity = await _cargoRepository.GetCargoDetailsTotalQuantity(principalId);
                var totalVolume = await _cargoRepository.GetCargoDetailsTotalVolume(principalId);
                var skuRecords = await _cargoRepository.GetCargoDetailsSKURecords(principalId,null,null);
                return Ok(new
                {
                    CargoDetails = (new
                    {
                        TotalQuantity = totalQuantity,
                        TotalVolume = totalVolume,
                        TotalSKU = skuRecords.Item1
                    })

                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("sku-records/{principalId}")]
        public async Task<IActionResult> GetSKURecords(int principalId, int rowSkip, int rowTake)
        {
            var skus = await _cargoRepository.GetCargoDetailsSKURecords(principalId, rowSkip, rowTake);
            return Ok(new
            {
                totalCount = skus.Item1,
                Data = skus.Item2
                
            });
        }

        [HttpPost("cargo-detail")]
        public async Task<IActionResult> GetCargoDetailsDashboard([FromBody] CargoDetailsRequest cargoDetailsRequest)
        {
            try
            {
                var inventory = await _inventoryRepository.GetInventoryList(null, null, null, null, cargoDetailsRequest.PrincipalId, null, null, null);
                var cargoDetailsNameFitered = inventory.Item1.Where(a => a.CargoName == cargoDetailsRequest.CargoName);
                return Ok(new
                {
                    CargoDetails = cargoDetailsNameFitered

                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
