using MM.IT.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.FOM;

/// <summary>
/// Fom Data DB -> Order Head Entity Nesnesi
/// </summary>
[Table("Order_Head", Schema = "dbo")]
public class FomOrderHeadEntity : IEntity
{
    public string customer_order_number { get; set; }
    public DateTime order_date { get; set; }
    public string order_status { get; set; }
    public string fulfillment_method { get; set; }
    public int contractual_partner { get; set; }
    public string sa_first_name { get; set; }
    public string sa_last_name { get; set; }
    public string sa_company_name { get; set; }
    public string sa_phone_number { get; set; }
    public string sa_mobile_number { get; set; }
    public string sa_address_line1 { get; set; }
    public string sa_address_line2 { get; set; }
    public string sa_zip_code { get; set; }
    public string sa_city { get; set; }
    public string sa_country { get; set; }
    public string bp_customer_user_id { get; set; }
    public string bp_first_name { get; set; }
    public string bp_last_name { get; set; }
    public string bp_email { get; set; }
    public string bp_tax_id { get; set; }
    public string bp_ba_first_name { get; set; }
    public string bp_ba_last_name { get; set; }
    public string bp_ba_company { get; set; }
    public string bp_ba_street1 { get; set; }
    public string bp_ba_street2 { get; set; }
    public string bp_ba_street3 { get; set; }
    public string bp_ba_city { get; set; }
    public string bp_ba_country { get; set; }
    public string bp_ba_zip_code { get; set; }
}
