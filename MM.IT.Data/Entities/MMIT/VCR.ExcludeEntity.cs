using MM.IT.Data.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace MM.IT.Data.Entities.MMIT;

/// <summary>
/// Excludes Tablosu 
/// </summary>
[Table("Excludes", Schema = "VCR")]
public class VCRExcludeEntity : BaseEntity<int>
{
    public int ArticleId { get; set; }
    public int PGID { get; set; }
}