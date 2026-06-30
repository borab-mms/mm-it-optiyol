using MM.IT.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MasterData;

/// <summary>
/// Master Data DB -> ProductGroup Entity Nesnesi
/// </summary>
[Table("ART_ProductGroup", Schema = "dbo")]
public class MasterDataARTProductGroupEntity : IEntity
{
    /// <summary>
    /// Ürün Grubu Id Bilgisi
    /// </summary>
    public short PgId { get; set; }

    /// <summary>
    /// PgName Bilgisi
    /// </summary>
    public string? PgName { get; set; }

    /// <summary>
    /// DpId Bilgisi
    /// </summary>
    public short DpId { get; set; }
}

