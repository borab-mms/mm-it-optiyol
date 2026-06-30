using MM.IT.Data.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MM.Optiyol.Api.Data.Entities.External.MMLogistics.Optiyol
{
	
	[Table("Status", Schema = "Optiyol")]
	public class OptiyolStatusEntity : BaseEntity<int>
	{
		[Key]
		public int Id { get; set; }
		public string EventName { get; set; }
		public string? ServiceType { get; set; }
		public string? StatusName { get; set; }
		public string? Description { get; set; }
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime CreatedDate { get; set; }

	}
}
