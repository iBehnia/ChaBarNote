namespace Domain.Dto
{
    //public class Result
    //{
    //    public string Message { get; set; }
    //    public System.Net.HttpStatusCode StatusCode { get; set; }
    //}

    public class Result<T> where T : class
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public System.Net.HttpStatusCode StatusCode { get; set; }
    }
}
