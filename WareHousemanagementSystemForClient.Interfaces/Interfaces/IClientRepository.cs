using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouseManagementSystemForClient.Model.DTOModels.ClientModels;

namespace WareHousemanagementSystemForClient.Interfaces.Interfaces
{
    public interface IClientRepository
    {
        Task<ClientDTO> GetClientByUsername(string username);
        Task<int> SaveClientCredential(ClientForSave client);
    }
}
