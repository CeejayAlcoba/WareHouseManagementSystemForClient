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
    public class StorageItem
    {
        public DateTime? BillingDate { get; set; }
        public DateTime? CutOff { get; set; }
        public double Quantity { get; set; } = 0;
        public double UomValue { get; set; } = 0;
        public string ActionType { get; set; }
        public int NODays { get; set; } = 0;
        public double CurrentQuantity { get; set; } = 0;
        public double CurrentUomValue { get; set; } = 0;
        public double StorageCharge { get; set; } = 0;
    }
    public class StorageReportModel : StorageBillingDetail
    {
        public List<StorageItem> StorageItemList { get; set; } = new List<StorageItem>();

    }
    public class StorageBillingDetail
    {
        public double StorageBill { get; set; } = 0;
        public double Vat { get; set; } = 0;
        public double StorageBillType { get; set; } = 0;
        public string UOM { get; set; }
        public double TotalUomValue { get; set; } = 0;
        public double TotalCharge { get; set; } = 0;
    }
}
