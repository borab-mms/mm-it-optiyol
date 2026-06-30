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
    /// OrderReturnItems Tablosu 
    /// </summary>
    [Table("OrderReturnItems", Schema = "FOM")]
    public class SterlingOrderReturnItemEntity : IEntity
    {

        [Key]
        public int Id { get; set; }
        public int CustomerOrderNumber { get; set; }
        public string OrderReturnId { get; set; }
        public string InsuredAmount { get; set; }
        public int? ContractPeriod { get; set; }
        public int? GoodWillPeriod { get; set; }
        public string GeneralTermsAndConditions { get; set; }
        public string CertificateNumber { get; set; }
        public bool? External { get; set; }
        public string OrderItemId { get; set; }
        public string Ean { get; set; }
        public string LogisticClassRef { get; set; }
        public string ManufacturerName { get; set; }
        public string HandlingType { get; set; }
        public string Type { get; set; }
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public string VatSing { get; set; }
        public string VatRate { get; set; }
        public string KindOfProduct { get; set; }
        public string Quantity { get; set; }
        public string RefundAmountUnitRefundAmount { get; set; }
        public string RefundAmountItemRefundAmount { get; set; }
        public string ReturnReason { get; set; }
        public string CustomerReturnReason { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }

    }
}
