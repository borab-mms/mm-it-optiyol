using MM.IT.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;

namespace MM.IT.Common.Extensions;

/// <summary>
/// Convert işlemleri için kullanılan extension methodları 
/// </summary>
public static class SerializationExtensions
{
    /// <summary>
    /// Objeyi byteArray'a çevirir.
    /// </summary>
    /// <param name="obj">Obje</param>
    /// <returns>Byte Array</returns>
    public static byte[] ConvertToByteArray(this object obj)
    {
        if (obj == null)
            return null;

        var json = JsonSerializer.Serialize(obj);
        var byteArray = Encoding.UTF8.GetBytes(json);

        return byteArray;
    }

    /// <summary>
    /// Byte arrayı generic objeye çevirir.
    /// </summary>
    /// <typeparam name="T">Generic Nesne</typeparam>
    /// <param name="data">Byte Array</param>
    /// <returns>Generic Object</returns>
    public static T ContertTo<T>(this byte[] data)
    {
        if (data == null)
            return default(T);

        var json = Encoding.UTF8.GetString(data);
        var obj = JsonSerializer.Deserialize<T>(json);
        return obj;
    }

    /// <summary>
    /// JsonElement nesnesini objeye çevirir.
    /// </summary>
    /// <typeparam name="T">Generic Obje</typeparam>
    /// <param name="element">JsonElement</param>
    /// <returns>Generic Obje</returns>
    public static T ToObject<T>(this JsonElement element)
    {
        var json = element.GetRawText();
        return JsonSerializer.Deserialize<T>(json);
    }

    /// <summary>
    /// JsonDocument nesnesini objeye çevirir.
    /// </summary>
    /// <typeparam name="T">Generic Obje</typeparam>
    /// <param name="document">JsonDocument</param>
    /// <returns>Generic Obje</returns>
    public static T ToObject<T>(this JsonDocument document)
    {
        if (document == null)
        {
            return default(T);
        }

        var json = document.RootElement.GetRawText();
        return JsonSerializer.Deserialize<T>(json);
    }

    public static T ToObject<T>(this ExpandoObject expandoObject)
    {

        if (expandoObject == null)
        {
            return default(T);
        }

        var json = JsonSerializer.Serialize(expandoObject);
        return JsonSerializer.Deserialize<T>(json);
    }

    /// <summary>
    /// Objeyi Klonlar.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static T Clone<T>(this T source)
    {
        var serialized = JsonSerializer.Serialize(source);
        return JsonSerializer.Deserialize<T>(serialized);
    }

    /// <summary>
    /// Datatable verisini List'e Çevirir.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="table"></param>
    /// <returns></returns>
    public static IList<T> BindToList<T>(this DataTable table)
    {
        var data = new List<T>();
        foreach (DataRow row in table.Rows)
        {
            T item = row.BindToItem<T>();
            data.Add(item);
        }
        return data;
    }

    /// <summary>
    /// Datatable verisini Dynamic'e Çevirir.
    /// </summary>
    /// <param name="table"></param>
    /// <returns></returns>
    public static IList<dynamic> BindToDynamic(this DataTable table)
    {
        var data = new List<dynamic>();
        foreach (DataRow row in table.Rows)
        {
            dynamic item = new ExpandoObject();
            data.Add(item);

            var dictionary = (IDictionary<string, object>)item;
            foreach (DataColumn column in table.Columns)
            {
                dictionary[column.ColumnName] = row[column];
            }
        }
        return data;
    }

    /// <summary>
    /// DataRow verisini Objeye Çeveirir.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="row"></param>
    /// <returns></returns>
    public static T BindToItem<T>(this DataRow row)
    {
        Type type = typeof(T);
        PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

        var entity = Activator.CreateInstance<T>();

        foreach (DataColumn column in row.Table.Columns)
        {
            column.ColumnName = column.ColumnName?.Trim();
        }

        foreach (PropertyInfo property in properties)
        {
            var name = property.GetCustomAttributes(typeof(LocalizedNameAttribute), true).Cast<LocalizedNameAttribute>().FirstOrDefault()?.Description ?? property.Name;

            if (row.Table.Columns.Contains(name))
            {
                if (row[name] != DBNull.Value)
                {
                    Type valueType = property.PropertyType;
                    var value = row[name].ChangeType(valueType);
                    property.SetValue(entity, value);
                }
            }
        }

        foreach (FieldInfo field in fields)
        {
            var name = field.GetCustomAttributes(typeof(LocalizedNameAttribute), true).Cast<LocalizedNameAttribute>().FirstOrDefault()?.Description ?? field.Name;

            if (row.Table.Columns.Contains(name))
            {
                if (row[name] != DBNull.Value)
                {
                    Type valueType = field.FieldType;
                    var value = row[name].ChangeType(valueType);
                    field.SetValue(entity, value);
                }
            }
        }

        return entity;
    }

    /// <summary>
    /// Obje Türünü Değiştirir.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="conversion"></param>
    /// <returns></returns>
    public static object ChangeType(this object value, Type conversion)
    {
        if (value == DBNull.Value || string.IsNullOrWhiteSpace(value?.ToString()))
        {
            return null;
        }

        var t = conversion;

        if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
        {
            if (value == null)
            {
                return null;
            }

            t = Nullable.GetUnderlyingType(t);
        }

        return Convert.ChangeType(value, t);
    }


    public static string ToXMLString<T>(this T source, XmlWriterSettings settings = null, XmlSerializerNamespaces namespaces = null)
    {
        try
        {
            if (source == null) return string.Empty;

            var xmlserializer = new XmlSerializer(typeof(T));
            var stringWriter = new StringWriter();
            using var writer = XmlWriter.Create(stringWriter, settings);
            xmlserializer.Serialize(writer, source, namespaces);
            return stringWriter.ToString();
        }
        catch
        {
            return string.Empty;
        }
    }
}