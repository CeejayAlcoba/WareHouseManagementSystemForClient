using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseManagementSystemForClient.Model.DTOModels.BillingModel
{
    public class BillingDTO
    {
        public IEnumerable<BillingItem> BillingItems { get; set; } = new List<BillingItem>();
        public double TotalVolume { get; set; }
        public double TotalCharge { get; set; }
    }
}
