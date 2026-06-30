namespace MM.Optiyol.Api.Models.Optiyol
{
	public class OptiyolSaveStatusModel
	{
		public string EventName { get; set; }
		public DateTime UpdatedDate { get; set; }
		public List<OptiyolSaveStatusDetail> Orders { get; set; } = new List<OptiyolSaveStatusDetail>();
	}
	public class OptiyolSaveStatusDetail
	{
		public string ServiceType { get; set; }
		public string OrderId { get; set; } 
	}
}
