using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseManagementSystemForClient.Model.URLSearchParameterModels
{
    public class CargoURLSearch
    {
        public int? PrincipalId { get; set; } 
        public string? Search { get; set; } 
        public string? Sku { get; set; }
        public int? RowTake { get; set; }
        public int? PageNumber { get; set; }
    }
}
