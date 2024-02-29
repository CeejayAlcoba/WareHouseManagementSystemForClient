using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouseManagementSystemForClient.Model.DTOModels.BillingDetailModel;

namespace WareHousemanagementSystemForClient.Interfaces.Interfaces
{
    public interface IBillingDetailRepositoy
    {
        Task<BillingDetailModel> GetBillingDetailByPrincipalId(int principalId);
    }
}
