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
    /// HD_Barcode_Head Tablosu 
    /// </summary>
    [Table("HD_Barcode_Head", Schema = "dbo")]
    public class HDBarcodeHeadEntity : IEntity
    {
        [Key]
        public string Barcode { get; set; }
        public string StoreId { get; set; }
        public int DocumentId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string DeliveryTime { get; set; }
        public DateTime? PrintedOn { get; set; }
        public string Status { get; set; }
    }
}
