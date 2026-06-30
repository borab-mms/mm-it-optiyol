using MM.IT.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE.Sterling
{
    /// <summary>
    /// OrderTotalDetails Tablosu 
    /// </summary>
    [Table("OrderTotalDetails", Schema = "FOM")]
    public class SterlingOrderTotalDetailEntity : IEntity
    {

        [Key]
        public Guid GuidId { get; set; }
        public int Id { get; set; }
        public int CustomerOrderNumber { get; set; }
        public string TotalType { get; set; }
        public string Type { get; set; }
        public string GrossAmount { get; set; }
        public string NetAmount { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }

    }
}
