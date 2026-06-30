using MM.IT.Data.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MM.Optiyol.Api.Data.Entities.External.MMLogistics.Optiyol
{
    [Table("RouterStarteds", Schema = "Optiyol")]
    public class OptiyolRouterStartedEntity : BaseEntity<Guid>
    {
        [Key]
        public Guid Id { get; set; }

        public string TransactionId { get; set; }
        public int CompanyId { get; set; }
        public string RouteTrackerCode { get; set; }
        public string VehicleCode { get; set; }
        public DateTime StartTime { get; set; }
        public int NumberOfStops { get; set; }
        public int NumberOfOrders { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual IList<OptiyolRouterStartedOrdersEntity> OptiyolRouterStartedOrders { get; set; }
        //public virtual OptiyolNotificationEntity OptiyolNotification { get; set; }
    }

}
