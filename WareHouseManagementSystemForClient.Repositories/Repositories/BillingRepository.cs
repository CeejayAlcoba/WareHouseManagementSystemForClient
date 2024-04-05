using AutoMapper;
using Dapper;
using Microsoft.Identity.Client.Extensions.Msal;
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
        public BillingRepository(IGenericRepository genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }
        public async Task<double> GetVatbyPrincipal(int? principalId)
        {
            string procedureName = "WMS_InventoryList";
            var parameters = new DynamicParameters();
            parameters.Add("PrincipalId", principalId, DbType.Int64, ParameterDirection.Input);
            parameters.Add("RowTake", 1, DbType.Int64, ParameterDirection.Input);
            parameters.Add("PageNumber", 1, DbType.Int64, ParameterDirection.Input);

            var inventories = await _genericRepository.GetAllAsync<Report>(procedureName, parameters);

            if (inventories == null) return 0;

            return (double)inventories.FirstOrDefault().Vat;
        }
        public async Task<HandlingInBillDTO> GetHandlingIn(BillingURLSearch billing)
        {
            if (billing.DateFrom == null || billing.DateTo == null) return new HandlingInBillDTO();
            string procedureName = "WMS_GetHandlingIn";
            var parameters = new DynamicParameters();
            parameters.Add("DateFrom", billing.DateFrom, DbType.String, ParameterDirection.Input);
            parameters.Add("DateTo", billing.DateTo, DbType.String, ParameterDirection.Input);
            parameters.Add("CargoType", billing.CargoType, DbType.String, ParameterDirection.Input);
            parameters.Add("PrincipalId", billing.PrincipalId, DbType.String, ParameterDirection.Input);
            parameters.Add("CategoryId", billing.CategoryId, DbType.String, ParameterDirection.Input);

            var result = await _genericRepository.GetAllAsync<HandlingInSqlColumns>(procedureName, parameters);
            var mapped = _mapper.Map<HandlingInBillDTO>(result);

            return mapped;
        }
        public async Task<HandlingOutBillDTO> GetHandlingOut(BillingURLSearch billing)
        {
            if (billing.DateFrom == null || billing.DateTo == null) return new HandlingOutBillDTO();
            string procedureName = "WMS_GetHandlingOut";
            var parameters = new DynamicParameters();
            parameters.Add("DateFrom", billing.DateFrom, DbType.String, ParameterDirection.Input);
            parameters.Add("DateTo", billing.DateTo, DbType.String, ParameterDirection.Input);
            parameters.Add("CargoType", billing.CargoType, DbType.String, ParameterDirection.Input);
            parameters.Add("PrincipalId", billing.PrincipalId, DbType.String, ParameterDirection.Input);
            parameters.Add("CategoryId", billing.CategoryId, DbType.String, ParameterDirection.Input);

            var result = await _genericRepository.GetAllAsync<HandlingOutSqlColumns>(procedureName, parameters);
            var mapped = _mapper.Map<HandlingOutBillDTO>(result);

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
            parameters.Add("@IsAllowZero", false, DbType.Boolean);

            var beginningBalance = await _genericRepository.GetFirstOrDefaultAsync<BeginningBalance>(procedureName, parameters);
            if (beginningBalance == null)
            {
                var billingDetail = await GetBillingDetailByPrincipal((int)billing.PrincipalId);
                beginningBalance = new BeginningBalance
                {
                    TotalQuantity = 0,
                    TotalVolume = 0,
                    StorageBill = billingDetail.Storage,
                    BillingDate = endDate
                };
            }
            else
            {
                beginningBalance.BillingDate = endDate;
            }
           

            return beginningBalance;
        }

        //public async Task<List<StorageBill>> GetStorageBill(BillingURLSearch billing, BillingDTO handlingIn, BillingDTO handlingOut)
        //{
        //    if (billing.DateFrom == null || billing.DateTo == null) return new List<StorageBill>();

        //    var beginningReport = await BeginningBalance(billing);
        //    if (beginningReport == null) return new List<StorageBill>();

        //    if (handlingIn == null)
        //    {
        //        handlingIn = await GetHandlingIn(billing);
        //    }
        //    if (handlingOut == null)
        //    {
        //        handlingOut = await GetHandlingOut(billing);
        //    }


        //    List<StorageBill> combinedReports = new List<StorageBill>();
        //    var beginningReportMapped = new StorageBill()    //--MAPPING AND ADD THE BEGINNING BALANCE--//
        //    {
        //        BillingDate = beginningReport.BillingDate,
        //        Quantity = beginningReport.TotalQuantity,
        //        Volume = beginningReport.TotalVolume,
        //    };
        //    combinedReports.Add(beginningReportMapped);

        //    var handlingInGrouped = HandlingInToGroup(handlingIn.BillingItems);

        //    var handlingOutGrouped = HandlingOutToGroup(handlingOut.BillingItems);
        //    ReportToCombined(combinedReports, handlingInGrouped); //--COMBINED REPORTS--//       
        //    ReportToCombined(combinedReports, handlingOutGrouped); //--COMBINED REPORTS--//

        //    combinedReports = combinedReports.OrderBy(c => c.BillingDate).ToList();

        //    double? initialQuantity = beginningReport.TotalQuantity;
        //    double? initialVolume = beginningReport.TotalVolume;

        //    List<StorageBill> storageReports = new List<StorageBill>();

        //    for (int a = 0; a < combinedReports.Count(); a++)  //--CALCULATE THE SYNC QUANTITY AND VOLUME--//
        //    {
        //        var cutOffDate = a < combinedReports.Count() - 1 ? combinedReports[a + 1]?.BillingDate : billing.DateTo;
        //        int NoOfDays = (cutOffDate.Value.Date - combinedReports[a].BillingDate.Value.Date).Days;
        //        var item = combinedReports[a];

        //        if (item.HIQuantity != null && item.HOQuantity == null) //---THIS IS HANDLING IN ---//
        //        {
        //            initialQuantity = initialQuantity + item.Quantity;
        //            initialVolume = initialVolume + item.Volume;
        //        }
        //        else if (item.HIQuantity == null && item.HOQuantity != null) //---THIS IS HANDLING OUT & VOID BEGINNING BALANCE---//
        //        {
        //            initialQuantity = initialQuantity - item.Quantity;
        //            initialVolume = initialVolume - item.Volume;
        //        }
        //        var totalCharge = beginningReport.StorageBill * initialVolume * NoOfDays;

        //        var reportMapped = new StorageBill() //---MAPPING--//
        //        {
        //            BillingDate = item.BillingDate,
        //            CutOff = cutOffDate,
        //            Quantity = initialQuantity,
        //            Volume = initialVolume,
        //            NODays = NoOfDays,
        //            HIQuantity = item.HIQuantity,
        //            HOQuantity = item.HOQuantity,
        //            HIVolume = item.HIVolume,
        //            HOVolume = item.HOVolume,
        //            StorageCharge = Math.Round((double)totalCharge, 2)
        //        };
        //        storageReports.Add(reportMapped);
        //    };

        //    return storageReports;
        //}
        public async Task<StorageReportModel>  GetStorageBillReport(BillingURLSearch billing, HandlingInBillDTO handlingIn, HandlingOutBillDTO handlingOut)
        {
            if(billing.DateFrom == null|| billing.DateTo == null) return new StorageReportModel();
            StorageReportModel storageReport = new StorageReportModel();

            string procedureName = "WMS_GetStorage";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@StorageDateFrom", billing.DateFrom, DbType.DateTime);
            parameters.Add("@StorageDateTo", billing.DateTo, DbType.DateTime);
            parameters.Add("@StorageCargoType", billing.CargoType, DbType.Int64);
            parameters.Add("@StoragePrincipalId", billing.PrincipalId, DbType.Int64);
            parameters.Add("@StorageCategoryId", billing.CategoryId, DbType.Int64);

            var multiple = await _genericRepository.QueryMultipleList<StorageItem, StorageBillingDetail, object, object, object>(procedureName, parameters);
            if (multiple.Item2.Count() == 0) return storageReport;

            storageReport.StorageBill = multiple.Item2.FirstOrDefault().StorageBill;
            storageReport.Vat = multiple.Item2.FirstOrDefault().Vat;
            storageReport.StorageBillType = multiple.Item2.FirstOrDefault().StorageBillType;
            storageReport.UOM = multiple.Item2.FirstOrDefault().UOM;

            if (multiple.Item1.Count() == 0) return storageReport;

            StorageBillingDetail billingDetail = multiple.Item2.FirstOrDefault();
            StorageItem initStorageItem = multiple.Item1.FirstOrDefault();

            double currentUomValue = isContainBeginning(multiple.Item1.ToList()) ? initStorageItem.UomValue : 0;
            double currentQuantity = isContainBeginning(multiple.Item1.ToList()) ? initStorageItem.Quantity : 0;

            for (int a = 0; a < multiple.Item1.Count(); a++)
            {
                var item = multiple.Item1.ToList()[a];
                var nextBillingDate = a + 1 < multiple.Item1.Count() ?
                                        (multiple.Item1.ToList()[a + 1].BillingDate) : billing.DateTo;
                var noOfDays = (nextBillingDate.Value.Date - item.BillingDate.Value.Date).Days;


                var storageItem = new StorageItem()
                {
                    BillingDate = item.BillingDate,
                    CutOff = nextBillingDate,
                    UomValue = item.UomValue,
                    Quantity = item.Quantity,
                    ActionType = item.ActionType,
                    NODays = noOfDays

                };
                var actionType = storageItem.ActionType;
                currentUomValue = GetComputedCurrent(currentUomValue, item.UomValue, actionType);
                currentQuantity = GetComputedCurrent(currentQuantity, item.Quantity, actionType);
                storageItem.CurrentQuantity = currentQuantity;
                storageItem.CurrentUomValue = currentUomValue;
                storageItem.StorageCharge = currentUomValue * billingDetail.StorageBill * noOfDays;

                storageReport.StorageItemList.Add(storageItem);
            }

            storageReport.TotalCharge = storageReport.StorageItemList.Select(c => c.StorageCharge).Sum();
            storageReport.TotalUomValue = storageReport.StorageItemList.Select(c => c.UomValue).Sum();
            return storageReport;
        }
        private double GetComputedCurrent(double currentValue, double value, string actionType)
        {
            actionType = actionType.ToUpper();
            if (actionType == "OUT") return currentValue - value;
            if (actionType == "IN") return currentValue + value;
            else return currentValue;
        }
        private bool isContainBeginning(List<StorageItem> items)
        {
            return items.Select(c => c.ActionType).Contains("BEGINNING");
        }
        public async Task<BillingDetails> GetBillingDetailByPrincipal(int principalId)
        {
            string procedureName = "WMS_GetBillingDetailByPrincipal";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@PrincipalId", principalId, DbType.Int64);
            return await _genericRepository.GetFirstOrDefaultAsync<BillingDetails>(procedureName, parameters);
        }
        // IEnumerable<StorageBill> HandlingInToGroup(IEnumerable<IBillingItem> billingItems)
        //{
        //    return billingItems
        //                    .GroupBy(c => c.BillingDate)
        //                    .Select(group => new StorageBill
        //                    {
        //                        BillingDate = group.Key,
        //                        Quantity = group.Sum(item => item.Quantity),
        //                        Volume = group.Sum(item => item.Volume),
        //                        HIQuantity = group.Sum(item => item.Quantity),
        //                        HIVolume = group.Sum(item => item.Volume)
        //                    }).ToList();
        //}
        // IEnumerable<StorageBill> HandlingOutToGroup(IEnumerable<IBillingItem> billingItems)
        //{
        //    return billingItems
        //                     .GroupBy(c => c.BillingDate)
        //                     .Select(group => new StorageBill
        //                     {
        //                         BillingDate = group.Key,
        //                         Quantity = group.Sum(item => item.Quantity),
        //                         Volume = group.Sum(item => item.Volume),
        //                         HOQuantity = group.Sum(item => item.Quantity),
        //                         HOVolume = group.Sum(item => item.Volume)
        //                     }).ToList();
        //}
         void ReportToCombined(List<StorageBill> combinedReports, IEnumerable<StorageBill> billingItems)
        {
            foreach (var item in billingItems)
            {
                var mapped = new StorageBill()
                {
                    BillingDate = item.BillingDate,
                    Quantity = item.Quantity,
                    Volume = item.Volume,
                    HOVolume = item.HOVolume,
                    HOQuantity = item.HOQuantity,
                    HIVolume = item.HIVolume,
                    HIQuantity = item.HIQuantity,
                };

                combinedReports.Add(mapped);
            }
        }

        
    }

}
