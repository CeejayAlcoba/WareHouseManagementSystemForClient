namespace WareHouseManagementSystemForClient.Model.DTOModels.ReportModels
{
    public class Report
    {
        public string? ArticleNoDescription { get; set; }
        public double? Qty { get; set; }
        public string? UOM { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? PalletAddress { get; set; }
        public double? TotalQuantity { get; set; }
        public double? TotalSKU { get; set; }
    }
}
