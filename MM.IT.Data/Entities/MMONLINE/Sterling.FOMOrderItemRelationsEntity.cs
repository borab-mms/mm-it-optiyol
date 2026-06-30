using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE;

/// <summary>
/// OrderItemRelations Tablosu 
/// </summary>
[Table("OrderItemRelations", Schema = "Sterling")]
public class FOMOrderItemRelationsEntity : BaseEntity<int>
{
    /// <summary>
    /// LineItemIdParent Bilgisi
    /// </summary>
    public string? LineItemIdParent { get; set; }

    /// <summary>
    /// CustomerOrderNumber Bilgisi
    /// </summary>
    public string CustomerOrderNumber { get; set; }

    /// <summary>
    /// LineItemIdChild Bilgisi
    /// </summary>
    /// 
    public string? LineItemIdChild { get; set; }

    /// <summary>
    /// LineItemIdRelationshipType Bilgisi
    /// </summary>
    public string? LineItemIdRelationshipType { get; set; }

    /// <summary>
    /// Güncelleme tarihi bilgisini saklar.
    /// </summary>
    public DateTime? UpdatedDate { get; set; }

}