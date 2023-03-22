using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseManagementSystemForClient.Model.MenuItemModels
{
    public class MenuItem
    {
        public int MenuItemId { get; set; }
        public string DirectoryPath { get; set; }
        public string Name { get; set; }
        public string Icon{ get; set; }
        public int ParentId { get; set; }
    }
}
