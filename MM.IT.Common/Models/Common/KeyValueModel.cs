using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.Common;

/// <summary>
/// Key ve Generic Value ihtiyacında kullanılacak model.
/// </summary>
/// <typeparam name="TValue">TValue: Generic type</typeparam>
[Serializable]
public class KeyValueModel<TValue>
{
    /// <summary>
    /// Key Bilgisi
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    /// Value bilgisi, herhangi bir değer olabilir.
    /// </summary>
    public TValue Value { get; set; }
}

/// <summary>
/// Key ve Generic Value ihtiyacında kullanılacak model.
/// </summary>
/// <typeparam name="TValue">TValue: Generic type</typeparam>
[Serializable]
public class KeyValueModelWithSeperator<TValue>
{
    /// <summary>
    /// Key Bilgisi
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    /// Ayırıcı Bilgi
    /// </summary>
    public string Seperator { get; set; }

    /// <summary>
    /// Value bilgisi, herhangi bir değer olabilir.
    /// </summary>
    public TValue Value { get; set; }
}

/// <summary>
/// Key ve Generic Value ihtiyacında kullanılacak model.
/// </summary>
/// <typeparam name="TKey">TKey: Generic type</typeparam>
/// <typeparam name="TValue">TValue: Generic type</typeparam>
[Serializable]
public class KeyValueModel<TKey, TValue>
{
    /// <summary>
    /// Key Bilgisi
    /// </summary>
    public TKey Key { get; set; }

    /// <summary>
    /// Value bilgisi, herhangi bir değer olabilir.
    /// </summary>
    public TValue Value { get; set; }
}

/// <summary>
/// Key ve Generic Value ihtiyacında kullanılacak model.
/// </summary>
/// <typeparam name="TKey">TKey: Generic type</typeparam>
/// <typeparam name="TValue">TValue: Generic type</typeparam>
[Serializable]
public class KeyValueModelWithSeperator<TKey, TValue>
{
    /// <summary>
    /// Key Bilgisi
    /// </summary>
    public TKey Key { get; set; }

    /// <summary>
    /// Ayırıcı Bilgi
    /// </summary>
    public string Seperator { get; set; }

    /// <summary>
    /// Value bilgisi, herhangi bir değer olabilir.
    /// </summary>
    public TValue Value { get; set; }
}

/// <summary>
/// Generic Key,Value ve Seperator ihtiyacında kullanılacak model.
/// </summary>
/// <typeparam name="TKey">TKey: Generic type</typeparam>
/// <typeparam name="TValue">TValue: Generic type</typeparam>
/// <typeparam name="TSeperator">TSeperator: Generic type</typeparam>
[Serializable]
public class KeyValueModelWithSeperator<TKey, TValue, TSeperator>
{
    /// <summary>
    /// Key Bilgisi
    /// </summary>
    public TKey Key { get; set; }

    /// <summary>
    /// Ayırıcı Bilgi
    /// </summary>
    public TSeperator Seperator { get; set; }

    /// <summary>
    /// Value bilgisi, herhangi bir değer olabilir.
    /// </summary>
    public TValue Value { get; set; }
}
