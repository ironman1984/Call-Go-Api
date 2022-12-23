using Amazon.Runtime.Internal;
using GoRestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CallApi
{
    public class GoRestClient
    {
        private string url;
        private string username;
        private string password;

        /// <summary>
        /// Initializes a new instance of the SugarRestClient class.
        /// </summary>
        public GoRestClient()
        {
        }

        /// <summary>
        /// Initializes a new instance of the SugarRestClient class.
        /// </summary>
        /// <param name="url">SugarCrm REST API url.</param>
        public GoRestClient(string url)
        {
            this.url = url;
        }


        /// <summary>
        /// Execute client.
        /// </summary>
        /// <param name="request">The request object.</param>
        /// <returns>SugarRestResponse object.</returns>
        public GoRestResponse Execute(GoRestRequest request)
        {
            GoRestResponse response = new GoRestResponse();
            
            ModelInfo modelInfo = ModelInfo.ReadByName(request.ModuleName);
            return this.InternalExceute(request, modelInfo);
        }

        
        /// <summary>
        /// Execute request asynchronously using SugarCrm module name.
        /// </summary>
        /// <param name="request">The request object.</param>
        /// <returns>SugarRestResponse object.</returns>
        public async Task<GoRestResponse> ExecuteAsync(GoRestRequest request)
        {
            GoRestResponse response = new GoRestResponse();
            

            ModelInfo modelInfo = ModelInfo.ReadByName(request.ModuleName);
            return await Task.Run(() => { return this.InternalExceute(request, modelInfo); });
        }


        /// <summary>
        /// Execute request asynchronously using the C# SugarCrm model type.
        /// </summary>
        /// <typeparam name="TEntity">The template parameter.</typeparam>
        /// <param name="request">The request object.</param>
        /// <returns>SugarRestResponse object.</returns>
        public async Task<GoRestResponse> ExecuteAsync<TEntity>(GoRestRequest request) where TEntity : EntityBase
        {
            ModelInfo modelInfo = ModelInfo.ReadByType(typeof(TEntity));
            request.ModuleName = modelInfo.ModelName;

            SugarRestResponse response = new SugarRestResponse();
            if (!this.IsRequestValidate(ref request, ref response))
            {
                return response;
            }

            return await Task.Run(() => { return this.InternalExceute(request, modelInfo); });
        }

        /// <summary>
        /// Execute request.
        /// </summary>
        /// <param name="request">The request object.</param>
        /// <param name="modelInfo">The model info for the referenced SugarCrm module.</param>
        /// <returns>SugarRestResponse object.</returns>
        private GoRestResponse InternalExceute(GoRestRequest request, ModelInfo modelInfo)
        {
            return this.Execute(request)
        }



    }
}
