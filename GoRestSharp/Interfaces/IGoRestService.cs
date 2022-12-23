
using GoRestService.Models;
using RestSharp;

namespace GoRestService.Service
{
    public interface IGoRestService
    {
        RestResponse Request(string url, Method method = Method.Get, GoRestConfig configs = default);
        Task<RestResponse> RequestAsync(string url, Method method = Method.Get, GoRestConfig configs = default);
    }
}