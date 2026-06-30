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
    /// Channels Tablosu 
    /// </summary>
    [Table("Channels", Schema = "Api")]
    public class ApiChannelEntity : BaseEntity<int>
    {
        public string ChannelName { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? EmailAddress { get; set; }
        public bool IsActive { get; set; }
        public string? UpdatedBy { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
