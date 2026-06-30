using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.MasterData
{
    public class ArtikelInfoModel
    {
        public int ArticleId { get; set; }
        public string ArticleName { get; set; }
        public short PgId { get; set; }
        public short BrandId { get; set; }
    }
}
