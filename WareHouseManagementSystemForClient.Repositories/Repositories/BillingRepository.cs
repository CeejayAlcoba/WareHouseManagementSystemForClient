using AutoMapper;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHousemanagementSystemForClient.Interfaces.Interfaces;
using WareHouseManagementSystemForClient.Model.DTOModels.BillingModel;
using WareHouseManagementSystemForClient.Model.DTOModels.ClientModels;
using WareHouseManagementSystemForClient.Model.DTOModels.ReportModels;
using WareHouseManagementSystemForClient.Model.URLSearchParameterModels;

namespace WareHouseManagementSystemForClient.Repositories.Repositories
{
    public class BillingRepository : IBillingRepository
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;
        private readonly IInventoryRepository _inventoryRepository;
        public BillingRepository(IGenericRepository genericRepository, IMapper mapper, IInventoryRepository inventoryRepository)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
            _inventoryRepository = inventoryRepository;
        }

        public async Task<BillingDTO> GetHandlingIn(BillingURLSearch billing)
        {

            string procedureName = "WMS_GetHandlingIn";

             var parameters = new DynamicParameters();
            parameters.Add("DateFrom", billing.DateFrom, DbType.String, ParameterDirection.Input);
            parameters.Add("DateTo", billing.DateTo, DbType.String, ParameterDirection.Input);
            parameters.Add("CargoType", billing.CargoType, DbType.String, ParameterDirection.Input);
            parameters.Add("PrincipalId", billing.PrincipalId, DbType.String, ParameterDirection.Input);
            parameters.Add("CategoryId", billing.CategoryId, DbType.String, ParameterDirection.Input);

            var result = await _genericRepository.GetAllAsync<BillingSQL>(procedureName, parameters);
            var mapped = _mapper.Map<BillingDTO>(result);

            return mapped;
        }
        public async Task<BillingDTO> GetHandlingOut(BillingURLSearch billing)
        {

            string procedureName = "WMS_GetHandlingOut";

            var parameters = new DynamicParameters();
            parameters.Add("DateFrom", billing.DateFrom, DbType.String, ParameterDirection.Input);
            parameters.Add("DateTo", billing.DateTo, DbType.String, ParameterDirection.Input);
            parameters.Add("CargoType", billing.CargoType, DbType.String, ParameterDirection.Input);
            parameters.Add("PrincipalId", billing.PrincipalId, DbType.String, ParameterDirection.Input);
            parameters.Add("CategoryId", billing.CategoryId, DbType.String, ParameterDirection.Input);

            var result = await _genericRepository.GetAllAsync<BillingSQL>(procedureName, parameters);
            var mapped = _mapper.Map<BillingDTO>(result);

            return mapped;
        }

        public async Task<BeginningBalance> BeginningBalance(BillingURLSearch billing)
        {
            var procedureName = "WMS_InventoryList";
            DateTime? endDate = billing.DateFrom.Value.AddDays(-1);
            DateTime? beginningDate = new DateTime(1753, 1, 1);
            var parameters = new DynamicParameters();
            parameters.Add("PrincipalId", billing.PrincipalId, DbType.Int64, ParameterDirection.Input);
            parameters.Add("DateFrom", beginningDate, DbType.DateTime, ParameterDirection.Input);
            parameters.Add("DateTo", endDate, DbType.DateTime, ParameterDirection.Input);
            parameters.Add("CargoType", billing.CargoType, DbType.Int64, ParameterDirection.Input);
            parameters.Add("RowTake", 1, DbType.Int64, ParameterDirection.Input);
            parameters.Add("PageNumber", 1, DbType.Int64, ParameterDirection.Input);

            var beginningBalance = await _genericRepository.GetFirstOrDefaultAsync<BeginningBalance>(procedureName, parameters);
            if (beginningBalance != null) beginningBalance.BillingDate = endDate; 

            return beginningBalance;
        }

        public async Task<List<StorageBill>> GetStorageBill(BillingURLSearch billing, BillingDTO handlingIn, BillingDTO handlingOut)
        {
            
            var beginningReport = await BeginningBalance(billing);
            if (beginningReport == null) return new List<StorageBill>();

            if (handlingIn == null)
            {
                handlingIn = await GetHandlingIn(billing);
            }
            if (handlingOut == null)
            {
                handlingOut = await GetHandlingOut(billing);
            }

                //--COMBINED REPORTS--//
            List<StorageBill> combinedReports = new List<StorageBill>();

            //--MAPPING AND ADD THE HANDLING IN--//
            foreach (var item in handlingIn.BillingItems) 
            {
                var mapped = new StorageBill()
                {
                    BillingDate = item.BillingDate,
                    Quantity = item.Quantity,
                    Volume = item.Volume,
                    HIVolume = item.Volume,
                    HIQuantity = item.Quantity,
                };
                combinedReports.Add(mapped);
            }

            //--MAPPING AND ADD THE HANDLING OUT--//
            foreach (var item in handlingOut.BillingItems) 
            {
                var mapped = new StorageBill()
                {
                    BillingDate = item.BillingDate,
                    Quantity = item.Quantity,
                    Volume = item.Volume,
                    HOVolume = item.Volume,
                    HOQuantity = item.Quantity,
                };
                combinedReports.Add(mapped);
            }

            //--MAPPING AND ADD THE BEGINNING BALANCE--//
            var beginningReportMapped = new StorageBill() 
            {
                BillingDate = beginningReport.BillingDate,
                //CutOff = beginningReport.CutOff,
                Quantity = beginningReport.TotalQuantity,
                Volume = beginningReport.TotalVolume,
            };

            combinedReports.Add(beginningReportMapped);
            combinedReports = combinedReports.OrderBy(c => c.BillingDate).ToList();

            double? initialQuantity = beginningReport.TotalQuantity;
            double? initialVolume = beginningReport.TotalVolume;

            List<StorageBill> storageReports = new List<StorageBill>();

            //--CALCULATE THE SYNC QUANTITY AND VOLUME--//
            for (int a = 0; a < combinedReports.Count(); a++)
            {
                var cutOffDate = a < combinedReports.Count() - 1 ? combinedReports[a + 1]?.BillingDate : billing.DateTo;
                int NoOfDays = (cutOffDate.Value.Date - combinedReports[a].BillingDate.Value.Date).Days;
                var item = combinedReports[a];

                if (item.HIQuantity != null && item.HOQuantity == null) //---THIS IS HANDLING IN ---//
                {
                    initialQuantity = initialQuantity + item.Quantity;
                    initialVolume = initialVolume + item.Volume;
                }
                else if (item.HIQuantity == null && item.HOQuantity != null) //---THIS IS HANDLING OUT & VOID BEGINNING BALANCE---//
                {
                    initialQuantity = initialQuantity - item.Quantity;
                    initialVolume = initialVolume - item.Volume;
                }

                var reportMapped = new StorageBill() //---MAPPING--//
                {
                    BillingDate = item.BillingDate,
                    CutOff = cutOffDate,
                    Quantity = initialQuantity,
                    Volume = initialVolume,
                    NODays = NoOfDays,
                    HIQuantity = item.HIQuantity,
                    HOQuantity = item.HOQuantity,
                    HIVolume = item.HIVolume,
                    HOVolume = item.HOVolume,
                    StorageCharge = beginningReport.StorageBill * initialVolume * NoOfDays
                };
                storageReports.Add(reportMapped);
            };

            return storageReports;
        }
        
    }
}
