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
	/// MissingDataOrders Tablosu 
	/// </summary>
	[Table("MissingDataOrders", Schema = "FOM")]
	public class SterlingMissingDataOrderEntity : IEntity
	{
		[Key]
		public int Id { get; set; }
		public int CustomerOrderNumber { get; set; }
		public string Source { get; set; }
		public bool IsActive { get; set; }
		public DateTime CreatedDate { get; set; }
	}
}
