using MM.IT.Data.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace MM.Optiyol.Api.Data.Entities.External.MMLogistics.Optiyol
{
    [Table("OrderCompletedWithItemLotNumbers", Schema = "Optiyol")]
    public class OptiyolOrderCompletedWithItemLotNumbersEntity : BaseEntity<Guid>
    {
        public Guid Id { get; set; }

        public string TransactionId { get; set; }
        public string LotNumber { get; set; }

        public int Quantity { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid? OrderCompletedWithItemsId { get; set; }
        public virtual OptiyolOrderCompletedWithItemsEntity OrderCompletedWithItems { get; set; }
    }

}
