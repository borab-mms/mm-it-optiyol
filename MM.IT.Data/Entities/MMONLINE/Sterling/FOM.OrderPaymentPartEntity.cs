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
    /// OrderPaymentParts Tablosu 
    /// </summary>
    [Table("OrderPaymentParts", Schema = "FOM")]
    public class SterlingOrderPaymentPartEntity : IEntity
    {

        [Key]
        public int Id { get; set; }
        public int CustomerOrderNumber { get; set; }
        public Guid OrderPaymentId { get; set; }
        public string Method { get; set; }
        public string Amount { get; set; }
        public string Currency { get; set; }
        public string PaymentId { get; set; }
        public string MerchantId { get; set; }
        public string CardBrand { get; set; }
        public string GiftCardSetId { get; set; }
        public string GiftCardInSetId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }

    }
}
