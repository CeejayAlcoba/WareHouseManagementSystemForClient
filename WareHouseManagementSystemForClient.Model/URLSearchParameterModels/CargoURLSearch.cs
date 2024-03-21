using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouseManagementSystemForClient.Model.DTOModels.ReportModels;

namespace WareHouseManagementSystemForClient.Model.URLSearchParameterModels
{
    public class CargoURLSearch : ReportItem
    {
        public int? PrincipalId { get; set; } 
        public int? RowTake { get; set; }
        public int? PageNumber { get; set; }
    }
}
