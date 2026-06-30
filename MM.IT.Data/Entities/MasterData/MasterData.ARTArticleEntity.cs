using MM.IT.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MasterData;

/// <summary>
/// Master Data DB -> Artikel Entity Nesnesi
/// </summary>
[Table("ART_Article", Schema = "dbo")]
public class MasterDataARTArticleEntity : IEntity
{
    /// <summary>
    /// Article Id Bilgisi
    /// </summary>
    public int ArticleId { get; set; }

    /// <summary>
    /// Article Adı Bilgisi
    /// </summary>
    public string ArticleName { get; set; }

    /// <summary>
    /// Ürün Grubu Id Bilgisi
    /// </summary>
    public short? PgId { get; set; }

    /// <summary>
    /// Marka Id Bilgisi
    /// </summary>
    public short? BrandId { get; set; }
}