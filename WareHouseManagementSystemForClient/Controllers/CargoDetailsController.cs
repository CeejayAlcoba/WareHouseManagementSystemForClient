using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WareHousemanagementSystemForClient.Interfaces.Interfaces;
using WareHouseManagementSystemForClient.Model.CargoModels;

namespace WareHouseManagementSystemForClient.Controllers
{
 
    [Route("api/cargo-details")]
    [ApiController]
    [Authorize]
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
                return Ok(new { CargoDetails = cargoDetails.Item1, TotalCount = cargoDetails.Item2 });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("total-items/{principalId}")]
        public async Task<IActionResult> GetTotalItems(int principalId)
        {
            try
            {
                var totalItems = await _cargoRepository.GetTotalItems(principalId);
                
                return Ok(totalItems);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("total-handling-in/{principalId}")]
        public async Task<IActionResult> GetTotalHandingIn(int principalId)
        {
            try
            {
                var totalItems = await _cargoRepository.GetTotalHandlingIn(principalId);

                return Ok(totalItems);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("total-handling-out/{principalId}")]
        public async Task<IActionResult> GetTotalHandingOut(int principalId)
        {
            try
            {
                var totalItems = await _cargoRepository.GetTotalHandlingOut(principalId);

                return Ok(totalItems);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("cbm/{principalId}")]
        public async Task<IActionResult> GetCBM(int principalId)
        {
            try
            {
                var totalItems = await _cargoRepository.GetCBM(principalId);

                return Ok(totalItems);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
