using MM.IT.Data.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MM.Optiyol.Api.Data.Entities.External.MMLogistics.Optiyol
{
    [Table("OrderCompletedWithItem", Schema = "Optiyol")]
    public class OptiyolOrderCompletedWithItemEntity : BaseEntity<Guid>
    {
        [Key]
        public Guid Id { get; set; }

        public int CompanyId { get; set; }
        public string TransactionId { get; set; }
        public string OrderId { get; set; }
        public int VisitSequence { get; set; }
        public string CustomerName { get; set; }
        public string ServiceType { get; set; }
        public bool IsDropoff { get; set; }
        public bool IsPickup { get; set; }
        public string LocationId { get; set; }
        public string LocationAddress { get; set; }
        public double LocationLat { get; set; }
        public double LocationLon { get; set; }
        public bool IsLocationVerified { get; set; }
        public string VehicleCode { get; set; }
        public string Channel { get; set; }
        public string ClaimId { get; set; }
        public string ChannelExtraData { get; set; }
        public int OrderPk { get; set; }
        public string Recipients { get; set; }
        public string ArrivedLat { get; set; }
        public string ArrivedLon { get; set; }
        public bool IsComplete { get; set; }
        public string CompletedTime { get; set; }
        public string PlannedCompleteTime { get; set; }
        public string ContactPersonRelation { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPersonPhone { get; set; }
        public string CustomerNote { get; set; }
        public string PaymentMethod { get; set; }
        public string Note { get; set; }
        public string Sign { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual IList<OptiyolOrderCompletedWithItemsEntity> OptiyolOrderCompletedWithItems { get; set; }
        //public virtual OptiyolNotificationEntity OptiyolNotification { get; set; }
    }

}
