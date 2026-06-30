using MM.IT.Data.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace MM.Optiyol.Api.Data.Entities.External.MMLogistics.Optiyol
{
    [Table("OrderCompletedWithItems", Schema = "Optiyol")]
    public class OptiyolOrderCompletedWithItemsEntity : BaseEntity<Guid>
    {
        public Guid Id { get; set; }

        public string TransactionId { get; set; }
        public string OrderItemId { get; set; }
        public string Barcode { get; set; }
        public string SkuId { get; set; }
        public string SkuName { get; set; }
        public string UomQuantity { get; set; }
        public int PlannedSkuQuantity { get; set; }
        public int ActualSkuQuantity { get; set; }
        public double ActualSkuPercentageQuantity { get; set; }
        public double PlannedPrice { get; set; }
        public double ActualPrice { get; set; }
        public string Status { get; set; }
        public string DriverNote { get; set; }
        public string CancelReason { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid? OrderCompletedWithItemId { get; set; }
        public virtual OptiyolOrderCompletedWithItemEntity OrderCompletedWithItem { get; set; }

        public virtual IList<OptiyolOrderCompletedWithItemLotNumbersEntity> OptiyolOrderCompletedWithItemLotNumbers { get; set; }
    }

}
