using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseManagementSystemForClient.Model.ParamRequestModels
{
    public class ParamRequestForReports
    {
        public int PrincipalId { get; set; } = 0;
        public DateTime? DateFrom { get; set; } = null;
        public DateTime? DateTo { get; set; } = null;
        public string Sku { get; set; } = string.Empty;
        public int? CargoType { get; set; } = null;
        public string Search { get;set; } = string.Empty;
        public int? RowTake { get; set; } = null;
        public int? RowSkip { get; set;} = null;

    }
}
