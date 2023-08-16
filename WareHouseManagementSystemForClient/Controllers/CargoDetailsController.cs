using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public CargoDetailsController(ICargoRepository cargoRepository)
        {
            _cargoRepository = cargoRepository;
        }
        [HttpPost]
        public async Task<IActionResult> GetAllByPrincipal(int principalId, int rowSkip, int rowTake)
        {
            try
            {
                var cargoDetails = await _cargoRepository.GetAllByPrincipal(principalId, rowSkip, rowTake);
                if (cargoDetails.Item1 == null)
                {
                    return NotFound();
                }
                return Ok(new { 
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
                var totalSKU = await _cargoRepository.GetCargoDetailsTotalSKU(principalId);
                return Ok(new
                {
                    CargoDetails =(new
                    {
                        TotalQuantity = totalQuantity,
                        TotalVolume = totalVolume,
                        TotalSKU = totalSKU
                    })
                    
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }  
        }
    }
}
