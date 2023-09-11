
using WareHouseManagementSystemForClient.Model.ParamRequestModels;
using WareHouseManagementSystemForClient.Model.ReportModels;

namespace WareHousemanagementSystemForClient.Interfaces.Interfaces
{
    public interface IInboundRepository
    {
        Task<(IEnumerable<ReportDataTable>, int, double?)> GetInboundList(ParamRequestForReports paramRequest);
    }
}
