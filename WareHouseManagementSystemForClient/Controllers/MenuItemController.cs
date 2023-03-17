using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WareHousemanagementSystemForClient.Interfaces.Interfaces;

namespace WareHouseManagementSystemForClient.Controllers
{
    [Route("api/menu-item")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemRepository _menuItemRepository;
        public MenuItemController(IMenuItemRepository menuItemRepository)
        {
            _menuItemRepository = menuItemRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetMenuItems()
        {
            try
            {
                var menuItems = await _menuItemRepository.GetMenuItems();
                return Ok(menuItems);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
