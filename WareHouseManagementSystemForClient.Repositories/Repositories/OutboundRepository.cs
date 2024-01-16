using AutoMapper;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHousemanagementSystemForClient.Interfaces.Interfaces;
using WareHouseManagementSystemForClient.DbContext.Context;
using WareHouseManagementSystemForClient.Model.DTOModels.ReportModels;
using WareHouseManagementSystemForClient.Model.URLSearchParameterModels;

namespace WareHouseManagementSystemForClient.Repositories.Repositories
{
    public class OutboundRepository : IOutboundRepository
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;

        public OutboundRepository(IGenericRepository genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<ReportDTO> GetOutboundList(ReportsURLSearch urlSearch)
        {

            var procedureName = "WMS_GetOutboundList";
            var parameters = new DynamicParameters();
            parameters.Add("PrincipalId", urlSearch.PrincipalId, DbType.Int64, ParameterDirection.Input);
            parameters.Add("DateFrom", urlSearch.DateFrom, DbType.DateTime, ParameterDirection.Input);
            parameters.Add("DateTo", urlSearch.DateTo, DbType.DateTime, ParameterDirection.Input);
            parameters.Add("Sku", urlSearch.Sku, DbType.String, ParameterDirection.Input);
            parameters.Add("Search", urlSearch.Search, DbType.String, ParameterDirection.Input);
            parameters.Add("CargoType", urlSearch.CargoType, DbType.Int64, ParameterDirection.Input);
            parameters.Add("RowTake", urlSearch.RowTake, DbType.String, ParameterDirection.Input);
            parameters.Add("PageNumber", urlSearch.PageNumber, DbType.Int64, ParameterDirection.Input);

            var outbounds = await _genericRepository.GetAllAsync<Report>(procedureName, parameters);

            return _mapper.Map<ReportDTO>(outbounds.ToList());

        }
    }
}

