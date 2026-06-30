using MM.IT.Data.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MM.Optiyol.Api.Data.Entities.External.MMLogistics.Optiyol
{
    [Table("RouteFinisheds", Schema = "Optiyol")]
    public class OptiyolRouteFinishedEntity : BaseEntity<Guid>
    {
        [Key]
        public Guid Id { get; set; }

        public string TransactionId { get; set; }
        public string RouteTrackerCode { get; set; }
        public string VehicleCode { get; set; }
        public DateTime FinishTime { get; set; }
        public int NumberOfStops { get; set; }
        public int NumberOfOrders { get; set; }
        public int NumberOfCompletedOrders { get; set; }
        public int NumberOfNotCompletedOrders { get; set; }
        public int NumberOfCanceledOrders { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public int CompanyId { get; set; }

        public DateTime CreatedDate { get; set; }
        public virtual IList<OptiyolRouterFinishedOrdersEntity> OptiyolRouterFinishedOrders { get; set; }
        //public virtual OptiyolNotificationEntity OptiyolNotification { get; set; }
    }

}
