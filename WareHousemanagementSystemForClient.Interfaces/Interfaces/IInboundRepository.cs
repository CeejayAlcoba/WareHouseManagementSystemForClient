﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouseManagementSystemForClient.Model.CargoModels;

namespace WareHousemanagementSystemForClient.Interfaces.Interfaces
{
    public interface IInboundRepository
    {
        Task<(IEnumerable<Inbound>, int,double?,double?)> GetInboundList(DateTime? asOfDate, string? search, string? sku, int principalId, int? cargoType, int? rowSkip, int? rowTake);
        Task<(IEnumerable<Inbound>, int)> GetAllInboundByPrincipal(int principalId);
    }
}
