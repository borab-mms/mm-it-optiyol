using MM.IT.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE;

/// <summary>
/// Sellers Tablosu 
/// </summary>
[Table("Sellers", Schema = "Sterling")]
public class FOMSellerEntity : IEntity
{
    /// <summary>
    /// SellerId Bilgisi
    /// </summary>
    public int SellerId { get; set; }

    /// <summary>
    /// FirstNumber Bilgisi
    /// </summary>
    /// 
    public int? FirstNumber { get; set; }

    /// <summary>
    /// SellerType Bilgisi
    /// </summary>
    /// 
    public string? SellerType { get; set; }

    /// <summary>
    /// SellerName Bilgisi
    /// </summary>
    /// 
    public string? SellerName { get; set; }

    /// <summary>
    /// Oluşturulma tarihi bilgisini saklar.
    /// </summary>
    public DateTime CreatedDate { get; set; }
}
