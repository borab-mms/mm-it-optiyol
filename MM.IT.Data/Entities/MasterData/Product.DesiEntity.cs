using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MasterData;

/// <summary>
/// Desi Tablosu 
/// </summary>
[Table("Desi", Schema = "Product")]
public class DesiEntity : BaseEntity<int>
{
    public string SOURCE { get; set; } 
    public string ORG_CODE { get; set; }
    public string ARTICLE_CODE { get; set; }
    public string UOM { get; set; }
    public int? QUANTİTY { get; set; }
    public decimal? WIDTH { get; set; }
    public decimal? HEIGHT { get; set; }
    public decimal? LENGTH { get; set; }
    public decimal? WEIGHT { get; set; }
    public string BARCODE { get; set; }
    public decimal? NET_WEIGHT { get; set; }
    public string WEIGHT_UNIT { get; set; }
    public decimal? QUANTITY_PER_UNIT { get; set; }
    public string LENGTH_UNIT { get; set; }
    public decimal? DESI { get; set; }
    public string PACKING_TYPE { get; set; }
    public string VOLUME_UNIT { get; set; }

}

