using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouseManagementSystemForClient.Model.DTOModels.PrincipalModels;

namespace WareHousemanagementSystemForClient.Interfaces.Interfaces
{
    public interface IPrincipalRepository
    {
        Task<IEnumerable<PrincipalDTO>> GetClientPrincipalsByClientId(int clienId);
    }
}
