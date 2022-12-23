
namespace GoRestService.Models
{
    public class GoRestConfig
    {
        #region Constructor
        public GoRestConfig(GoRestAuthorize authorization = default, object body = default, Dictionary<string, string> headers = default, string userAgen = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/108.0.0.0 Safari/537.36 Edg/108.0.1462.54")
        {
            Headers = new Dictionary<string, string>();
            Parameters = new Dictionary<string, string>();
            Authorization = authorization;
            Body = body;
            Headers = headers;
            UserAgent= userAgen;
        }
        #endregion

        public string Proxy { get; set; }

        public string UserAgent { get; set; }

        public GoRestAuthorize Authorization { get; set; }
        public object Body { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
    }
}