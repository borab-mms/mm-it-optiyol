using MM.IT.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE
{
    /// <summary>
    /// PaymentTypes Tablosu 
    /// </summary>
    [Table("PaymentTypes", Schema = "Sterling")]
    public class FOMPaymentTypeEntity : IEntity
    {
        /// <summary>
        /// PrimaryKey bilgisini saklar.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// PaymentType Bilgisi
        /// </summary>
        public string? PaymentType { get; set; }

        /// <summary>
        /// PaymentName Bilgisi
        /// </summary>
        public string? PaymentName { get; set; }

    }
}
