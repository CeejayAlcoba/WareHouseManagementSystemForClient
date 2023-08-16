using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouseManagementSystemForClient.Model.CargoModels;

namespace WareHousemanagementSystemForClient.Interfaces.Interfaces
{
    public interface ICargoRepository
    {
        Task<(IEnumerable<CargoDetails>, int,double?)> GetAllByPrincipal(int principalId, int rowSkip, int rowTake);
        Task<double> GetCargoDetailsTotalQuantity(int principalId);
        Task<double> GetCargoDetailsTotalVolume(int principalId);
        Task<double> GetCargoDetailsTotalSKU(int principalId);
    }
}
