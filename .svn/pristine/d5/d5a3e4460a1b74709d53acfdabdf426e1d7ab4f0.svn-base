using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHousemanagementSystemForClient.Interfaces.Interfaces;
using WareHouseManagementSystemForClient.DbContext.Context;
using WareHouseManagementSystemForClient.Model.MenuItemModels;

namespace WareHouseManagementSystemForClient.Repositories.Repositories
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly DapperContext _context;
        public MenuItemRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<MenuItem>> GetMenuItems()
        {
            var procedureName = "GetMenuItems";
            using (var connection = _context.CreateConnection())
            {
                var menuItems = await connection.QueryAsync<MenuItem>
                   (procedureName,commandType: CommandType.StoredProcedure);

                return menuItems.ToList();
            }
        }
    }
}
