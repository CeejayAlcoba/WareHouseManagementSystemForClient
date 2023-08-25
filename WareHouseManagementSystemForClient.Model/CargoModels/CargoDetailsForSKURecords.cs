using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseManagementSystemForClient.Model.CargoModels
{
    public class CargoDetailsForSKURecords
    {
        public string? CargoName { get; set; }
        public string? ICRReferenceNo { get; set; }
        public double? Quantity { get; set; }
        public string? Uom { get; set; }
        public double? Volume { get; set; }
        public DateTime? DateAccepted { get; set; }
        public DateTime? ActualCheckinDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime? DateReleased { get; set; }
        public DateTime? DeliveryDate { get; set; }
    }
}
