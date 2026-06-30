using MM.IT.Data.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MM.Optiyol.Api.Data.Entities.External.MMLogistics.Optiyol
{
    [Table("Notifications", Schema = "Optiyol")]
    public class OptiyolNotificationEntity : BaseEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? HookId { get; set; }
        public string? TransactionId { get; set; }
        public int? StatusId { get; set; }
        public string? ServiceType { get; set; }

        public string? EventName { get; set; }

        public string? OrderId { get; set; }

        public string? RouteTrackerCode { get; set; }

        public DateTime CreatedDate { get; set; }

        //public virtual IList<OptiyolRoutePlannedEntity> OptiyolRoutePlanneds { get; set; }
        //public virtual IList<OptiyolRouteLoadListCompletedEntity> OptiyolRouteLoadListCompleteds { get; set; }
        //public virtual IList<OptiyolRouterStartedEntity> OptiyolRouterStarteds { get; set; }
        //public virtual IList<OptiyolOrderArrivedEntity> OptiyolOrderArriveds { get; set; }
        //public virtual IList<OptiyolOrderCompletedEntity> OptiyolOrderCompleteds { get; set; }
        //public virtual IList<OptiyolOrderCompletedWithItemEntity> OptiyolOrderCompletedWithItems { get; set; }
        //public virtual IList<OptiyolOrderCanceledEntity> OptiyolOrderCanceleds { get; set; }
        //public virtual IList<OptiyolOrderCanceledWithItemsEntity> OptiyolOrderCanceledWithItems { get; set; }
        //public virtual IList<OptiyolOrderUndoCanceledEntity> OptiyolOrderUndoCanceleds { get; set; }

    }
}
