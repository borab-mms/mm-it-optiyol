using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE;

/// <summary>
/// HistoryOrderItemDiscounts Tablosu 
/// </summary>
[Table("HistoryOrderItemDiscounts", Schema = "Sterling")]
public class FOMHistoryOrderItemDiscountEntity : BaseEntity<int>
{
    /// <summary>
    /// CustomerOrderNumber Bilgisi
    /// </summary>
    public string CustomerOrderNumber { get; set; }
    /// <summary>
    /// ProductId Bilgisi
    /// </summary>
    /// 
    public string ProductId { get; set; }

    /// <summary>
    /// LineItemId Bilgisi
    /// </summary>
    /// 
    public string LineItemId { get; set; }

    /// <summary>
    /// LineItemReference Bilgisi
    /// </summary>
    /// 
    public string LineItemReference { get; set; }

    /// <summary>
    /// LineItemStatusDescription Bilgisi
    /// </summary>
    /// 
    public string LineItemStatusDescription { get; set; }
    /// <summary>
    /// ItemDiscountType Bilgisi
    /// </summary>
    public string? ItemDiscountType { get; set; }

    /// <summary>
    /// ItemDiscountCode Bilgisi
    /// </summary>
    /// 
    public string? ItemDiscountCode { get; set; }

    /// <summary>
    /// ItemDiscountDescription Bilgisi
    /// </summary>
    public string? ItemDiscountDescription { get; set; }

    /// <summary>
    /// ItemDiscountValue Bilgisi
    /// </summary>
    public string? ItemDiscountValue { get; set; }

    /// <summary>
    /// ItemDiscountUnitValue Bilgisi
    /// </summary>
    public string? ItemDiscountUnitValue { get; set; }

    /// <summary>
    /// ItemDiscountReasonCode Bilgisi
    /// </summary>
    public string? ItemDiscountReasonCode { get; set; }

    /// <summary>
    /// ItemDiscountName Bilgisi
    /// </summary>
    public string? ItemDiscountName { get; set; }

    /// <summary>
    /// ItemDiscountReference Bilgisi
    /// </summary>
    public string? ItemDiscountReference { get; set; }

    /// <summary>
    /// ItemDiscountCostCenter Bilgisi
    /// </summary>
    public string? ItemDiscountCostCenter { get; set; }

    /// <summary>
    /// ItemDiscountClass Bilgisi
    /// </summary>
    public string? ItemDiscountClass { get; set; }

    /// <summary>
    /// GroupKey tarihi bilgisini saklar.
    /// </summary>
    public string? GroupKey { get; set; }
}