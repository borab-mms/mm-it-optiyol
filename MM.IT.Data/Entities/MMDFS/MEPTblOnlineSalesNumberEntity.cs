using MM.IT.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMDFS
{
    [Table("MEP_TblOnlineSalesNumber")]
    public class MEPTblOnlineSalesNumberEntity : IEntity
    {
        [Key]
        public int Id { get; set; }
        public int? CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateUser { get; set; }
        public int? UpdateDate { get; set; }
        public string? UUID { get; set; }
        public byte? WFState { get; set; }
        public string OnlineSalesNumber { get; set; }
    }
}
