using MM.IT.Data.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MM.Optiyol.Api.Data.Entities.External.MMLogistics.Optiyol
{
    [Table("OrderArriveds", Schema = "Optiyol")]
    public class OptiyolOrderArrivedEntity : BaseEntity<Guid>
    {
        [Key]
        public Guid Id { get; set; }

        public string TransactionId { get; set; }
        public string OrderId { get; set; }
        public int VisitSequence { get; set; }
        public string CustomerName { get; set; }
        public string ServiceType { get; set; }
        public bool IsDropoff { get; set; }
        public bool IsPickup { get; set; }
        public bool IsArrived { get; set; }
        public string LocationId { get; set; }
        public string LocationAddress { get; set; }
        public string LocationLat { get; set; }
        public string LocationLon { get; set; }
        public DateTime ArrivedTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string ArrivedLat { get; set; }
        public string ArrivedLon { get; set; }
        public string VehicleCode { get; set; }
        public string Otp { get; set; }
        public string Channel { get; set; }
        public string Recipients { get; set; }
        public bool IsLocationVerified { get; set; }
        public string CompanyId { get; set; }

        public DateTime CreatedDate { get; set; }
        //public virtual OptiyolNotificationEntity OptiyolNotification { get; set; }
    }

}
