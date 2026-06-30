using MM.IT.Data.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace MM.Optiyol.Api.Data.Entities.External.MMLogistics.Optiyol
{

    [Table("RouteFinishedOrders", Schema = "Optiyol")]
    public class OptiyolRouterFinishedOrdersEntity : BaseEntity<Guid>
    {
        public Guid Id { get; set; }
        public string TransactionId { get; set; }
        public string OrderId { get; set; }
        public Guid? RouteFinishedId { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual OptiyolRouteFinishedEntity RouteFinished { get; set; }
    }
}
