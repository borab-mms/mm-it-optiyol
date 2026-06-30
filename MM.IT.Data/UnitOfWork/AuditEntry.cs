using Microsoft.EntityFrameworkCore.ChangeTracking;
using MM.IT.Common.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.UnitOfWork;

/// <summary>
/// ITraceableEntity'den türeyen Entity'lerin SaveChanges ve SaveChangesAsync öncesindeki ve sonrasındaki bilgilerini tutan nesne.
/// </summary>
public class AuditEntry
{
    /// <summary>
    /// Yapılan değişiklik durumu: https://docs.microsoft.com/en-us/dotnet/api/system.data.entitystate
    /// </summary>
    public string State { get; set; }

    /// <summary>
    /// Entity Adı
    /// </summary>
    public string TableName { get; set; }

    /// <summary>
    /// Aktif Transaction Id Bilgisi
    /// </summary>
    public Guid TransactionId { get; set; }

    /// <summary>
    /// Entity Property Listesi
    /// </summary>
    public Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();

    /// <summary>
    /// Entity Property Dataları -> SaveChanges ve SaveChangesAsync öncesi
    /// </summary>
    public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();

    /// <summary>
    /// Entity Property Dataları -> SaveChanges ve SaveChangesAsync sonrası
    /// </summary>
    public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();

    /// <summary>
    /// Entity Geçici Property Tanımlamaları
    /// </summary>
    public List<PropertyEntry> TemporaryProperties { get; } = new List<PropertyEntry>();

    /// <summary>
    /// Entity Geçici Property tanımlamalarına sahip mi bilgisini tutar.
    /// </summary>
    public bool HasTemporaryProperties => TemporaryProperties.Any();

    /// <summary>
    /// AuditEntry bilgisini AuditModel bilgisine çevirir ve döner.
    /// </summary>
    /// <returns>AuditModel</returns>
    public AuditModel ToAuditModel()
    {
        return new AuditModel()
        {
            TableName = TableName,
            State = State,
            TransactionId = TransactionId,
            Date = DateTime.UtcNow,
            KeyValues = KeyValues,
            OldValues = OldValues,
            NewValues = NewValues,
        };

    }
}
