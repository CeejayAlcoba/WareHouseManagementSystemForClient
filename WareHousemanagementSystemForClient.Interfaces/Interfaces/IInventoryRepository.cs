using WareHouseManagementSystemForClient.Model.DTOModels.ReportModels;
using WareHouseManagementSystemForClient.Model.URLSearchParameterModels;

namespace WareHousemanagementSystemForClient.Interfaces.Interfaces
{
    public interface IInventoryRepository
    {
        Task<ReportDTO> GetInventoryList(ReportsURLSearch urlSearch);
    }
}
