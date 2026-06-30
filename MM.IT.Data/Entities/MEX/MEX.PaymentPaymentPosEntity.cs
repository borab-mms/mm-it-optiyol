using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MEX;

/// <summary>
/// PaymentPaymentPoss Tablosu 
/// </summary>
[Table("PaymentPaymentPoss", Schema = "MEX")]
public class MEXPaymentPaymentPosEntity : BaseEntity<int>
{
    public int PaymentId { get; set; }
    public int ItemId { get; set; }
    public string PosId { get; set; }
    public string PosBankCode { get; set; }
    public string PosBankName { get; set; }
    public string CardHolderNameFromBank { get; set; }
    public string CardProcessingType { get; set; }
    public string ReturnCode { get; set; }
    public string Message { get; set; }
    public DateTime HostDate { get; set; }
    public string AuthCode { get; set; }
    public string TransId { get; set; }
    public string ReferenceNo { get; set; }
    public string CustomData { get; set; }
}