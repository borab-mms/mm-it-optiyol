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
    /// HD_SalesDocument_Head Tablosu 
    /// </summary>
    [Table("HD_SalesDocument_Head", Schema = "dbo")]
    public class HDSalesDocumentHeadEntity : IEntity
    {
        [Key]
        public string StoreId { get; set; }
        [Key]
        public int SalesDocument { get; set; }
        public string ExternalDoc { get; set; }
        public string OrderId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
