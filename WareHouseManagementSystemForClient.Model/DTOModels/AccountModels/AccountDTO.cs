using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouseManagementSystemForClient.Model.DTOModels.ClientModels;
using WareHouseManagementSystemForClient.Model.DTOModels.PrincipalModels;

namespace WareHouseManagementSystemForClient.Model.DTOModels.AccountModels
{
    public class AccountDTO
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public bool? IsActive { get; set; }
        public List<PrincipalDTO> Principals { get; set; } = new List<PrincipalDTO>();
 
    }
    public class AccountChangePassword 
    {

        public int Id { get; set; }
        public string Username { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string RetypedPassword { get; set; }
    }
}
