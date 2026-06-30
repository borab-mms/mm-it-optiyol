using MM.IT.Data.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MM.Optiyol.Api.Data.Entities.External.MMLogistics.Optiyol
{
	[Table("OrderStatusHistory", Schema = "Optiyol")]
	public class OptiyolOrderStatusEntity: BaseEntity<int>
	{
			[Key]
			public int Id { get; set; }

			public string OrderId { get; set; }
			public int StatusId { get; set; }
			public DateTime? UpdatedDate { get; set; }
			[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
			public DateTime CreatedDate { get; set; }

		
	}
}
