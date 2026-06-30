using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MM.IT.Data.Entities.Interfaces;

namespace MM.Optiyol.Api.Data.Entities.External.MMLogistics.Optiyol
{
    [Table("BarcodeHead", Schema ="Optiyol")]
    public class OptiyolBarcodeHeadEntity : IEntity<int>
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public long Barcode { get; set; }

        [MaxLength(100)]
        public string ServiceType { get; set; }

        [MaxLength(100)]
        public string FromLocationId { get; set; }

        [MaxLength(100)]
        public string ToLocationId { get; set; }

        [MaxLength(100)]
        public string ToLocationName { get; set; }

        [MaxLength(100)]
        public string ToLocationAddress { get; set; }

        [MaxLength(100)]
        public string ToLocationCounty { get; set; }

        [MaxLength(100)]
        public string ToLocationCity { get; set; }

        public string ToLocationLatitude { get; set; }

        public string ToLocationLongitude { get; set; }

        [MaxLength(100)]
        public string ToLocationPhone { get; set; }

        [Column(TypeName = "decimal(10,4)")]
        public decimal Volume { get; set; } = 0;

        [Column(TypeName = "decimal(10,4)")]
        public decimal Weight { get; set; } = 0;

        [MaxLength(500)]
        public string? Note { get; set; }

        public string RequiresPaymentAtDoor { get; set; }

        [MaxLength(50)]
        public string PaymentMethod { get; set; }

        [Column(TypeName = "decimal(10,4)")]
        public decimal PaymentAmount { get; set; }

        public TimeSpan? EarliestPickupTime { get; set; }
        public TimeSpan? LatestPickupTime { get; set; }
        public TimeSpan? EarliestDeliveryTime { get; set; }
        public TimeSpan? LatestDeliveryTime { get; set; }
        public string? RequiredVehicleProperties { get; set; }
        public string? BarcodeProperties { get; set; }
        public DateTime BarcodePlannedDate { get; set; }
        public TimeSpan? BarcodePlannedTime { get; set; }
        public int Status { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }
    }
}
