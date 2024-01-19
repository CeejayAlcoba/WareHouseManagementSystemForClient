using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouseManagementSystemForClient.Model.DTOModels.BillingModel;
using WareHouseManagementSystemForClient.Model.URLSearchParameterModels;

namespace WareHousemanagementSystemForClient.Interfaces.Interfaces
{
    public interface IBillingRepository
    {
        Task<BillingDTO> GetHandlingIn(BillingURLSearch billing);
        Task<BillingDTO> GetHandlingOut(BillingURLSearch billing);
        Task<List<StorageBill>> GetStorageBill(BillingURLSearch billing, BillingDTO handlingIn, BillingDTO handlingOut);
        Task<double> GetVatbyPrincipal(int? principal);
        Task<BillingDetails> GetBillingDetailByPrincipal(int principalId);
    }
}
