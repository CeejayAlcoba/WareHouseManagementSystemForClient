using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseManagementSystemForClient.Model.DTOModels.SecurityModels
{
    public class Security
    {
        public string Hash { get; set; }
        public byte[] Salt { get; set; }
    }
}
