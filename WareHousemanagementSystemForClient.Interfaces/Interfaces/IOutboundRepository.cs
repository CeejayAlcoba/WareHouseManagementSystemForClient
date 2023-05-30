﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouseManagementSystemForClient.Model.CargoModels;

namespace WareHousemanagementSystemForClient.Interfaces.Interfaces
{
    public interface IOutboundRepository
    {
        public Task<(IEnumerable<Outbound>, int,double?,double?)> GetOutboundList(DateTime? AcceptedDateFrom, DateTime? AcceptedDateTo, string? searchRep, int? categoryId, int principalId, int? cargoType, int rowSkip, int rowTake);
        public Task<(IEnumerable<Inbound>, int)> GetAllOutboundByPrincipal(int principalId);
    }
}
