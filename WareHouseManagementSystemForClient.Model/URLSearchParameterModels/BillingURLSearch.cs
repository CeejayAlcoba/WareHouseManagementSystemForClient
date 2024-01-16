using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseManagementSystemForClient.Model.URLSearchParameterModels
{
    public class BillingURLSearch
    {
        public int? PrincipalId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int? CategoryId { get; set; }
        public int? CargoType { get; set; }
    }
}
