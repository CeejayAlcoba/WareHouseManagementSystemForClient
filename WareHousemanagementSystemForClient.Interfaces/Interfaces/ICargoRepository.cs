using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouseManagementSystemForClient.Model.ParamRequestModels;
using WareHouseManagementSystemForClient.Model.ReportModels;

namespace WareHousemanagementSystemForClient.Interfaces.Interfaces
{
    public interface ICargoRepository
    {
        Task<(IEnumerable<ReportDataTable>, int, double?)> GetAllByPrincipal(ParamRequestForCargos paramRequest);
        Task<IEnumerable<string>> GetSKUNamesByPrincipalId(int principalId);
    }
}
