using MM.IT.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.OBHomeDelivery
{

    /// <summary>
    /// HD_SalesDocument_Item_Current Tablosu 
    /// </summary>
    [Table("HD_SalesDocument_Item_Current", Schema = "dbo")]
    public class HDSalesDocumentItemCurrentEntity : IEntity
    {
        [Key]
        public string StoreId { get; set; }
        [Key]
        public int SalesDocument { get; set; }
        [Key]
        public int ItemId { get; set; }
        public string Barcode { get; set; }
    }
}
