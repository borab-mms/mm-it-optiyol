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
    /// OrderComponentAttributes Tablosu 
    /// </summary>
    [Table("OrderComponentAttributes", Schema = "FOM")]
    public class SterlingOrderComponentAttributeEntity : IEntity
    {
        [Key]
        public int Id { get; set; }
        public Guid OrderAddressId { get; set; }
        public int? OrderAddressComponentId { get; set; }
        public string OrderAddressComponentType { get; set; }
        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }
    }
}
