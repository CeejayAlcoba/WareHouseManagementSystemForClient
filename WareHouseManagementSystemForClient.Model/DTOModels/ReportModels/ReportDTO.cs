

namespace WareHouseManagementSystemForClient.Model.DTOModels.ReportModels
{
    public class ReportDTO
    {
        public IEnumerable<ReportItem> Items { get; set; } = new List<ReportItem>();
        public double? TotalQuantity { get; set; } = 0;
        public double? TotalSKU { get; set; } = 0;
    }


}
