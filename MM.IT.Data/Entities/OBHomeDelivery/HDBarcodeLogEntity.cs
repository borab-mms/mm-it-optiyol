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
    /// HD_Barcode_Log Tablosu 
    /// </summary>
    [Table("HD_Barcode_Log", Schema = "dbo")]
    public class HDBarcodeLogEntity : IEntity
    {
        [Key]
        public Guid LogId { get; set; }
        public string Barcode { get; set; }
        public string Operation { get; set; }
        public DateTime LogDate { get; set; }
    }
}
