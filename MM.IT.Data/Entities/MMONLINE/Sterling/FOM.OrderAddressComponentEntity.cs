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
    /// OrderAddressComponents Tablosu 
    /// </summary>
    [Table("OrderAddressComponents", Schema = "FOM")]
    public class SterlingOrderAddressComponentEntity : IEntity
    {
        [Key]
        public int Id { get; set; }
        //public string CustomerOrderNumber { get; set; }
        public string ComponentType { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }
    }
}
