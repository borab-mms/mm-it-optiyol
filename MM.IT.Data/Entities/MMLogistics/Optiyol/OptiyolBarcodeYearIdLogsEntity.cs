
using MM.IT.Data.Entities.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace MM.Optiyol.Api.Data.Entities.External.MMLogistics.Optiyol
{
    [Table("BarcodeYearIdLogs",Schema="Optiyol")]
    public class OptiyolBarcodeYearIdLogsEntity:IEntity<Guid>
    {
        public Guid Id { get; set; }
        public int Year {  get; set; }
        public int LastId { get; set; }
    }
}
