using WareHouseManagementSystemForClient.Model.DTOModels.ReportModels;
using WareHouseManagementSystemForClient.Model.URLSearchParameterModels;

namespace WareHousemanagementSystemForClient.Interfaces.Interfaces
{
    public interface IInboundRepository
    {
        Task<ReportDTO> GetInboundList(ReportsURLSearch urlSearch);
    }
}
