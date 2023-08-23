using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouseManagementSystemForClient.Model.CargoModels;
using WareHouseManagementSystemForClient.Model.CargoModelsForTable;

namespace WareHousemanagementSystemForClient.Interfaces.Interfaces
{
    public interface ICargoRepository
    {
        Task<(IEnumerable<CargoDetailsForTable>, int, double?)> GetAllByPrincipal(int principalId, int? rowSkip, int? rowTake,string? search);
        Task<double> GetCargoDetailsTotalQuantity(int principalId);
        Task<double> GetCargoDetailsTotalVolume(int principalId);
        Task<double> GetCargoDetailsTotalSKU(int principalId);
        Task<(double, IEnumerable<CargoDetailsForSKURecords>)> GetCargoDetailsSKURecords(int principalId, int? rowSkip, int? rowTake, string? search);
    }
}
