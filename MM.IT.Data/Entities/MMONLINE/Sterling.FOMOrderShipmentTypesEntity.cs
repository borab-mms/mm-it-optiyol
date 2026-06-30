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
    /// OrderShipmentTypes Tablosu 
    /// </summary>
    [Table("OrderShipmentTypes", Schema = "Sterling")]
    public class FOMOrderShipmentTypesEntity : IEntity
    {
        /// <summary>
        /// Id Bilgisi
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// StatusName Bilgisi
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        /// StatusName Bilgisi
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// CreatedDate Bilgisi
        /// </summary>
        public DateTime CreatedDate { get; set; }

    }
}
