namespace WareHouseManagementSystemForClient.Model.DTOModels.ReportModels
{
    public class Report
    {
        public string? CargoName { get; set; }
        public string? Description { get; set; }
        public double? Quantity { get; set; }
        public string? UOM { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? ExpDate { get; set; }
        public string? BINLocation { get; set; }
        public double? TotalQuantity { get; set; }
        public double? TotalSKU { get; set; }
    }
}
