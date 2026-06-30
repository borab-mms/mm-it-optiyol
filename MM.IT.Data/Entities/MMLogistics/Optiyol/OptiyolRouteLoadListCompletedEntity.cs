using MM.IT.Data.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MM.Optiyol.Api.Data.Entities.External.MMLogistics.Optiyol
{
    [Table("RouteLoadListCompleteds", Schema = "Optiyol")]
    public class OptiyolRouteLoadListCompletedEntity : BaseEntity<Guid>
    {
        [Key]
        public Guid Id { get; set; }

        public string TransactionId { get; set; }
        public string RouteTrackerCode { get; set; }
        public string VehicleCode { get; set; }
        public DateTime LoadCompletionTime { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual IList<OptiyolRouteLoadListCompletedItemsEntity> OptiyolRouteLoadListCompletedItems { get; set; }
        //public virtual OptiyolNotificationEntity OptiyolNotification { get; set; }
    }

}
