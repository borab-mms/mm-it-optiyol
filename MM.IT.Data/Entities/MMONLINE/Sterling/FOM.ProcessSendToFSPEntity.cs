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
	/// ProcessSendToFSPs Tablosu 
	/// </summary>
	[Table("ProcessSendToFSPs", Schema = "FOM")]
	public class SterlingProcessSendToFSPEntity : IEntity
	{

		[Key]
		public int Id { get; set; }
		public int CustomerOrderNumber { get; set; }
		public int? ArvatoStatus { get; set; }
		//[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime? UpdatedDate { get; set; }
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime CreatedDate { get; set; }

	}
}
