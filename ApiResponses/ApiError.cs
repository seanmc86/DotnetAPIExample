using Newtonsoft.Json;

namespace TodoAPI.ApiResponses
{
    public class ApiError
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("errors", NullValueHandling = NullValueHandling.Ignore)]
        public ApiErrorDetails[] Errors { get; set; }

        public ApiError()
        {
        }

        public ApiError(string message, params ApiErrorDetails[] errorDetails)
        {
            Message = message;
            Errors = errorDetails;
        }
    }
}
