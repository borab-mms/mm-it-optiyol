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
    /// OrderItemsHolds Tablosu 
    /// </summary>
    [Table("OrderItemsHolds", Schema = "FOM")]
    public class SterlingOrderItemHoldsEntity : IEntity
    {

        [Key]
        public int Id { get; set; }
        public int CustomerOrderNumber { get; set; }
        public string OrderItemId { get; set; }
        public string Type { get; set; }
        public string Reason { get; set; }
        public string State { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }

    }
}
