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
    /// DC_TransactionResults Tablosu 
    /// </summary>
    [Table("DC_TransactionResults", Schema = "dbo")]
    public class DCTransactionResultEntity : BaseEntity<int>
    {
        public int CustomerOrderNumber { get; set; }
        public string LineItemId { get; set; }
        public DateTime LocalDateTime { get; set; }
        public DateTime ServerDateTime { get; set; }
        public string TxId { get; set; }
        public string HostTxId { get; set; }
        public decimal Amount { get; set; }
        public string PinCode { get; set; }
        public string SerialCode { get; set; }
        public string Result { get; set; }
        public string ResultText { get; set; }
        public string RequestData { get; set; }
        public string ResponseData { get; set; }
        public DateTime? SendingDate { get; set; }

    }
}
