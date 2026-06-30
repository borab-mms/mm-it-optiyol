using MM.IT.Data.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace MM.Optiyol.Api.Data.Entities.External.MMLogistics.Optiyol
{
    [Table("RouterStartedOrders", Schema = "Optiyol")]
    public class OptiyolRouterStartedOrdersEntity : BaseEntity<Guid>
    {
        public Guid Id { get; set; }

        public string TransactionId { get; set; }
        public string OrderId { get; set; }
        public int Sequence { get; set; }
        public int VisitSequence { get; set; }
        public string ServiceType { get; set; }
        public string DeliveryTimeLower { get; set; }
        public string DeliveryTimeUpper { get; set; }
        public bool IsLoaded { get; set; }
        public string TrackUrl { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid? RouterStartedId { get; set; }
        public virtual OptiyolRouterStartedEntity RouterStarted { get; set; }
    }

}
