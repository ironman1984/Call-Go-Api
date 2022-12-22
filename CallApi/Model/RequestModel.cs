namespace CallApi.Model
{
    public class RequestModel
    {
        public string? Url { get; set; }

        public string? UserAgent { get; set; }

        public string? Proxy { get; set; }
        public string? AllowRedirect { get; set; }

        public int Timeout { get; set; }

        public RestSharp.Method RequestMethod { get; set; }

        public Dictionary<string, string>? Params { get; set; }

        public object? Body { get; set; }

        public string? JsonBody { get; set; }

        public Dictionary<string, string>? Headers { get; set; }
    }
}
