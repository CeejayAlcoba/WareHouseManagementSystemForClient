using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseManagementSystemForClient.Model.DTOModels.BillingModel
{
    public class BillingDetails
    {
        public int? BillingId { get; set; }
        public double? HandlingIn { get; set; }
        public double? HandlingOut { get; set; }
        public double? Storage { get; set; }
        public int? PrincipalId { get; set; }
        public int? CargoId { get; set; }
        public double? Vat { get; set; }
    }
}
