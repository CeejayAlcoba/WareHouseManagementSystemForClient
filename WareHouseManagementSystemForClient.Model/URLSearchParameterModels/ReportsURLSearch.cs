﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseManagementSystemForClient.Model.URLSearchParameterModels
{
    public class ReportsURLSearch
    {
        public int? CargoId { get; set; }
        public int? PrincipalId { get; set; }
        public DateTime? DateFrom { get; set; } 
        public DateTime? DateTo { get; set; }
        public string? Sku { get; set; } 
        public int? CargoType { get; set; }
        public string? Search { get;set; }
        public int? RowTake { get; set; } 
        public int? PageNumber { get; set;} 

    }
}