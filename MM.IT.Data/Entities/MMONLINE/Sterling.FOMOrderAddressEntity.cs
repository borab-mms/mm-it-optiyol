using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE;

/// <summary>
/// OrderAddress Tablosu 
/// </summary>
[Table("OrderAddress", Schema = "Sterling")]
public class FOMOrderAddressEntity : BaseEntity<int>
{

    /// <summary>
    /// CustomerOrderNumber Bilgisi
    /// </summary>
    public string? CustomerOrderNumber { get; set; }

    /// <summary>
    /// FulfillmentOrderId Bilgisi
    /// </summary>
    /// 
    public string? FulfillmentOrderId { get; set; }

    /// <summary>
    /// ProductId Bilgisi
    /// </summary>
    /// 
    public string? ProductId { get; set; }

    /// <summary>
    /// LineItemId Bilgisi
    /// </summary>
    /// 
    public string? LineItemId { get; set; }

    /// <summary>
    /// LineItemReference Bilgisi
    /// </summary>
    /// 
    public string? LineItemReference { get; set; }

    /// <summary>
    /// LineItemStatusDescription Bilgisi
    /// </summary>
    /// 
    public string? LineItemStatusDescription { get; set; }

    /// <summary>
    /// AddressType Bilgisi
    /// </summary>
    public string? AddressType { get; set; }

    /// <summary>
    /// Salutation Bilgisi
    /// </summary>
    /// 
    public string? Salutation { get; set; }

    /// <summary>
    /// FirstName Bilgisi
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// LastName Bilgisi
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// CompanyName Bilgisi
    /// </summary>
    public string? CompanyName { get; set; }

    /// <summary>
    /// Department Bilgisi
    /// </summary>
    public string? Department { get; set; }

    /// <summary>
    /// PhoneNumber Bilgisi
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// MobileNumber Bilgisi
    /// </summary>
    public string? MobileNumber { get; set; }

    /// <summary>
    /// AddressLine1 Bilgisi
    /// </summary>
    public string? AddressLine1 { get; set; }

    /// <summary>
    /// AddressLine2 Bilgisi
    /// </summary>
    public string? AddressLine2 { get; set; }

    /// <summary>
    /// ZipCode Bilgisi
    /// </summary>
    public string? ZipCode { get; set; }

    /// <summary>
    /// City Bilgisi
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// Country Bilgisi
    /// </summary>
    public string? Country { get; set; }

    /// <summary>
    /// AddressId Bilgisi
    /// </summary>
    public string? AddressId { get; set; }

    /// <summary>
    /// PickupLocation Bilgisi
    /// </summary>
    public string? PickupLocation { get; set; }

    /// <summary>
    /// Title Bilgisi
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Street Bilgisi
    /// </summary>
    public string? Street { get; set; }

    /// <summary>
    /// Güncelleme tarihi bilgisini saklar.
    /// </summary>
    public DateTime? UpdatedDate { get; set; }

}
