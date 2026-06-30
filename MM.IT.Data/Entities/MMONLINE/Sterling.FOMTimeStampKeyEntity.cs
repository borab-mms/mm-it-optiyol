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
    /// TimeStampKey Tablosu 
    /// </summary>
    [Table("TimeStampKey", Schema = "Sterling")]
    public class FOMTimeStampKeyEntity : IEntity<int>
    {
        /// <summary>
        /// Id Bilgisi
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// TimeKey Bilgisi
        /// </summary>
        public int TimeKey { get; set; }

        /// <summary>
        /// CountTransaction Bilgisi
        /// </summary>
        /// 
        public int? CountTransaction { get; set; }
    }
}
