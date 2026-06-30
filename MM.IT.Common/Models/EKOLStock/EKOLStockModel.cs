using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.EKOLStock
{
    public class EKOLStockModel
    {
        public string ArticleCode;

        public string AsnNo;

        public string BatchNr;

        public DateTime? ExpireDate;

        public decimal? EKOLStockId;

        public string OrgCode;

        public string PackagingGroup;

        public decimal? QtyStock;

        public decimal? QtyStockInPlanned;

        public decimal? QtyStockOutPlanned;

        public string SerialNr;

        public string StockLocation;

        public string StockType;

        public decimal? Desi;
        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
