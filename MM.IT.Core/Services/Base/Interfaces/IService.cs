using MM.IT.Common.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Services.Base.Interfaces;
/// <summary>
/// Business Service Interface Tanımı
/// Tüm Business servis sınıfları bu interface'den türemelidir.
/// </summary>
public interface IService
{
    /// <summary>
    /// Mesaj bilgisi alarak Servis Result Model oluşturup döner. 
    /// Bu result bilgisinde Code: 200(Success)'dür.
    /// </summary>
    /// <param name="message">String: Business servis sonuç mesajı</param>
    /// <returns>ServiceResultModel: Business servis result nesnesi</returns>
    ServiceResultModel Result(string message);

    /// <summary>
    /// Mesaj ve code(HTTP, int) bilgisi alarak Servis Result Model oluşturup döner.  
    /// </summary>
    /// <param name="message">String: Business servis sonuç mesajı</param>
    /// <param name="code">Int: Http Status Code</param>
    /// <returns>ServiceResultModel: Business servis result nesnesi</returns>
    ServiceResultModel Result(string message, int code);

    /// <summary>
    /// Mesaj ve code(HTTP, int) ve data bilgisi alarak Servis Result Model oluşturup döner.   
    /// </summary>
    /// <param name="data">Object: Object tipinde sonuç datası</param>
    /// <param name="message">String: Business servis sonuç mesajı</param>
    /// <param name="code">Int: Http Status Code</param>
    /// <returns></returns>
    ServiceResultModel Result(object data, string message, int code);

    /// <summary>
    /// Mesaj ve code(HTTP, int) ve data bilgisi alarak Servis Result Model oluşturup döner.  
    /// </summary>
    /// <typeparam name="TDataModel">TDataModel: Generic Data model tipi</typeparam>
    /// <param name="data">TDataModel: sonuç datası</param>
    /// <param name="message">String: Business servis sonuç mesajı</param>
    /// <param name="code">Int: Http Status Code</param>
    /// <returns></returns>
    ServiceResultModel<TDataModel> Result<TDataModel>(TDataModel data, string message, int code);
}
