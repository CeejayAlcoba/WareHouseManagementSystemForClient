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
        public async Task<IActionResult> GetAllByPrincipal(int principalId, int? rowSkip, int? rowTake,string? search)
        {
            try
            {
                var cargoDetails = await _cargoRepository.GetAllByPrincipal(principalId, rowSkip, rowTake, search);
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
                var inventories = await _inventoryRepository.GetInventoryList(null, null, null, principalId, null, null, null);
                return Ok(new
                {
                    CargoDetails = (new
                    {
                        TotalSKU = inventories.Item2,
                        TotalQuantity = inventories.Item3,
                        TotalVolume = inventories.Item4
                    })

                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("sku-records/{principalId}")]
        public async Task<IActionResult> GetSKURecords(int principalId, int? rowSkip, int? rowTake,string? search)
        {
            var skus = await _cargoRepository.GetCargoDetailsSKURecords(principalId, rowSkip, rowTake, search);
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
                var inventory = await _inventoryRepository.GetInventoryList(null, null, null, cargoDetailsRequest.PrincipalId, null, null, null);
                var cargoDetailsNameFitered = inventory.Item1.Where(a => a.CargoName == cargoDetailsRequest.CargoName);
                int totalCount = cargoDetailsNameFitered.Count();
                if (cargoDetailsRequest.RowTake!=null && cargoDetailsRequest.RowSkip!=null)
                {
                    int customRowSkip = ((int)cargoDetailsRequest.RowSkip - 1) * 8;
                    cargoDetailsNameFitered = cargoDetailsNameFitered.Skip(customRowSkip).Take((int)cargoDetailsRequest.RowTake);
                }
             
                return Ok(new
                {
                    CargoDetails = cargoDetailsNameFitered,
                    TotalCount = totalCount,

                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("all-skus/{principalId}")]
        public async Task<IActionResult> GetAllSKUByPrincipalId(int principalId)
        {
            try
            {
                var skus = await _cargoRepository.GetAllSKUByPrincipalId(principalId);

                return Ok(new
                {
                    Skus = skus

                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
