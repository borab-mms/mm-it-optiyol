using MM.IT.Data.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MM.Optiyol.Api.Data.Entities.External.MMLogistics.Optiyol
{
    [Table("RoutePlanneds", Schema = "Optiyol")]
    public class OptiyolRoutePlannedEntity : BaseEntity<Guid>
    {
        [Key]
        public Guid Id { get; set; }

        public int CompanyId { get; set; }
        public string TransactionId { get; set; }
        public string CompanyName { get; set; }
        public string RouteTrackerCode { get; set; }
        public string VehicleCode { get; set; }
        public string UserName { get; set; }
        public string DriverFirstName { get; set; }
        public string DriverLastName { get; set; }
        public string DriverEmail { get; set; }
        public DateTime CreatedDate { get; set; }
        //public virtual OptiyolNotificationEntity OptiyolNotification { get; set; }

    }
}
