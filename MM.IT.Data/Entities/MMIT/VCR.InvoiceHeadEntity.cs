using MM.IT.Data.Entities.Base;
using MM.IT.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMIT
{
    /// <summary>
    /// InvoiceHead Tablosu 
    /// </summary>
    [Table("InvoiceHead", Schema = "VCR")]
    public class VCRInvoiceHeadEntity : IEntity
    {
        [Key]
        public string InvoiceId { get; set; }
        public string? UUID { get; set; }
        public string? CustomerOrderNumber { get; set; }
        public string? OrderFulfillmentId { get; set; }
        public int? SystemNo { get; set; }
        public string? SAPCode { get; set; }
        public string? InvoiceType { get; set; }
        public string? Channel { get; set; }
        public int? CashRegisterNo { get; set; }
        public int? CashierNo { get; set; }
        public int? DrawerNo { get; set; }
        public string? IssueDate { get; set; }
        public string? IssueTime { get; set; }
        public int? LineCount { get; set; }
        public decimal? InvoiceTotalAmount { get; set; }
        public string? InvoiceAmountInWords { get; set; }
        public decimal? InvoiceTotalNetAmount { get; set; }
        public decimal? InvoiceTotalVatAmount { get; set; }
        public decimal? CashChange { get; set; }
        public string? LegalText { get; set; }
        public string? InvoiceTypeText { get; set; }
        public string? PrinterCode { get; set; }
        public string? Note { get; set; }
        public string? XMLContent { get; set; }
        public int StatusId { get; set; }
        public int? SendCount { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
