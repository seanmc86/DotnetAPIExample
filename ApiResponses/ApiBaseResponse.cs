using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;

namespace TodoAPI.ApiResponses
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ApiBaseResponse
    {
        [JsonProperty("statusCode", Required = Required.Always)]
        public HttpStatusCode StatusCode { get; set; }

        [JsonProperty("apiVersion", NullValueHandling = NullValueHandling.Ignore)]
        public string ApiVersion { get; set; }

        [JsonProperty("method", NullValueHandling = NullValueHandling.Ignore)]
        public string Method { get; set; }

        public ApiBaseResponse()
        {
        }

        public ApiBaseResponse(HttpStatusCode statusCode, string method = null, string apiVersion = null)
        {
            StatusCode = statusCode;
            Method = method;
            ApiVersion = apiVersion;
        }
    }
}
