using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE;

/// <summary>
/// OrderCharges Tablosu 
/// </summary>
[Table("OrderCharges", Schema = "Sterling")]
public class FOMOrderChargesEntity : BaseEntity<int>
{
    /// <summary>
    /// OrderChargeType Bilgisi
    /// </summary>
    public string? OrderChargeType { get; set; }

    /// <summary>
    /// CustomerOrderNumber Bilgisi
    /// </summary>
    public string CustomerOrderNumber { get; set; }

    /// <summary>
    /// OrderChargeDescription Bilgisi
    /// </summary>
    /// 
    public string? OrderChargeDescription { get; set; }

    /// <summary>
    /// OrderChargeValue Bilgisi
    /// </summary>
    public string? OrderChargeValue { get; set; }

    /// <summary>
    /// OrderChargeName Bilgisi
    /// </summary>
    public string? OrderChargeName { get; set; }

    /// <summary>
    /// OrderChargeVatSign Bilgisi
    /// </summary>
    public string? OrderChargeVatSign { get; set; }

    /// <summary>
    /// OrderChargeVatPercentage Bilgisi
    /// </summary>
    public string? OrderChargeVatPercentage { get; set; }

    /// <summary>
    /// Güncelleme tarihi bilgisini saklar.
    /// </summary>
    public DateTime? UpdatedDate { get; set; }
}