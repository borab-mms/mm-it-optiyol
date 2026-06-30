using MM.IT.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE
{
    /// <summary>
    /// StatusList Tablosu 
    /// </summary>
    [Table("StatusList", Schema = "Sterling")]
    public class SterlingStatusListEntity : IEntity
    {

        /// <summary>
        /// Id Bilgisi
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// StatusName Bilgisi
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        /// Oluşturulma tarihi bilgisini saklar.
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
