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
    /// MasterDataStatusList Tablosu 
    /// </summary>
    [Table("MasterDataStatusList", Schema = "FOM")]
    public class SterlingMasterDataStatusListEntity : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string? RequestedFulfillmentType { get; set; }
        public string? RequestedFulfillmentLevelOfService { get; set; }
        public string? RequestedFulfillmentPickupLocationType { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }
    }
}
