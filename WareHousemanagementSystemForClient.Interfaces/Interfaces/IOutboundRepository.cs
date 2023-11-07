using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouseManagementSystemForClient.Model.DTOModels.ReportModels;
using WareHouseManagementSystemForClient.Model.URLSearchParameterModels;

namespace WareHousemanagementSystemForClient.Interfaces.Interfaces
{
    public interface IOutboundRepository
    {
        Task<ReportDTO> GetOutboundList(ReportsURLSearch urlSearch);
    }
}
