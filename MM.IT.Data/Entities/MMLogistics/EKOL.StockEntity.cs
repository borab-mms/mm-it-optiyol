using Microsoft.EntityFrameworkCore.Infrastructure;
using MM.IT.Data.Entities.Base;
using MM.IT.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMLogistics;

/// <summary>
/// EKOLStocks Tablosu 
/// </summary>
[Table("Stocks", Schema = "EKOL")]
public class EKOLStockEntity : BaseEntity<int>
{
    public string ArticleCode { get; set; }
    public string AsnNo { get; set; }
    public string BatchNr { get; set; }
    public DateTime? ExpireDate { get; set; }
    public decimal? EKOLStockId { get; set; }
    public string OrgCode { get; set; }
    public string PackagingGroup { get; set; }
    public decimal? QtyStock { get; set; }
    public decimal? QtyStockInPlanned { get; set; }
    public decimal? QtyStockOutPlanned { get; set; }
    public string SerialNr { get; set; }
    public string StockLocation { get; set; }
    public string StockType { get; set; }
    public DateTime? UpdatedDate { get; set; }

}
