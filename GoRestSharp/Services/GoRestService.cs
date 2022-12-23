
using GoRestService.Models;
using GoRestService.Service;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Text.Json;

namespace GoRest.Service
{
    public class GoRestService : IGoRestService
    {
        #region variable
        public RestClient client;
        public string baseUrl;
        public string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/108.0.0.0 Safari/537.36 Edg/108.0.1462.54";
        #endregion

        #region construction
        public GoRestService()
        {
            ReadConfig("");
        }

        public GoRestService(string baseUrl)
        {
            this.baseUrl = baseUrl;
            ReadConfig(baseUrl);

        }
        #endregion

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

        /// <summary>
        /// Create request from url
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="configs"></param>
        /// <returns></returns>
        public RestResponse Request(string url, Method method = Method.Get, GoRestConfig configs = default)
        {
            try
            {
                string resource = "/api";
                var request = new RestRequest(resource, method);

                #region Add Url
                if (!string.IsNullOrEmpty(url))
                {
                    request.AddHeader("Poptls-Url", url);
                }
                #endregion

                if (configs != null)
                {
                    #region User Agent
                    if (!string.IsNullOrEmpty(configs.UserAgent))
                    {
                        request.AddHeader("User-Agent", configs.UserAgent);
                    }
                    #endregion

                    #region Proxy
                    if (!string.IsNullOrEmpty(configs.Proxy))
                    {
                        request.AddHeader("Poptls-Proxy", configs.Proxy);
                    }
                    #endregion

                    #region Header
                    if (configs.Headers != null)
                        foreach (var header in configs.Headers)
                            request.AddHeader(header.Key, header.Value);
                    request.AddHeader("Content-Type", "application/json");

                    // Authorize
                    if (configs.Authorization != null)
                        request.AddHeader("Authorization", "Bearer " + configs.Authorization.ToString());
                    #endregion
                    #region Parameter
                    if (configs.Body != null)
                    {
                        string bodySerialize = JsonSerializer.Serialize(configs.Body);
                        request.AddParameter("application/json", bodySerialize, ParameterType.RequestBody);
                    }
                    foreach (var parameter in configs.Parameters)
                        request.AddParameter(parameter.Key, parameter.Value);
                    #endregion
                }
                else
                {
                    #region User Agent
                    if (!string.IsNullOrEmpty(this.userAgent))
                    {
                        request.AddHeader("User-Agent", this.userAgent);
                    }
                    #endregion
                }

                return client.Execute(request);
            }
            catch
            {
                //logger.LogError($"Failed to request {url}");
                return default;
            }
        }

        /// <summary>
        /// Create request from url
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="configs"></param>
        /// <returns></returns>
        public async Task<RestResponse> RequestAsync(string url, Method method = Method.Get, GoRestConfig configs = default)
        {
            try
            {

                string resource = "/api";
                var request = new RestRequest(resource, method);

                #region Add Url
                if (!string.IsNullOrEmpty(url))
                {
                    request.AddHeader("Poptls-Url", url);
                }
                #endregion

                if (configs != null)
                {
                    #region User Agent
                    if (!string.IsNullOrEmpty(configs.UserAgent))
                    {
                        request.AddHeader("User-Agent", configs.UserAgent);
                    }
                    #endregion

                    #region Proxy
                    if (!string.IsNullOrEmpty(configs.Proxy))
                    {
                        request.AddHeader("Poptls-Proxy", configs.Proxy);
                    }
                    #endregion

                    #region Header
                    if (configs.Headers != null)
                        foreach (var header in configs.Headers)
                            request.AddHeader(header.Key, header.Value);
                    request.AddHeader("Content-Type", "application/json");

                    // Authorize
                    if (configs.Authorization != null)
                        request.AddHeader("Authorization", "Bearer " + configs.Authorization.ToString());
                    #endregion
                    #region Parameter
                    if (configs.Body != null)
                    {
                        string bodySerialize = JsonSerializer.Serialize(configs.Body);
                        request.AddParameter("application/json", bodySerialize, ParameterType.RequestBody);
                    }
                    foreach (var parameter in configs.Parameters)
                        request.AddParameter(parameter.Key, parameter.Value);
                    #endregion
                }
                else
                {
                    #region User Agent
                    if (!string.IsNullOrEmpty(this.userAgent))
                    {
                        request.AddHeader("User-Agent", this.userAgent);
                    }
                    #endregion
                }
                return await client.ExecuteAsync(request);
            }
            catch
            {
                //logger.LogError($"Failed to request {url}");
                return default;
            }
        }
    }
}