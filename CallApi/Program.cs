// See https://aka.ms/new-console-template for more information
using CallApi.Helper;
using CallApi.Model;
using RestSharp;

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
GoRest.Service.GoRestService restService = new GoRest.Service.GoRestService();

var response2 = restService.Request("https://browserleaks.com/ssl");

Console.WriteLine(response2.StatusCode);

Console.WriteLine("Start Call Api");
ApiHelper api = new ApiHelper(baseUrl);
var response = await api.CallApi2(requestModel);
Console.WriteLine($"Response Code: {response.StatusCode}");
Console.WriteLine($"Response Content: {response.Content}");
Console.WriteLine("End Call Api");
Console.ReadLine();


