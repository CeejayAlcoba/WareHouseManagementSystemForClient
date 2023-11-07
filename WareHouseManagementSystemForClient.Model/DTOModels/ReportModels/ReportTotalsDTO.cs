using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseManagementSystemForClient.Model.DTOModels.ReportModels
{
    public class ReportTotalsDTO
    {
        public double? TotalQuantity { get; set; } = 0;
        public double? TotalSKU { get; set; } = 0;
    }
}
