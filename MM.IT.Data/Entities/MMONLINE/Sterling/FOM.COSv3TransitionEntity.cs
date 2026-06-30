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
    /// COSv3Transition Tablosu 
    /// </summary>
    [Table("COSv3Transition", Schema = "FOM")]
    public class SterlingCOSv3TransitionEntity : IEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime? Created_after { get; set; }
        public DateTime? Created_before { get; set; }
        public DateTime? TransactionDate { get; set; }
        public int? OrderQuantity { get; set; }
        public int? StatusId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
