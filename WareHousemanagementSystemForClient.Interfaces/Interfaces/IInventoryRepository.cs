using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouseManagementSystemForClient.Model.ParamRequestModels;
using WareHouseManagementSystemForClient.Model.ReportModels;

namespace WareHousemanagementSystemForClient.Interfaces.Interfaces
{
    public interface IInventoryRepository
    {
        Task<(IEnumerable<ReportDataTable>, int, double?)> GetInventoryList(ParamRequestForReports paramRequest);
    }
}
