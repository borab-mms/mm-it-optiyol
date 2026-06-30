using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE
{
    /// <summary>
    /// DC_Items Tablosu 
    /// </summary>
    [Table("DC_Items", Schema = "dbo")]
    public class DCItemEntity : BaseEntity<int>
    {
        public int CustomerOrderNumber { get; set; }
        public string LineItemId { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int? DCProductMapsId { get; set; }
        public string BarcodeNo { get; set; }
        public decimal ItemPrice { get; set; }
        public decimal ItemPriceNet { get; set; }
        public int? TotalQuantity { get; set; }
        public int Status { get; set; }

    }
}
