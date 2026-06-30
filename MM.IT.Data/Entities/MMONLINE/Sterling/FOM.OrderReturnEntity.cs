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
    /// OrderReturns Tablosu 
    /// </summary>
    [Table("OrderReturns", Schema = "FOM")]
    public class SterlingOrderReturnEntity : IEntity
    {

        [Key]
        public int Id { get; set; }
        public int CustomerOrderNumber { get; set; }
        public string State { get; set; }
        public string OrderReturnId { get; set; }
        public string RefundedInStore { get; set; }
        public string ExternalSalesDocumentId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }

    }

}
