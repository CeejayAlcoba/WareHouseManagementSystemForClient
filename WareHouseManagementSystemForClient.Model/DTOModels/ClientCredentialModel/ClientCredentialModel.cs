using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseManagementSystemForClient.Model.DTOModels.ClientCredentialModel
{
    public class ClientCredentialModel
    {
        public int? UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Salt { get; set; }
        public bool? IsActive { get; set; }
    }
}
