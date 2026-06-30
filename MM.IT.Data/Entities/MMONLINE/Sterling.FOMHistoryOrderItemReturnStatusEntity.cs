using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE;


/// <summary>
/// HistoryOrderItemReturnStatus Tablosu 
/// </summary>
[Table("HistoryOrderItemReturnStatus", Schema = "Sterling")]
public class FOMHistoryOrderItemReturnStatusEntity : BaseEntity<int>
{
    /// <summary>
    /// ReturnOrderId Bilgisi
    /// </summary>
    public string? ReturnOrderId { get; set; }
    /// <summary>
    /// ReturnStatus Bilgisi
    /// </summary>
    public string? ReturnStatus { get; set; }

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
    /// StatusQuantity Bilgisi
    /// </summary>
    /// 
    public decimal? StatusQuantity { get; set; }

    /// <summary>
    /// GroupKey tarihi bilgisini saklar.
    /// </summary>
    public string? GroupKey { get; set; }

}