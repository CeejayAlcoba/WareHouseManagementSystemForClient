using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouseManagementSystemForClient.Model.DTOModels.MenuItemsModel;

namespace WareHousemanagementSystemForClient.Interfaces.Interfaces
{
    public interface IMenuItemRepository
    {
        Task<IEnumerable<MenuItemDTO>> GetMenuItems();
    }
}
