using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMIT
{

    /// <summary>
    /// ApiAndChannels Tablosu 
    /// </summary>
    [Table("ApiAndChannels", Schema = "Api")]
    public class ApiAndChannelEntity : BaseEntity<int>
    {
        public int ChannelId { get; set; }
        public int ApiId { get; set; }
        public bool IsActive { get; set; }
        public string? UpdatedBy { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
