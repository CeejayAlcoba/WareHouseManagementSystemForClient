using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseManagementSystemForClient.Model.DTOModels.ClientModels
{
    public class ClientDTO
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public bool IsActive { get; set; }
    }
    public class ClientForSave
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Lastname { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
