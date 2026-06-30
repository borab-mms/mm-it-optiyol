using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE;

/// <summary>
/// OrderPayment Tablosu 
/// </summary>
[Table("OrderPayment", Schema = "Sterling")]
public class FOMOrderPaymentEntity : BaseEntity<int>
{
    /// <summary>
    /// CustomerOrderNumber Bilgisi
    /// </summary>
    public string CustomerOrderNumber { get; set; }
    /// <summary>
    /// PaymentId Bilgisi
    /// </summary>
    public string? PaymentId { get; set; }

    /// <summary>
    /// PaymentRef Bilgisi
    /// </summary>
    /// 
    public string? PaymentRef { get; set; }

    /// <summary>
    /// PaymentType Bilgisi
    /// </summary>
    public string? PaymentType { get; set; }

    /// <summary>
    /// PaymentValue Bilgisi
    /// </summary>
    public string? PaymentValue { get; set; }

    ///// <summary>
    ///// OrderLanguage Bilgisi
    ///// </summary>
    //public string? OrderLanguage { get; set; }

    /// <summary>
    /// PaymentStatus Bilgisi
    /// </summary>
    public string? PaymentStatus { get; set; }

    /// <summary>
    /// PaymentCurrency Bilgisi
    /// </summary>
    public string? PaymentCurrency { get; set; }

    /// <summary>
    /// Güncelleme tarihi bilgisini saklar.
    /// </summary>
    public DateTime? UpdatedDate { get; set; }

}
