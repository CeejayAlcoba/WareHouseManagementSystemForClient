using Dapper;
using System.Data;
using WareHousemanagementSystemForClient.Interfaces.Interfaces;
using WareHouseManagementSystemForClient.DbContext.Context;
using WareHouseManagementSystemForClient.Model.DTOModels.MenuItemsModel;

namespace WareHouseManagementSystemForClient.Repositories.Repositories
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly IGenericRepository _genericRepository;
        public MenuItemRepository(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public async Task<IEnumerable<MenuItemDTO>> GetMenuItems()
        {
            var procedureName = "CLIENT_GetMenuItems";

            var menuItems = await _genericRepository.GetAllAsync<MenuItemDTO>(procedureName,null);

            return menuItems;
           
        }
    }
}
