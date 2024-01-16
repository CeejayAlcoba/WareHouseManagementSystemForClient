using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseManagementSystemForClient.Model.DTOModels.BillingModel
{
    public class StorageBill
    {
            public DateTime? BillingDate { get; set; }
            public DateTime? CutOff { get; set; }
            public double? Quantity { get; set; }
            public double? Volume { get; set; }
            public double? HIQuantity { get; set; }
            public double? HOQuantity { get; set; }
            public double? HIVolume { get; set; }
            public double? HOVolume { get; set; }
            public int NODays { get; set; }
            public double? StorageCharge { get; set; }
        
    }
}
