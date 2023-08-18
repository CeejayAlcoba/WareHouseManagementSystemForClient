using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseManagementSystemForClient.Model.CargoModelsForTable
{
    public class CargoDetailsForTable
    {
        public string CargoName { get; set; }
        public double TotalQuantity { get; set; }
        public string UnitOfMeasurement { get; set; }
        public double TotalVolume { get; set; }
    }
}
