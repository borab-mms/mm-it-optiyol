using MM.IT.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE
{

    /// <summary>
    /// MissingDataOrder Tablosu 
    /// </summary>
    [Table("MissingDataOrders", Schema = "Sterling")]
    public class FOMMissingDataOrderEntity : IEntity
    {

        /// <summary>
        /// CustomerOrderNumber Bilgisi
        /// </summary>
        public int CustomerOrderNumber { get; set; }

        /// <summary>
        /// IsActive Bilgisi
        /// </summary>
        public bool? IsActive { get; set; }
        public string Source { get; set; }

        /// <summary>
        /// Oluşturulma tarihi bilgisini saklar.
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
