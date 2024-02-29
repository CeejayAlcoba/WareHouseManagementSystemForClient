﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseManagementSystemForClient.Model.DTOModels.BillingModel
{
    public class BillingSQL1
    {
        public DateTime BillingDate { get; set; }
        public double Volume { get; set; }
        public double Quantity { get; set; }
        public string ICR { get; set; }
        public string OCR { get; set; }
        public double TotalVolume { get; set; }
        public double TotalCharge { get; set; }
    }
}
