using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WareHousemanagementSystemForClient.Interfaces.Interfaces;
using WareHouseManagementSystemForClient.Model.DTOModels.ReportModels;
using WareHouseManagementSystemForClient.Model.ResponseModels;
using WareHouseManagementSystemForClient.Model.URLSearchParameterModels;

namespace WareHouseManagementSystemForClient.Controllers
{

    [Route("api/cargo-details")]
    [ApiController]
    //[Authorize]
    public class CargoDetailsController : ControllerBase
    {

        private readonly ICargoRepository _cargoRepository;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IMapper _mapper;
        public CargoDetailsController(ICargoRepository cargoRepository, IInventoryRepository inventoryRepository, IMapper mapper)
        {
            _cargoRepository = cargoRepository;
            _inventoryRepository = inventoryRepository;
            _mapper = mapper;
        }
        [Route("cargo-totals")]
        [HttpGet]
        public async Task<IActionResult> GetCargoDetailsTotals([FromQuery]int principalId)
        {
            

            try
            {
                var paramMapped = new ReportsURLSearch
                {
                    PrincipalId = principalId
                };

                var inventories = await _inventoryRepository.GetInventoryList(paramMapped);
                var totalsMapped = _mapper.Map<ReportTotalsDTO>(inventories);

                return Ok(new OkResponse
                {
                    Data = totalsMapped,
                    Message="Ok",

                });
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse
                {
                    Message = ex.Message,

                });
            }
        }

        [Route("sku-records")]
        [HttpGet]
        public async Task<IActionResult> GetSKURecords([FromQuery]CargoURLSearch urlSearch)
        {
            try
            {
                var paramMapped = _mapper.Map<ReportsURLSearch>(urlSearch);

                var skus = await _inventoryRepository.GetInventoryList(paramMapped);
                return Ok(new OkResponse
                {
                    Data = skus,
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

        [HttpGet]
        public async Task<IActionResult> GetCargoDetails([FromQuery]CargoURLSearch urlSearch)
        {
         
            try
            {
                var paramMapped = _mapper.Map<ReportsURLSearch>(urlSearch);
                var cargos = await _inventoryRepository.GetInventoryList(paramMapped);
                return Ok(new OkResponse
                {
                   Data=cargos,
                });
            }
            catch (Exception ex)
            {
                return Ok(new BadRequestResponse
                {
                    Message = ex.Message
                });
            }
        }
        //[Route("item")]
        //[HttpGet]
        //public async Task<IActionResult> GetCargoDetail([FromQuery] int cargoId)
        //{
        //    var paramMapped = _mapper.Map<ParamRequestForReports>(cargoId);
        //    try
        //    {
        //        var cargos = await _cargoRepository.
        //        return Ok(new OkResponse
        //        {
        //            Data = cargos,
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new BadRequestResponse
        //        {
        //            Message = ex.Message
        //        });
        //    }
        //}
    }
}
