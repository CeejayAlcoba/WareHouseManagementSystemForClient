using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseManagementSystemForClient.Model.DTOModels.BillingModel
{
    public class BeginningBalance
    {
        public double? TotalQuantity { get; set; }
        public double? TotalVolume { get; set; }
        public double? StorageBill { get; set; }
        public DateTime? BillingDate { get; set; }
    }
}
