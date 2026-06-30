using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE;

/// <summary>
/// OrderItemWarranty Tablosu 
/// </summary>
[Table("OrderItemWarranty", Schema = "Sterling")]
public class FOMOrderItemWarrantyEntity : BaseEntity<int>
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
    /// WarrantyCertificateNumber Bilgisi
    /// </summary>
    public string? WarrantyCertificateNumber { get; set; }

    /// <summary>
    /// WarrantyItemSerialNumber Bilgisi
    /// </summary>
    /// 
    public string? WarrantyItemSerialNumber { get; set; }

    /// <summary>
    /// WarrantyContractPeriod Bilgisi
    /// </summary>
    public string? WarrantyContractPeriod { get; set; }

    /// <summary>
    /// WarrantyNumber Bilgisi
    /// </summary>
    public string? WarrantyNumber { get; set; }

    /// <summary>
    /// Güncelleme tarihi bilgisini saklar.
    /// </summary>
    public DateTime? UpdatedDate { get; set; }

}
