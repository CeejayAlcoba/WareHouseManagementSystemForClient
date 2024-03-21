using AutoMapper;
using Dapper;
using System.Data;
using WareHousemanagementSystemForClient.Interfaces.Interfaces;
using WareHouseManagementSystemForClient.Model.DTOModels.ReportModels;
using WareHouseManagementSystemForClient.Model.URLSearchParameterModels;

namespace WareHouseManagementSystemForClient.Repositories.Repositories
{
    public class InboundRepository : IInboundRepository
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;

        public InboundRepository(IGenericRepository genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }
        public async Task<ReportDTO> GetInboundList(ReportsURLSearch urlSearch)
        {

            var inbounds = await _genericRepository.GetAllAsync<Report>("WMS_GetInboundList",  new {
                CargoId = urlSearch.CargoId,
                PrincipalId = urlSearch.PrincipalId,
                DateFrom = urlSearch.DateFrom,
                DateTo = urlSearch.DateTo,
                CargoType = urlSearch.CargoType,
                RowTake = urlSearch.RowTake,
                PageNumber = urlSearch.PageNumber,
                SKU = urlSearch.CargoName,
                Description = urlSearch.Description,
                Quantity = urlSearch.Quantity,
                UOM = urlSearch.UOM,            
                ExpDate = urlSearch.ExpDate,
                BINLocation = urlSearch.BINLocation

            });

            var reportData = _mapper.Map<IEnumerable<Report>, ReportDTO>(inbounds.ToList());

            return reportData;

        }
    }
}
