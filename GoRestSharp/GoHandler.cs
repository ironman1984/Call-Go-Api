using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Net.Http.Headers;
using System.Reflection;

public static class HttpMessageUtils
{
    public const string TlsName = "Content-Type";

    class HandlerAllowingTlsOnGetRequests : DelegatingHandler
    {
        public RestClient client;
        public string baseUrl;
        public string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/108.0.0.0 Safari/537.36 Edg/108.0.1462.54";

        /// <summary>
        /// Read configs
        /// </summary>
        /// <param name="baseUrl"></param>
        public void ReadConfig(string baseUrl)
        {
            if (string.IsNullOrEmpty(baseUrl))
            {
                string fileName = "appsettings.json";
                if (File.Exists(fileName))
                {
                    string fileContent = System.IO.File.ReadAllText(fileName);
                    JObject o1 = JObject.Parse(fileContent);
                    string apiUrl = (string)o1["ApiUrl"];
                    this.client = new RestClient(apiUrl);
                    client.Options.MaxTimeout = -1;
                    this.baseUrl = apiUrl;
                }
            }
            else
            {
                this.client = new RestClient(baseUrl);
                client.Options.MaxTimeout = -1;
            }
        }

        public HandlerAllowingTlsOnGetRequests(
            HttpMessageHandler p_innerHttpMessageHandler
        ) : base(p_innerHttpMessageHandler)
        { }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage p_request, CancellationToken p_cancellationToken)
        {
            string resource = "/api";
            Method sendMethod;
            switch (p_request.Method.Method)
            {
                case "GET":
                    sendMethod = Method.Get;
                    break;
                case "POST":
                    sendMethod = Method.Post;
                    break; 
                case "PUT":
                    sendMethod = Method.Put;
                    break;
                case "DELETE":
                    sendMethod = Method.Delete;
                    break;
                default:
                    sendMethod = Method.Get;
                    break;
            }    
            var request = new RestRequest(resource, sendMethod);
            #region Add Url
            if (!string.IsNullOrEmpty(p_request.RequestUri.AbsoluteUri))
            {
                request.AddHeader("Poptls-Url", p_request.RequestUri.AbsoluteUri);
            }
            #endregion

            #region Add User Agent
            if (p_request.Headers.UserAgent!=null)
            {
                var uAgen = p_request.Headers.UserAgent.ToString();
                if (uAgen.Contains("RestSharp"))
                    uAgen = userAgent;
                request.AddHeader("User-Agent", uAgen);
            }    
           
            #endregion
            HttpResponseMessage response = await base.SendAsync(p_request, p_cancellationToken);
            return response;
        }
    }

    public static HttpClient GetHttpClient(this RestClient p_restClient)
    {
        return ((TypeInfo)p_restClient.GetType())
            .DeclaredProperties
            .First(p => p.Name == "HttpClient")
            .GetValue(p_restClient)
            as HttpClient
        ;
    }

    private static FieldInfo GetHttpMessageHandlerField(this HttpClient p_httpClient)
    {
        var handler = ((TypeInfo)p_httpClient.GetType().BaseType).DeclaredFields;
        return handler.FirstOrDefault(f => f.Name == "_handler" || f.Name == "handler");
        //return ((TypeInfo)p_httpClient.GetType().BaseType)
        //    .DeclaredFields
        //    .First(f => f.Name == "handler")
        //;
    }

    public static HttpMessageHandler GetMessageHandler(this HttpClient p_httpClient)
    {
        return p_httpClient.GetHttpMessageHandlerField()
            .GetValue(p_httpClient)
            as HttpMessageHandler
        ;
    }

    public static void SetMessageHandler(this HttpClient p_httpClient, HttpMessageHandler p_newHttpMessageHandler)
    {
        p_httpClient.GetHttpMessageHandlerField().SetValue(p_httpClient, p_newHttpMessageHandler);
    }

    public static HttpMessageHandler GetInnerMessageHandler(this RestClient p_restClient)
    {
        return p_restClient.GetHttpClient().GetMessageHandler();
    }

    public static void AllowTlsOnGetRequests(this RestClient p_restClient)
    {
        HttpMessageHandler innerMessageHandler = p_restClient.GetInnerMessageHandler();
        HttpMessageHandler newMessageHandler = new HandlerAllowingTlsOnGetRequests(innerMessageHandler);
        p_restClient.GetHttpClient().SetMessageHandler(newMessageHandler);
    }
}