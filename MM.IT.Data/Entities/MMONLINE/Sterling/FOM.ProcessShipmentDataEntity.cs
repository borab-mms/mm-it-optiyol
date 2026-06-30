using MM.IT.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE.Sterling
{
	/// <summary>
	/// ProcessShipmentDatas Tablosu 
	/// </summary>
	[Table("ProcessShipmentDatas", Schema = "FOM")]
	public class SterlingProcessShipmentDataEntity : IEntity
	{

		[Key]
		public string OrderFulfillmentId { get; set; }
		public int CustomerOrderNumber { get; set; }
		public byte? StatusId { get; set; }
		public byte? SellerId { get; set; }
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime UpdatedDate { get; set; }
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime CreatedDate { get; set; }

	}
}
