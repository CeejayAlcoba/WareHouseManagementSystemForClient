using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHousemanagementSystemForClient.Interfaces.Interfaces;
using WareHouseManagementSystemForClient.Model.DTOModels.BillingDetailModel;

namespace WareHouseManagementSystemForClient.Repositories.Repositories
{
    public class BillingDetailRepository : IBillingDetailRepositoy
    {
        private readonly IGenericRepository _genericRepository;
        public BillingDetailRepository(IGenericRepository genericRepository) {
        
        _genericRepository = genericRepository;
        }

        public async Task<BillingDetailModel> GetBillingDetailByPrincipalId(int principalId)
        {
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@PrincipalID",principalId,DbType.Int32);
            return await _genericRepository.GetFirstOrDefaultAsync<BillingDetailModel>("WMS_GetBillingDetailByPrincipal", parameter);
        }
    }
}
