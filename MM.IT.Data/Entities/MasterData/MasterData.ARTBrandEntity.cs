using MM.IT.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MasterData;

/// <summary>
/// Master Data DB -> Artikel Marka Entity Nesnesi
/// </summary>
[Table("ART_Brand", Schema = "dbo")]
public class MasterDataARTBrandEntity : IEntity
{
    /// <summary>
    /// Marka Id Bilgisi
    /// </summary>
    public int BrandId { get; set; }

    /// <summary>
    /// Marka Adı Bilgisi
    /// </summary>
    public string BrandName { get; set; }

}