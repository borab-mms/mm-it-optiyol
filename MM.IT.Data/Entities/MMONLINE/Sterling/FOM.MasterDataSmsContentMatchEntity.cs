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
    /// MasterDataSmsContentMatch Tablosu 
    /// </summary>
    [Table("MasterDataSmsContentMatch", Schema = "FOM")]
    public class SterlingMasterDataSmsContentMatchEntity : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string? FulfillmentType { get; set; }
        public string? LevelOfService { get; set; }
        public bool? ShipFromStore { get; set; }
        public string? State { get; set; }
        public string? OrderState { get; set; }
        public string? AggregatedState { get; set; }
        public int? SmsContentId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }
    }
}
