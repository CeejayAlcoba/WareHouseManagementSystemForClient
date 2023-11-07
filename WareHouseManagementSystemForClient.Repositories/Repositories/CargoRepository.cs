using AutoMapper;
using Dapper;
using System.Data;
using WareHousemanagementSystemForClient.Interfaces.Interfaces;

namespace WareHouseManagementSystemForClient.Repositories.Repositories
{
    public class CargoRepository : ICargoRepository
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;
        public CargoRepository(IGenericRepository genericRepository,IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }
        //public async Task<IEnumerable<Report>> GetCargoDetails(ParamRequestForCargo paramRequest)
        //{
           
        //    var procedureName = "CLIENT_GetCargoDetailsByPrincipal";
        //    var parameters = new DynamicParameters();
        //    parameters.Add("PrincipalId", paramRequest.PrincipalId, DbType.Int64, ParameterDirection.Input);
        //    parameters.Add("Search", paramRequest.Search, DbType.String, ParameterDirection.Input);
        //    parameters.Add("Sku", paramRequest.Sku, DbType.String, ParameterDirection.Input);

        //    var cargos = await _genericRepository.GetAllAsync<Report>(procedureName, parameters);



        //    var test= _mapper.Map<IEnumerable<ReportItem>>(cargos);
        //    return cargos;
        //}

        //public async Task<IEnumerable<string>> GetSKUDropdownByPrincipalId(int principalId)
        //{
        //    var procedureName = "CLIENT_GetSKUNamesByPrincipalId";
        //    var parameters = new DynamicParameters();
        //    parameters.Add("PrincipalId", principalId, DbType.Int64, ParameterDirection.Input);
           
        //    var skus = await _genericRepository.GetAllAsync<string>(procedureName, parameters);

        //    return skus;
        //}
    }
}
