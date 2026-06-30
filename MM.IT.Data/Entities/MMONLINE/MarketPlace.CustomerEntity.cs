using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE
{
    /// <summary>
    /// Customers Tablosu 
    /// </summary>
    [Table("Customers", Schema = "MarketPlace")]
    public class CustomerEntity : BaseEntity<int>
    {
        public string OrderHeadId { get; set; }
        public string BillingAddressFirstName { get; set; }
        public string BillingAddressLastName { get; set; }
        public string BillingAddressEmail { get; set; }
        public string BillingAddress { get; set; }
        public string BillingAddressMobileNumber { get; set; }
        //public string BillingAddressStreet { get; set; }
        public string BillingAddressZipCode { get; set; }
        public string BillingAddressDistrict { get; set; }
        public string BillingAddressCity { get; set; }
        public string BillingAddressTownship { get; set; }
        public string BillingAddressCompanyName { get; set; }
        public string BillingAddressCountry { get; set; }
        public bool BillingAddressIsEinvoice { get; set; }
        public string BillingAddressTaxOffice { get; set; }
        public string BillingAddressTaxNo { get; set; }
        public string BillingAddressTcKimlikNo { get; set; }
        public string ShippingAddressFirstName { get; set; }
        public string ShippingAddressLastName { get; set; }
        public string ShippingAddressEmail { get; set; }
        public string ShippingAddressMobileNumber { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingAddressZipCode { get; set; }
        public string ShippingAddressDistrict { get; set; }
        public string ShippingAddressCity { get; set; }
        public string ShippingAddressTownship { get; set; }

    }
}
