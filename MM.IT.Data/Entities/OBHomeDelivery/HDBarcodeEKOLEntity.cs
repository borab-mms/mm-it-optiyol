using MM.IT.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.OBHomeDelivery
{
    /// <summary>
    /// HD_Barcode_EKOL Tablosu 
    /// </summary>
    [Table("HD_Barcode_EKOL", Schema = "dbo")]
    public class HDBarcodeEKOLEntity : IEntity
    {
        public string Barcode { get; set; }
        public int SalesDocument { get; set; }
    }
}
