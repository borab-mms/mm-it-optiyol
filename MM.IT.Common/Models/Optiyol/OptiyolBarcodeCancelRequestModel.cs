namespace MM.Optiyol.Api.Models.Optiyol
{
    public class OptiyolBarcodeCancelRequestModel
    {
        public string OrderId { get; set; }
    }
    public class OptiyolBarcodeCancelResponseModel
    {
        public string OrderId { get; set; }
        public string Detail { get; set; }
    }
}
