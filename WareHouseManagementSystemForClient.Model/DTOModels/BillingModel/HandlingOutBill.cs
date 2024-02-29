using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseManagementSystemForClient.Model.DTOModels.BillingModel
{
    public class HandlingOutBillDTO : HandlingOutTotal
    {
        public List<HandlingOutItem> HandlingOutItems { get; set; } = new List<HandlingOutItem>();

    }
    public class HandlingOutItem
    {
        public DateTime? BillingDate { get; set; }
        public double Quantity { get; set; } = 0;
        public double UomValue { get; set; } = 0;
        public string OCR { get; set; }
    }
    public class HandlingOutTotal
    {
        public string UOM { get; set; } = "CBM";
        public double TotalCharge { get; set; } = 0;
        public double TotalUomValue { get; set; } = 0;
        public double HandlingOutBill_Det { get; set; } = 0;
    }

    public class HandlingOutSqlColumns : HandlingOutItem
    {
        public string UOM { get; set; } = "CBM";
        public double TotalCharge { get; set; } = 0;
        public double TotalUomValue { get; set; } = 0;
        public double HandlingOutBill_Det { get; set; } = 0;
    }
}
