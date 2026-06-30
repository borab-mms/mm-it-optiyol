using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Extensions;
/// <summary>
/// String işlemleri için kullanılan extension methodları 
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// String ifadesini pascal case'e çevirir.
    /// </summary>
    /// <param name="thestring">String</param>
    /// <returns>String</returns>
    public static string ToPascalCase(this string thestring)
    {
        if (string.IsNullOrWhiteSpace(thestring)) return thestring;

        var culture = new CultureInfo("en");
        if (thestring.Length < 2) return thestring.ToUpper(culture);

        string[] words = thestring.Split(
            new char[] { },
            StringSplitOptions.RemoveEmptyEntries);

        string result = "";
        foreach (string word in words)
        {
            result +=
                word.Substring(0, 1).ToUpper(culture) +
                word.Substring(1);
        }

        return result;
    }

    /// <summary>
    /// String nesnesini Camel Case'e çevirir.
    /// </summary>
    /// <param name="thestring">String</param>
    /// <returns>String</returns>
    public static string ToCamelCase(this string thestring)
    {
        if (string.IsNullOrWhiteSpace(thestring) || thestring.Length < 2)
            return thestring;

        var culture = new CultureInfo("en");

        string[] words = thestring.Split(
            new char[] { },
            StringSplitOptions.RemoveEmptyEntries);

        string result = words[0].ToLower(culture);
        for (int i = 1; i < words.Length; i++)
        {
            result +=
                words[i].Substring(0, 1).ToUpper(culture) +
                words[i].Substring(1);
        }

        return result;
    }

    /// <summary>
    /// String nesnesini Title Case'e çevirir.
    /// </summary>
    /// <param name="thestring">String</param>
    /// <returns>String</returns>
    public static string ToTitleCase(this string thestring)
    {
        if (string.IsNullOrWhiteSpace(thestring))
            return thestring;

        var culture = CultureInfo.CurrentCulture;
        return culture.TextInfo.ToTitleCase(thestring);
    }

    /// <summary>
    /// String nesnesinin içerisindeki dil bazlı karakterleri latin alfabesine çevirir.
    /// </summary>
    /// <param name="thestring"></param>
    /// <returns></returns>
    public static string RemoveDiacritics(this string thestring)
    {
        Encoding srcEncoding = Encoding.UTF8;
        Encoding destEncoding = Encoding.GetEncoding(1252); // Latin alphabet

        thestring = destEncoding.GetString(Encoding.Convert(srcEncoding, destEncoding, srcEncoding.GetBytes(thestring)));

        string normalizedString = thestring.Normalize(NormalizationForm.FormD);
        StringBuilder result = new StringBuilder();

        for (int i = 0; i < normalizedString.Length; i++)
        {
            if (!CharUnicodeInfo.GetUnicodeCategory(normalizedString[i]).Equals(UnicodeCategory.NonSpacingMark))
            {
                result.Append(normalizedString[i]);
            }
        }

        return result.ToString();
    }

    /// <summary>
    /// Aramalarda kullanılması için string nesnesinin içindeki dil bazlı karakterleri latin'e çevirir ve küçük harflere çevirir.
    /// </summary>
    /// <param name="thestring">String</param>
    /// <returns></returns>
    public static string ToSearchMode(this string thestring)
    {
        if (string.IsNullOrWhiteSpace(thestring))
            return string.Empty;

        string result = thestring.Trim().RemoveDiacritics().ToLowerInvariant();

        return result;
    }

    /// <summary>
    /// String nesnesini Guid'e çevirir. Guid formatında değilse null döner.
    /// </summary>
    /// <param name="thestring">String</param>
    /// <returns>String</returns>
    public static Guid? ToGuid(this string thestring)
    {
        if (string.IsNullOrWhiteSpace(thestring))
            return null;

        var isGuid = Guid.TryParse(thestring, out Guid result);

        if (isGuid)
        {
            return result;
        }

        return null;
    }

    /// <summary>
    /// String nesnesini Int'e çevirir. Int formatında değilse null döner.
    /// </summary>
    /// <param name="thestring">String</param>
    /// <returns>String</returns>
    public static int? ToInt(this string thestring)
    {
        if (string.IsNullOrWhiteSpace(thestring))
            return null;

        var isInteger = int.TryParse(thestring, out int result);

        if (isInteger)
        {
            return result;
        }

        return null;
    }

    /// <summary>
    /// String nesnesini Long'e çevirir. Long formatında değilse null döner.
    /// </summary>
    /// <param name="thestring">String</param>
    /// <returns>String</returns>
    public static long? ToLong(this string thestring)
    {
        if (string.IsNullOrWhiteSpace(thestring))
            return null;

        var isInteger = long.TryParse(thestring, out long result);

        if (isInteger)
        {
            return result;
        }

        return null;
    }

    /// <summary>
    /// String input'un başından istenilen ifadeyi siler.
    /// </summary>
    /// <param name="thestring"></param>
    /// <param name="trimString"></param>
    /// <param name="times">Kaç kere trim'lesin bilgisi</param>
    /// <returns></returns>
    public static string TrimStart(this string thestring, string trimString, int? times = null)
    {
        if (string.IsNullOrEmpty(thestring)) return string.Empty;
        if (string.IsNullOrEmpty(trimString)) return thestring;

        string result = thestring;
        while (result.StartsWith(trimString) && (times == null || times != 0))
        {
            result = result.Substring(trimString.Length);
            if (times != null) times--;
        }

        return result;
    }

    /// <summary>
    /// String input'un sonundan istenilen ifadeyi siler.
    /// </summary>
    /// <param name="thestring"></param>
    /// <param name="trimString"></param>
    /// <param name="times">Kaç kere trim'lesin bilgisi</param>
    public static string TrimEnd(this string thestring, string trimString, int? times = null)
    {
        if (string.IsNullOrEmpty(thestring)) return string.Empty;
        if (string.IsNullOrEmpty(trimString)) return thestring;

        string result = thestring;
        while (result.EndsWith(trimString) && (times == null || times != 0))
        {
            result = result.Substring(0, result.Length - trimString.Length);
            if (times != null) times--;
        }

        return result;
    }

    /// <summary>
    /// Belirtilen son char değeri dışındaki tüm değerleri gönderilen char ile değiştirir.
    /// </summary>
    /// <param name="thestring"></param>
    /// <param name="replaceChar"></param>
    /// <param name="last"></param>
    /// <returns></returns>
    public static string ReplaceAllExceptLast(this string thestring, char replaceChar, int last)
    {
        if (string.IsNullOrEmpty(thestring)) return thestring;

        var length = thestring.Length;

        if (length <= last)
        {
            return thestring;
        }

        return new String(replaceChar, length - last) + thestring.Substring(length - last);
    }

    /// <summary>
    /// Belirtilen array içerisindeki değerleri string'de arar ve newValue ile değiştirir.
    /// </summary>
    /// <param name="thestring"></param>
    /// <param name="toBeReplaced"></param>
    /// <param name="newValue"></param>
    /// <returns></returns>
    public static string ReplaceAll(this string thestring, string[] toBeReplaced, string newValue)
    {
        if (string.IsNullOrEmpty(thestring) || toBeReplaced == null || toBeReplaced.Length <= 0) return thestring;

        if (newValue == null) newValue = string.Empty;

        foreach (string str in toBeReplaced)
            if (!string.IsNullOrEmpty(str))
                thestring = thestring.Replace(str, newValue);

        return thestring;
    }

    /// <summary>
    /// string.Format Extension Methodu.
    /// </summary>
    /// <param name="theString"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public static string Format(this string theString, params object[] args)
    {
        if (string.IsNullOrEmpty(theString)) return string.Empty;

        return string.Format(theString, args);
    }

    /// <summary>
    /// String'i datetime'e parse edip döndürür.     
    /// </summary>
    /// <param name="thestring">String</param>
    /// <returns>String</returns>
    public static DateTime? ToShortDate(this string thestring)
    {
        if (string.IsNullOrWhiteSpace(thestring))
        {
            return null;
        }

        return DateTime.ParseExact(thestring, CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern, null);
    }

    /// <summary>
    /// String'i datetime'e parse edip döndürür.     
    /// </summary>
    /// <param name="thestring">String</param>
    /// <param name="seperator">Datetime değerleri arasındaki ayraç</param>
    /// <returns>String</returns>
    public static DateTime[] ToShortDateArray(this string thestring, char seperator)
    {
        return thestring.Trim().Split(seperator).Select(p => DateTime.ParseExact(p.Trim(), CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern, null)).ToArray();
    }

    /// <summary>
    /// String'i base64 formatına çevirip döndürür.
    /// </summary>
    /// <param name="thestring"></param>
    /// <returns></returns>
    public static string Base64Encode(this string thestring)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(thestring);
        return System.Convert.ToBase64String(plainTextBytes);
    }

    /// <summary>
    /// Base64 string'i plain text'e çevirip döndürür.
    /// </summary>
    /// <param name="thestring"></param>
    /// <returns></returns>
    public static string Base64Decode(this string thestring)
    {
        var base64EncodedBytes = System.Convert.FromBase64String(thestring);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }

    /// <summary>
    ///     A string extension method that replace first occurence.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    /// <returns>The string with the first occurence of old value replace by new value.</returns>
    public static string ReplaceFirst(this string @this, string oldValue, string newValue)
    {
        int startindex = @this.IndexOf(oldValue);

        if (startindex == -1)
        {
            return @this;
        }

        return @this.Remove(startindex, oldValue.Length).Insert(startindex, newValue);
    }

    /// <summary>
    ///     A string extension method that replace first number of occurences.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="number">Number of.</param>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    /// <returns>The string with the numbers of occurences of old value replace by new value.</returns>
    public static string ReplaceFirst(this string @this, int number, string oldValue, string newValue)
    {
        List<string> list = @this.Split(oldValue).ToList();
        int old = number + 1;
        IEnumerable<string> listStart = list.Take(old);
        IEnumerable<string> listEnd = list.Skip(old);

        return string.Join(newValue, listStart) +
               (listEnd.Any() ? oldValue : "") +
               string.Join(oldValue, listEnd);
    }
}
