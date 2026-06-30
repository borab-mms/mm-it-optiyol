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
/// HistoryOrderItemRelations Tablosu 
/// </summary>
[Table("HistoryOrderItemRelations", Schema = "Sterling")]
public class FOMHistoryOrderItemRelationsEntity : BaseEntity<int>
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
    /// GroupKey tarihi bilgisini saklar.
    /// </summary>
    public string? GroupKey { get; set; }

}