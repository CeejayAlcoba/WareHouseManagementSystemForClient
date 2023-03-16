using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseManagementSystemForClient.Model.CargoModels
{
    public class CargoTableReq
    {
        public int Id { get; set; }
        public int RowSkip { get; set; }
        public int RowTake { get; set; }
    }
}
