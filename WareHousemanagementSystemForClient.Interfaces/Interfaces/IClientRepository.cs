﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouseManagementSystemForClient.Model.ClientModels;
using WareHouseManagementSystemForClient.Model.SecurityModels;

namespace WareHousemanagementSystemForClient.Interfaces.Interfaces
{
    public interface IClientRepository
    {
        Task<Client> GetClientByUsername(string username);
    }
}
