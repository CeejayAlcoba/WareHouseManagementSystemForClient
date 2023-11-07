using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHousemanagementSystemForClient.Interfaces.Interfaces;
using WareHouseManagementSystemForClient.Model.DTOModels.DropdownModels;

namespace WareHouseManagementSystemForClient.Repositories.Repositories
{
    public class DropdownRepository : IDropdownRepository
    { 
        private readonly IGenericRepository _genericRepository;
        public DropdownRepository(IGenericRepository genericRepository) { 
        _genericRepository = genericRepository;
        }
        public async Task<IEnumerable<DropdownDTO>> GetSKUDropdown(int principalId)
        {
            var procedureName = "CLIENT_GetSKUDropdown";
            var parameters = new DynamicParameters();
            parameters.Add("PrincipalId", principalId, DbType.Int64, ParameterDirection.Input);

            var skus = await _genericRepository.GetAllAsync<DropdownDTO>(procedureName, parameters);

            return skus;
        }
     
    }
}
