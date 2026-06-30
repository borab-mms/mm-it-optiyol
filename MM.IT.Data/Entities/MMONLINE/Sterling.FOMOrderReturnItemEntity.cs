using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE;

/// <summary>
/// OrderReturnItems Tablosu 
/// </summary>
[Table("OrderReturnItems", Schema = "Sterling")]
public class FOMOrderReturnItemEntity : BaseEntity<int>
{
    /// <summary>
    /// ReturnOrderId Bilgisi
    /// </summary>
    public string? ReturnOrderId { get; set; }

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
    /// ReturnCreated Bilgisi
    /// </summary>
    /// 
    public DateTime? ReturnCreated { get; set; }

    /// <summary>
    /// ReturnReasonCode Bilgisi
    /// </summary>
    public string? ReturnReasonCode { get; set; }

    /// <summary>
    /// ReturnOutletId Bilgisi
    /// </summary>
    public int? ReturnOutletId { get; set; }

    /// <summary>
    /// CustomerReturnReason Bilgisi
    /// </summary>
    public string? CustomerReturnReason { get; set; }

    /// <summary>
    /// ReturnChannel Bilgisi
    /// </summary>
    public string? ReturnChannel { get; set; }

    /// <summary>
    /// ReturnQuantity Bilgisi
    /// </summary>
    public int? ReturnQuantity { get; set; }

    /// <summary>
    /// Address Bilgisi
    /// </summary>
    public int? Address { get; set; }

    /// <summary>
    /// Güncelleme tarihi bilgisini saklar.
    /// </summary>
    public DateTime? UpdatedDate { get; set; }

}