// See https://aka.ms/new-console-template for more information
using CallApi.Helper;
using CallApi.Model;
using RestSharp;
using System.Runtime.InteropServices;
using System;
using System.Runtime.InteropServices;
using System.Text;
unsafe class Program
{
    private static void Main(string[] args)
    {
        string baseUrl = "http://192.168.1.150:9000";
        var requestModel = new RequestModel
        {
            Url = "https://browserleaks.com/ssl",
            UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/108.0.0.0 Safari/537.36 Edg/108.0.1462.54",
            Proxy = "",
            AllowRedirect = false.ToString(),
            Timeout = 100,
            RequestMethod = Method.Get
        };
        var options = new RestClientOptions(baseUrl)
        {

        };

        Environment.SetEnvironmentVariable("GODEBUG", "cgocheck=2");
        var ret = GoMath.Add(1, 2);
        var ret2 = GoMath.AddNum(2);
        string msg = "I am the Hal 9000";
        GoString s = new GoString
        {
            p = Marshal.StringToHGlobalAnsi(msg),
            n = msg.Length
        };


        var methodValue = "GET";
        var method = new GoString
        {
            p = Marshal.StringToHGlobalAnsi(methodValue),
            n = methodValue.Length
        };

        var pageURLValue = "https://tls.browserleaks.com/json";
        pageURLValue = "https://tls.peet.ws/api/all";
        pageURLValue = requestModel.Url;
        pageURLValue = "https://tls.browserleaks.com/json";
        var pageURL = new GoString
        {
            p = Marshal.StringToHGlobalAnsi(pageURLValue),
            n = pageURLValue.Length
        };


        var userAgentValue = requestModel.UserAgent;
        var userAgent = userAgentValue.ToGoString();
        /*var userAgent = new GoString
        {
            p = Marshal.StringToHGlobalAnsi(userAgentValue),
            n = userAgentValue.Length
        };*/


        var proxyValue = requestModel.Proxy;
        var proxy = proxyValue.ToGoString();
        /*var proxy = new GoString
        {
            p = Marshal.StringToHGlobalAnsi(proxyValue),
            n = proxyValue.Length
        };*/

        var queryStringValue = "";
        var queryString = queryStringValue.ToGoString();
        /*var queryString = new GoString
        {
            p = Marshal.StringToHGlobalAnsi(queryStringValue),
            n = queryStringValue.Length
        };*/

        var bodyJsonValue = "";
        var bodyJson = bodyJsonValue.ToGoString();
        /*var bodyJson = new GoString
        {
            p = Marshal.StringToHGlobalAnsi(bodyJsonValue),
            n = bodyJsonValue.Length
        };*/

        var retTls = GoHello.CallTls(method, pageURL, userAgent, proxy, queryString, bodyJson);
        var tlsResponse = GoUtils.PointerToString(retTls);
        /*byte* buf = (byte*)retTls;
        byte[] lenBytes = new byte[4];

        for (int i = 0; i < 4; i++)
        {
            lenBytes[i] = *buf++;
        }
;
        // Read the result itself

        int n = BitConverter.ToInt32(lenBytes, 0);
        int j = 0;
        byte[] bytes = new byte[n];

        for (int i = 0; i < n; i++)
        {
            if (i < 4)
            {
                *buf++ = 0;
            }
            else
            {
                bytes[j] = *buf++;
                j++;
            }
        }

        Console.WriteLine($"Test: {Encoding.UTF8.GetString(bytes)}");*/
        Console.WriteLine($"tlsResponse: {tlsResponse}");
        
        //GoMath.TestParam2(s, s, s, s, s, data, data);
        var client = new RestClient("https://tls.browserleaks.com/json");
        var request = new RestRequest("/", Method.Get);
        request.AddHeader("User-Agent", requestModel.UserAgent);
        client.AllowTlsOnGetRequests();
        var goResponse = client.Execute(request);
        Console.WriteLine($"Go Response Content: {goResponse.Content}");

        GoRest.Service.GoRestService restService = new GoRest.Service.GoRestService();
        var response2 = restService.Request("https://tls.browserleaks.com/json");
        Console.WriteLine($"Response Code: {response2.StatusCode}");
        Console.WriteLine($"Response Content: {response2.Content}");
    }
}



//Console.WriteLine("Start Call Api");
//ApiHelper api = new ApiHelper(baseUrl);
//var response = await api.CallApi2(requestModel);
//Console.WriteLine($"Response Code: {response.StatusCode}");
//Console.WriteLine($"Response Content: {response.Content}");
//Console.WriteLine("End Call Api");
//Console.ReadLine();


