
using Microsoft.AspNetCore.Mvc;
using WareHousemanagementSystemForClient.Interfaces.Interfaces;
using WareHouseManagementSystemForClient.Model.ResponseModels;

namespace WareHouseManagementSystemForClient.Controllers
{
    [Route("api/dropdown")]
    [ApiController]
    public class DropdownController : ControllerBase
    {
        private readonly IDropdownRepository _dropdownRepository;
        public DropdownController(IDropdownRepository dropdownRepository)
        {
            _dropdownRepository = dropdownRepository;
        }
        [Route("sku")]
        [HttpGet]
        public async Task<IActionResult> GetSKUDropdown([FromQuery] int principalId)
        {
            try
            {
                var skus = await _dropdownRepository.GetSKUDropdown(principalId);

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
    }
}
