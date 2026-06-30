using MM.IT.Data.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MM.Optiyol.Api.Data.Entities.External.MMLogistics.Optiyol
{
    [Table("OrderReturneds", Schema = "Optiyol")]
    public class OptiyolOrderReturnedEntity : BaseEntity<Guid>
    {
        [Key]
        public Guid Id { get; set; }

        public string TransactionId { get; set; }
        public string OrderId { get; set; }
        public int CompanyId { get; set; }
        public string Recipients { get; set; }
        public string Images { get; set; }
        public int VisitSequence { get; set; }
        public string CustomerName { get; set; }
        public string ServiceType { get; set; }
        public bool IsDropoff { get; set; }
        public bool IsPickup { get; set; }
        public string LocationId { get; set; }
        public string LocationAddress { get; set; }
        public string LocationLat { get; set; }
        public string LocationLon { get; set; }
        public bool IsLocationVerified { get; set; }
        public string VehicleCode { get; set; }
        public string Channel { get; set; }
        public string ReturnedLat { get; set; }
        public string ReturnedLon { get; set; }
        public bool IsReturned { get; set; }
        public DateTime ReturnedTime { get; set; }
        public string ReturnedLocation { get; set; }
        public string ContactPersonRelation { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPersonPhone { get; set; }
        public string Note { get; set; }
        public string Sign { get; set; }

        public DateTime CreatedDate { get; set; }
        //public virtual OptiyolNotificationEntity OptiyolNotification { get; set; }
    }

}
