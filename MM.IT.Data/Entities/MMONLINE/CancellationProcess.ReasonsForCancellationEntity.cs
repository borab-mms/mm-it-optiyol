using MM.IT.Data.Entities.MMONLINE.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE
{
    /// <summary>
    /// ReasonsForCancellation Tablosu 
    /// </summary>
    [Table("ReasonsForCancellation", Schema = "CancellationProcess")]
    public class CancellationProcessReasonsForCancellationEntity : ISterlingEntity<int>
    {

        /// <summary>
        /// ReasonName Bilgisi
        /// </summary>
        public string? ReasonName { get; set; }

        /// <summary>
        /// IsActive Bilgisi
        /// </summary>
        public bool? IsActive { get; set; }
    }
}
