using MM.IT.Data.Entities.Base;
using MM.IT.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE.Sterling
{

    /// <summary>
    /// RawDatas Tablosu 
    /// </summary>
    [Table("RawDatas", Schema = "FOM")]
    public class SterlingRawDataEntity : BaseEntity<int>
    {
        /// <summary>
        /// Şipariş Numarası Bilgisi
        /// </summary>
        public int CustomerOrderNumber { get; set; }

        /// <summary>
        /// ResultCode Bilgisi
        /// </summary>
        /// 
        public DateTime? OrderDate { get; set; }

        /// <summary>
        /// StartProcessingDate Bilgisi
        /// </summary>
        /// 
        public DateTime? StartProcessingDate { get; set; }

        /// <summary>
        /// RawData Json Bilgisi
        /// </summary>
        public string? RawData { get; set; }

        /// <summary>
        /// IsParsed Bilgisi
        /// </summary>
        public bool? IsParsed { get; set; }

        /// <summary>
        /// UpdatedDate Bilgisi
        /// </summary>
        public DateTime? UpdatedDate { get; set; }
    }
}

