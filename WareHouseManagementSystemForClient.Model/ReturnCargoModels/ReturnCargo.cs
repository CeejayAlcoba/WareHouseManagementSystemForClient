using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseManagementSystemForClient.Model.ReturnCargoModels
{
    public class ReturnCargo
    {
        public int ID { get; set; }
        public int? ReferenceID { get; set; }
        public double? Quantity { get; set; }
        public int? BookingID { get; set; }
        public int? Status { get; set; }
        public DateTime? DateTime { get; set; }
        public string? Description { get; set; }
        public double? Volume { get; set; }
    }
}
