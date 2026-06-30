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
    /// HD_Barcode_Location Tablosu 
    /// </summary>
    [Table("HD_Barcode_Location", Schema = "dbo")]
    public class HDBarcodeLocationEntity : IEntity
    {
        public string Barcode { get; set; }
        public string Status { get; set; }
        public DateTime LogDate { get; set; }
    }
}
