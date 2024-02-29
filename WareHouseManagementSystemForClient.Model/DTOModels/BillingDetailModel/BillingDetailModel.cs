using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseManagementSystemForClient.Model.DTOModels.BillingDetailModel
{
    public class BillingDetailModel
    {
        public int BillingID { get; set; }
        public int? PrincipalID { get; set; }
        public int? CargoID { get; set; }
        public float VAT { get; set; } = 0;
        public float HandlingIn { get; set; } = 0;
        public float HandlingOut { get; set; } = 0;
        public float Storage { get; set; } = 0;
        public bool HandlingInBillType { get; set; } = false;
        public bool HandlingOutBillType { get; set; } = false;
        public bool StorageBillType { get; set; } = false;
    }
}
