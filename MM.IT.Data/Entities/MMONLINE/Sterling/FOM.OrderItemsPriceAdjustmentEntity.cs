using MM.IT.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE.Sterling
{

    /// <summary>
    /// OrderItemsPriceAdjustments Tablosu 
    /// </summary>
    [Table("OrderItemsPriceAdjustments", Schema = "FOM")]
    public class SterlingOrderItemsPriceAdjustmentEntity : IEntity
    {

        [Key]
        public int Id { get; set; }
        public int CustomerOrderNumber { get; set; }
        public string OrderItemId { get; set; }
        public string PriceAdjustmentsId { get; set; }
        public string PromotionType { get; set; }
        public string PriceAdjustmentsType { get; set; }
        public string Name { get; set; }
        public string CostCenter { get; set; }
        public string DiscountClass { get; set; }
        public string GrossAmount { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }

    }
}
