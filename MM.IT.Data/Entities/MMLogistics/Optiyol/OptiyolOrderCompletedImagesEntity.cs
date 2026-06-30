using MM.IT.Data.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MM.Optiyol.Api.Data.Entities.External.MMLogistics.Optiyol
{

    [Table("OrderCompletedImages", Schema = "Optiyol")]
    public class OptiyolOrderCompletedImagesEntity : BaseEntity<Guid>
    {
        [Key]
        public Guid Id { get; set; }
        public string TransactionId { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? OrderCompletedId { get; set; }
        public virtual OptiyolOrderCompletedEntity OrderCompleted { get; set; }
    }
}
