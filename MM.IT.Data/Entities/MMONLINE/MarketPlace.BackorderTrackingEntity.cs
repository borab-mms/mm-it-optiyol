using MM.IT.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE
{
	/// <summary>
	/// BackorderTracking Tablosu 
	/// </summary>
	[Table("BackorderTracking", Schema = "MarketPlace")]
	public class BackorderTrackingEntity : IEntity
	{
		[Key]
		public int Id { get; set; }
		public int? CustomerOrderNumber { get; set; }
		public int? SellerId { get; set; }
		public int? ProductId { get; set; }
		public int? StoreStock1282 { get; set; }
		public int? StoreStock6049 { get; set; }
		public int? StoreStock { get; set; }
		public int? FomStock { get; set; }
		public int? StatusId { get; set; }
		public string? Description { get; set; }
		public string? UpdatedByRegistrationNumber { get; set; }
		public DateTime? UpdatedDate { get; set; }
		public DateTime CreatedDate { get; set; }

	}
}
