using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseManagementSystemForClient.Model.DTOModels.ReportModels
{
    public class ReportItem
    {
        public string? ArticleNoDescription { get; set; }
        public double? Qty { get; set; }
        public string? UOM { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? PalletAddress { get; set; }
    }
}
