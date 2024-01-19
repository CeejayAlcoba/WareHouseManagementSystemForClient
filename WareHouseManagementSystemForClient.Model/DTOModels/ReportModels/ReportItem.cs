using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseManagementSystemForClient.Model.DTOModels.ReportModels
{
    public class ReportItem
    {
        public string? CargoName { get; set; }
        public string? Description { get; set; }
        public double? Quantity { get; set; }
        public string? UOM { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? ExpDate { get; set; }
        public string? BINLocation { get; set; }
        public double? Vat { get; set; }
    }
}
