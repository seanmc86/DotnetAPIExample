using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace TodoAPI.ApiResponses
{
    public class ApiErrorResponse : ApiBaseResponse
    {
        [JsonProperty("error", Required = Required.Always)]
        public ApiError Error { get; set; }

        public ApiErrorResponse()
        {
        }

        public ApiErrorResponse(HttpStatusCode statusCode, ApiError error, string method = null, string apiVersion = null) : base(statusCode, method, apiVersion)
        {
            Error = error;
        }

        public ApiErrorResponse(HttpStatusCode statusCode, string message, string method = null, string apiVersion = null) : base(statusCode, method, apiVersion)
        {
            Error = new ApiError(message);
        }
    }
}
