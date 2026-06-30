using MM.IT.Data.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace MM.Optiyol.Api.Data.Entities.External.MMLogistics.Optiyol
{
    [Table("RouteLoadListCompletedItems", Schema = "Optiyol")]
    public class OptiyolRouteLoadListCompletedItemsEntity : BaseEntity<Guid>
    {
        public Guid Id { get; set; }
        public string TransactionId { get; set; }
        public string OrderId { get; set; }
        public string Channel { get; set; }
        public string OrderItemId { get; set; }
        public string SkuId { get; set; }
        public string SkuName { get; set; }
        public int SkuLoadQuantity { get; set; }
        public string LotNumber { get; set; }
        public string UomQuantity { get; set; }
        public DateTime CreatedDate { get; set; }

        public Guid? RouteLoadListCompletedId { get; set; }
        public virtual OptiyolRouteLoadListCompletedEntity RouteLoadListCompleted { get; set; }
    }

}
