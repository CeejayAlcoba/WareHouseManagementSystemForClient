using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseManagementSystemForClient.Model.ReportModels
{
    public class ReportDataTable
    {
        public string ArticleNoDescription { get; set; } = string.Empty;
        public double Qty { get; set; } = 0;
        public string UOM { get; set; } = string.Empty;
        public string BatchNo { get;set; } = string.Empty;
        public DateTime? ExpiryDate { get; set; } = null;
        public string PalletAddress { get; set; } = string.Empty;
    }
}
