using MM.IT.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MediaMarktIT
{

    /// <summary>
    /// MM_Products Tablosu 
    /// </summary>
    [Table("MM_Products", Schema = "dbo")]
    public class MMProductsEntity : IEntity
    {
        [Key]
        public int ID { get; set; }
        public int? ProductId { get; set; }
        public string? ProductName { get; set; }
        public int? LogisticClass { get; set; }
        public string? DescriptionShort { get; set; }
        public string? DescriptionLong { get; set; }
        public int? PG { get; set; }
        public int? MPG { get; set; }
        public string? SalesPrice { get; set; }
        public string? Barcode { get; set; }
        public string? ContentJSon { get; set; }
        public string? CatalogGroupId { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
