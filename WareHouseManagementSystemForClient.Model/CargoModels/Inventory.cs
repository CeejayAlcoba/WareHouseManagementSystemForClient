using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseManagementSystemForClient.Model.CargoModels
{
    public class Inventory
    {
        public int BookingId { get; set; }
        public DateTime? DateReleased { get; set; }
        public double? InboundQuantity { get; set; }
        public double? OutboundQuantity { get; set; }
        public double? Balance { get; set; }
        public double? Volume { get; set; }


        //public double? Quantity { get; set; }
        //public int Id { get; set; }
        //public int BookingId { get; set; }
        //public int PrincipalId { get; set; }
        //public DateTime? DateAccepted { get; set; }
        //public DateTime? DateReleased { get; set; }
        //public string? CargoName { get; set; }
        //public DateTime? ActualCheckinDate { get; set; }
        //public string? ItemCode { get; set; }
        //public string? ICRReferenceNo { get; set; }
        //public int? ProductCategoryId { get; set; }
        //public string? BinLocation { get; set; }
        //public double? Quantity { get; set; }
        //public double? Length { get; set; }
        //public double? Width { get; set; }
        //public double? HongkongSize { get; set; }
        //public double? SingaporeSize { get; set; }
        //public string? Uom { get; set; }
        //public DateTime? ExpirationDate { get; set; }
        //public string? ShipperName { get; set; }
        //public string? ShipperAddress { get; set; }
        //public string? ConsigneeContactNo { get; set; }
        //public string? ConsigneeName { get; set; }
        //public string? ConsigneeAddress { get; set; }  
        //public DateTime? DeliveryDueDate { get; set; }
        //public int? CargoType { get; set; }
        //public string? Address { get; set; }
        //public Guid? QRValue { get; set; }
        //public string? Description { get; set; }
        //public double? Volume { get; set; }
        //public double? TotalPCS { get; set; }
        //public double? PaletteCount { get; set; }
        //public string? ClientName { get; set; }
        //public DateTime? DeliveryDate { get; set; }
        //public string? DeliveryNote { get; set; }
        //public string? TructNum { get; set; }
        //public string? TruckType { get; set; }
        //public string? PONumber { get; set; }
        //public string? Dimension { get; set; }
    }
}
