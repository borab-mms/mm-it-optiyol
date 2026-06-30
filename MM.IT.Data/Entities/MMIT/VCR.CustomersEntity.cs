using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMIT;

/// <summary>
/// Customers Tablosu 
/// </summary>
[Table("Customers", Schema = "VCR")]
public class VCRCustomersEntity : BaseEntity<int>
{
    public string InvoiceId { get; set; }
    public string? ChannelCustomerId { get; set; }
    public string? FirstName { get; set; }
    public string? FamilyOrCompanyName { get; set; }
    public string? CompanyName { get; set; }
    public string? Address { get; set; }
    public string? District { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public string? TaxId { get; set; }
    public string? TaxOffice { get; set; }
    public string PhoneNumber { get; set; }
    public string EMail { get; set; }
    public bool? EMailFlag { get; set; }
    public string? LoyaltyCustomerText { get; set; }
    public string? LoyaltyCustomerNumber { get; set; }
}

