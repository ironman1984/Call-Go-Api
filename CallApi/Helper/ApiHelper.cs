using CallApi.Model;
using RestSharp;

namespace CallApi.Helper
{
    public class ApiHelper
    {
        #region variables
        public string baseUrl;
        RestClient client;
        RestRequest request;
        #endregion

        #region construction
        public ApiHelper() { }
        public ApiHelper(string baseUrl)
        {
            this.baseUrl = baseUrl;
            client = new RestClient(baseUrl);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Set base Url for Api
        /// </summary>
        /// <param name="baseUrl"></param>
        public void SetBaseUrl(string baseUrl)
        {
            this.baseUrl = baseUrl;
            client = new RestClient(baseUrl);
        }

        /// <summary>
        /// Call url
        /// </summary>
        /// <param name="requestInput"></param>
        /// <returns></returns>
        public async Task<RestResponse> CallApi(RequestModel requestInput)
        {
            RestRequest request = new RestRequest("/api", requestInput.RequestMethod);
            if (!string.IsNullOrEmpty(requestInput.Url))
            {
                request.AddHeader("Poptls-Url", requestInput.Url);
            }
            if (!string.IsNullOrEmpty(requestInput.UserAgent))
            {
                request.AddHeader("User-Agent", requestInput.UserAgent);
            }

            if (!string.IsNullOrEmpty(requestInput.Proxy))
            {
                request.AddHeader("Poptls-Proxy", requestInput.Proxy);
            }
            if (!string.IsNullOrEmpty(requestInput.AllowRedirect))
            {
                request.AddHeader("Poptls-Allowredirect", requestInput.AllowRedirect);
            }
            if (!string.IsNullOrEmpty(requestInput.Timeout.ToString()))
            {
                request.AddHeader("Poptls-Timeout", requestInput.Timeout.ToString());
            }
            if (requestInput.Params != null && requestInput.Params.Count > 0)
            {
                foreach (var item in requestInput.Params)
                {
                    request.AddQueryParameter(item.Key, item.Value);
                }
            }
            if (!string.IsNullOrEmpty(requestInput.JsonBody))
            {
                request.AddJsonBody(requestInput.JsonBody);
            }
            else
            {
                if (requestInput.Body != null)
                {
                    request.AddBody(requestInput.Body);
                }
            }

            // act
            RestResponse response = await client.ExecuteAsync(request);
            return response;
        }

        /// <summary>
        /// Call Api base gateway url and input params
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="requestInput"></param>
        /// <returns></returns>
        public async Task<RestResponse> CallApi(string baseUrl, RequestModel requestInput)
        {
            client = new RestClient(baseUrl);
            request = new RestRequest("/api", Method.Get);
            if (!string.IsNullOrEmpty(requestInput.Url))
            {
                request.AddHeader("Poptls-Url", requestInput.Url);
            }

            if (!string.IsNullOrEmpty(requestInput.UserAgent))
            {
                request.AddHeader("User-Agent", requestInput.UserAgent);
            }

            if (!string.IsNullOrEmpty(requestInput.Proxy))
            {
                request.AddHeader("Poptls-Proxy", requestInput.Proxy);
            }
            if (!string.IsNullOrEmpty(requestInput.AllowRedirect))
            {
                request.AddHeader("Poptls-Allowredirect", requestInput.AllowRedirect);
            }
            if (!string.IsNullOrEmpty(requestInput.Timeout.ToString()))
            {
                request.AddHeader("Poptls-Timeout", requestInput.Timeout.ToString());
            }

            if (requestInput.Params != null && requestInput.Params.Count > 0)
            {
                foreach (var item in requestInput.Params)
                {
                    request.AddQueryParameter(item.Key, item.Value);
                }
            }
            if (!string.IsNullOrEmpty(requestInput.JsonBody))
            {
                request.AddJsonBody(requestInput.JsonBody);
            }
            else
            {
                if (requestInput.Body != null)
                {
                    request.AddBody(requestInput.Body);
                }
            }
            // act
            RestResponse response = await client.ExecuteAsync(request);
            return response;
        }
        #endregion



    }
}
