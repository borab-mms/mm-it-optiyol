using MM.IT.Data.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MM.Optiyol.Api.Data.Entities.External.MMLogistics.Optiyol
{

    [Table("RawDatas", Schema = "Optiyol")]
    public class OptiyolRawDataEntity : BaseEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// EventName Bilgisi
        /// </summary>
        public string? ServiceName { get; set; }

        /// <summary>
        /// TransactionId Bilgisi
        /// </summary>
        public string? TransactionId { get; set; }

        /// <summary>
        /// RawData Json Bilgisi
        /// </summary>
        public string? RawData { get; set; }

        /// <summary>
        /// Response Bilgisi
        /// </summary>
        public bool? IsParsed { get; set; }

        /// <summary>
        /// IsNewEvent Bilgisi
        /// </summary>
        public bool? IsNewEvent { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
