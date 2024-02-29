using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseManagementSystemForClient.Model.DTOModels.BillingModel
{
    public class HandlingInBillDTO : HandlingInTotal
    {
        public List<HandlingInItem> HandlingInItems { get; set; } = new List<HandlingInItem>();

    }
    public class HandlingInItem
    {
        public DateTime? BillingDate { get; set; }
        public double Quantity { get; set; } = 0;
        public double UomValue { get; set; } = 0;

        public string ICR { get; set; }
    }
    public class HandlingInTotal
    {
        public string UOM { get; set; } = "CBM";
        public double TotalCharge { get; set; } = 0;
        public double TotalUomValue { get; set; } = 0;
        public double HandlingInBill_Det { get; set; } = 0;
    }

    public class HandlingInSqlColumns : HandlingInItem
    {
        public string UOM { get; set; } = "CBM";
        public double TotalCharge { get; set; } = 0;
        public double TotalUomValue { get; set; } = 0;
        public double HandlingInBill_Det { get; set; } = 0;
    }
}
