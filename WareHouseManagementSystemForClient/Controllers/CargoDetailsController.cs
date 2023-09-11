using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WareHousemanagementSystemForClient.Interfaces.Interfaces;
using WareHouseManagementSystemForClient.Model.ParamRequestModels;

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
        [HttpGet("cargo-details-dashboard/{principalId}")]
        public async Task<IActionResult> GetCargoDetailsDashboard(int principalId)
        {
            ParamRequestForReports paramRequest = new ParamRequestForReports()
            {
                PrincipalId = principalId,
                DateFrom = null,
                DateTo = null,
                Search= "",
                Sku = "",
                CargoType = 3,
                RowSkip=null,
                RowTake = null,
            };

            try
            {
                var inventories = await _inventoryRepository.GetInventoryList(paramRequest);
                return Ok(new
                {
                    CargoDetails = (new
                    {
                        TotalSKU = inventories.Item2,
                        TotalQuantity = inventories.Item3
                    })

                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost("sku-records")]
        public async Task<IActionResult> GetSKURecords(ParamRequestForCargos paramRequest)
        {
            ParamRequestForReports paramRequestForReports = new ParamRequestForReports()
            {
                PrincipalId= paramRequest.PrincipalId,
                Search = paramRequest.Search,
                Sku= paramRequest.Sku,
                RowSkip= paramRequest.RowSkip,
                RowTake= paramRequest.RowTake,
                DateFrom=null,
                DateTo=null,
            };
            var skus = await _inventoryRepository.GetInventoryList(paramRequestForReports);
            return Ok(new
            {
                Data = skus.Item1,
                TotalCount = skus.Item2,
                TotalQuantity = skus.Item3
            });
        }

        [HttpPost("cargo-details")]
        public async Task<IActionResult> GetCargoDetails(ParamRequestForCargos paramRequest)
        {
         
            try
            {
                var cargos = await _cargoRepository.GetAllByPrincipal(paramRequest);
                return Ok(new
                {
                    CargoDetails = cargos.Item1,
                    TotalCount = cargos.Item2,
                    TotalQuantity = cargos.Item3
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("cargo-detail")]
        public async Task<IActionResult> GetCargoDetail(ParamRequestForCargos paramRequest)
        {
            ParamRequestForReports paramRequestForReports = new ParamRequestForReports()
            {
                PrincipalId = paramRequest.PrincipalId,
                DateFrom = null,
                DateTo = null,
                Search = paramRequest.Search,
                Sku = paramRequest.Sku,
                CargoType = 3,
                RowSkip = paramRequest.RowSkip,
                RowTake = paramRequest.RowTake,
            };
            try
            {
                var cargos = await _inventoryRepository.GetInventoryList(paramRequestForReports);
                return Ok(new
                {
                    CargoDetails = cargos.Item1,
                    TotalCount = cargos.Item2,
                    TotalQuantity = cargos.Item3
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("sku-names/{principalId}")]
        public async Task<IActionResult> GetSKUNamesByPrincipalId(int principalId)
        {
            try
            {
                var skus = await _cargoRepository.GetSKUNamesByPrincipalId(principalId);

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
