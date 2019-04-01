using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace TodoAPI.ApiResponses
{
    public class ApiSuccessResponse<T> : ApiBaseResponse
    {
        [JsonProperty("data", Required = Required.Always)]
        public T Data { get; set; }

        [JsonProperty("params", NullValueHandling = NullValueHandling.Ignore)]
        public IDictionary<string, string> Params { get; set; }

        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        public ApiSuccessResponse()
        {
        }

        public ApiSuccessResponse(HttpStatusCode statusCode, T data, string message = null, string method = null, string apiVersion = null) : base(statusCode, method, apiVersion)
        {
            Data = data;
        }
    }
}
