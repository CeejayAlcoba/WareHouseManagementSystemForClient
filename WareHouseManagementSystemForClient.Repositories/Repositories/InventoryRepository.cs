using AutoMapper;
using Dapper;
using System.Data;
using WareHousemanagementSystemForClient.Interfaces.Interfaces;
using WareHouseManagementSystemForClient.Model.DTOModels.ReportModels;
using WareHouseManagementSystemForClient.Model.URLSearchParameterModels;

namespace WareHouseManagementSystemForClient.Repositories.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;
        public InventoryRepository(IGenericRepository genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }
        public async Task<ReportDTO> GetInventoryList(ReportsURLSearch urlSearch)
        {
           
            var procedureName = "WMS_InventoryList";
            //var parameters = new DynamicParameters();
            //parameters.Add("CargoId", urlSearch.CargoId, DbType.Int64, ParameterDirection.Input);
            //parameters.Add("PrincipalId", urlSearch.PrincipalId, DbType.Int64, ParameterDirection.Input);
            //parameters.Add("DateFrom", urlSearch.DateFrom, DbType.DateTime, ParameterDirection.Input);
            //parameters.Add("DateTo", urlSearch.DateTo, DbType.DateTime, ParameterDirection.Input);
            //parameters.Add("CargoName", urlSearch.Sku, DbType.String, ParameterDirection.Input);
            //parameters.Add("CargoType", urlSearch.CargoType, DbType.Int64, ParameterDirection.Input);
            //parameters.Add("RowTake", urlSearch.RowTake, DbType.Int64, ParameterDirection.Input);
            //parameters.Add("PageNumber", urlSearch.PageNumber, DbType.Int64, ParameterDirection.Input);

            var inventories = await _genericRepository.GetAllAsync<Report>(procedureName, new
            {
                CargoId = urlSearch.CargoId,
                PrincipalId = urlSearch.PrincipalId,
                DateFrom = urlSearch.DateFrom,
                DateTo = urlSearch.DateTo,
                CargoType = urlSearch.CargoType,
                RowTake = urlSearch.RowTake,
                PageNumber = urlSearch.PageNumber,
                CargoName = urlSearch.Sku ?? urlSearch.CargoName,
                Description = urlSearch.Description,
                Quantity = urlSearch.Quantity,
                UoM = urlSearch.UOM,
                BatchNo = urlSearch.BatchNo,
                ExpDate = urlSearch.ExpDate,
                BinLocation = urlSearch.BINLocation

            });

            var reportData = _mapper.Map<IEnumerable<Report>, ReportDTO>(inventories.ToList());


            return reportData;
        }
    }
}
