using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE;

/// <summary>
/// HistoryOrderCustomers Tablosu 
/// </summary>
[Table("HistoryOrderCustomers", Schema = "Sterling")]
public class FOMHistoryOrderCustomersEntity : BaseEntity<int>
{

    /// <summary>
    /// CustomerUserId Bilgisi
    /// </summary>
    /// 
    public string? CustomerUserId { get; set; }

    /// <summary>
    /// CustomerOrderNumber Bilgisi
    /// </summary>
    public string CustomerOrderNumber { get; set; }

    /// <summary>
    /// FirstName Bilgisi
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// LastName Bilgisi
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Email Bilgisi
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// TaxID Bilgisi
    /// </summary>
    public string? TaxID { get; set; }

    /// <summary>
    /// LoyaltyID Bilgisi
    /// </summary>
    public string? LoyaltyID { get; set; }

    /// <summary>
    /// LoyaltyWin Bilgisi
    /// </summary>
    public decimal? LoyaltyWin { get; set; }

    /// <summary>
    /// LoyaltyWinType Bilgisi
    /// </summary>
    public string? LoyaltyWinType { get; set; }

    /// <summary>
    /// GroupKey tarihi bilgisini saklar.
    /// </summary>
    public string? GroupKey { get; set; }
}