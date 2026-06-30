using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.Common;

/// <summary>
/// ITraceableEntity'den türeyen Entity'lerin SaveChanges ve SaveChangesAsync öncesindeki ve sonrasındaki bilgilerini tutan model nesnesi.
/// Log'da bu bilgiyi Json olarak tutmak için kullanıldı. Detay bilgisi: GymKong.Data.UnitOfWork.AuditEntry
/// </summary>
public class AuditModel
{
    /// <summary>
    /// Constructor
    /// </summary>
    public AuditModel()
    {
        Id = Guid.NewGuid();
    }

    /// <summary>
    /// Id Bilgisi
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Tablo Adı Bilgisi
    /// </summary>
    public string TableName { get; set; }

    /// <summary>
    /// Durum Bilgisi -> Entity Added, Modified, Removed
    /// </summary>
    public string State { get; set; }

    /// <summary>
    /// Tarih Bilgisi
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Transaction Id Bilgisi
    /// </summary>
    public Guid TransactionId { get; set; }

    /// <summary>
    /// Field İsimleri 
    /// </summary>
    public Dictionary<string, object> KeyValues { get; set; }

    /// <summary>
    /// Eski Değerler
    /// </summary>
    public Dictionary<string, object> OldValues { get; set; }

    /// <summary>
    /// Yeni Değerler
    /// </summary>
    public Dictionary<string, object> NewValues { get; set; }

    /// <summary>
    /// ToString override metodu. Objeyi Json'a serialize eder ve döner.
    /// </summary>
    /// <returns>string: Objenin Json'a serialize edilmiş hali</returns>
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}
