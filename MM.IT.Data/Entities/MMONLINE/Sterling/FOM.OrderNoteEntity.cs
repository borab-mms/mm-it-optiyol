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
    /// OrderNotes Tablosu 
    /// </summary>
    [Table("OrderNotes", Schema = "FOM")]
    public class SterlingOrderNoteEntity : IEntity
    {

        [Key]
        public int Id { get; set; }
        public int CustomerOrderNumber { get; set; }
        public string OrderNoteId { get; set; }
        public string RefType { get; set; }
        public string RefValue { get; set; }
        public string Text { get; set; }
        public string Reason { get; set; }
        public bool? Internal { get; set; }
        public bool? HighPriority { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedAt { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }

    }
}
