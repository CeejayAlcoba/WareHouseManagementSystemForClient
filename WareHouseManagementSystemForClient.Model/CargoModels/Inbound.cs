using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseManagementSystemForClient.Model.CargoModels
{
    public class Inbound
    {
        public DateTime? DateAccepted { get; set; }
        public DateTime? ActualCheckinDate { get; set; }
        public int? ShipmentNo { get; set; }
        public string? ICRReferenceNo { get; set; }
        public string? Dr { get; set; }
        public int Id { get; set; }
        public string? SKU { get; set; }
        public string? Principal { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public string? BinLocation { get; set; }
        public double? Quantity { get; set; }
        public string? Size { get; set; }
        public string? Dimension { get; set; }
        public string? Uom { get; set; }
        public double? Volume { get; set; }
        public string? VM { get; set; }
        public double? TotalPcs { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string? Shipper { get; set; }
        public string? ShipperAddress { get; set; }
        public string? Consignee { get; set; }
        public string? ConsigneeAddress { get; set; }
        public DateTime? DeliveryDueDate { get; set; }
        public string? Address { get; set; }


        public int BookingId { get; set; }
        public DateTime? DateReleased { get; set; }
        public string? ItemCode { get; set; }
        public int? ProductCategoryId { get; set; }
        public int PrincipalId { get; set; }
        public double? Length { get; set; }
        public double? Width { get; set; }

        public double? HongkongSize { get; set; }
        public double? SingaporeSize { get; set; }
        public string? ConsigneeContactNo { get; set; }
        public string? ConsigneeName { get; set; }
        public int? CargoType { get; set; }
        public Guid? QRValue { get; set; }
        public double? PaletteCount { get; set; }
        public string? ClientName { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string? DeliveryNote { get; set; }
        public string? TructNum { get; set; }
        public string? TruckType { get; set; }
        public string? PONumber { get; set; }
    }
}
