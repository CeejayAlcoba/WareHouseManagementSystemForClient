using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouseManagementSystemForClient.Model.DTOModels.ClientModels;
using WareHouseManagementSystemForClient.Model.DTOModels.SecurityModels;

namespace WareHousemanagementSystemForClient.Interfaces.Interfaces
{
    public interface IAccountRepository
    {
        Task<ClientDTO> ClientLogin(LoginCredential credential);
    }
}
