namespace BikeWebService.Models
{
    public class ResponseModel<T>
    {
        public T Data { get; set; }
        public int resultCode { get; set; }
        public string message { get; set; }
    }
}