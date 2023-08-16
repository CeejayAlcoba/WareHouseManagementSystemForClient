using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseManagementSystemForClient.Model.CargoModels
{
    public class CargoDetails
    {
        public string CargoName { get; set; }
        public double TotalQuantity { get; set; }
        public string UnitOfMeasurement { get; set; }
        public double TotalVolume { get; set; }
    }
}
