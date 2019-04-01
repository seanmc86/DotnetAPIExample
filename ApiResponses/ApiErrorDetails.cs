using Newtonsoft.Json;

namespace TodoAPI.ApiResponses
{
    public class ApiErrorDetails
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("reason", NullValueHandling = NullValueHandling.Ignore)]
        public string Reason { get; set; }

        public ApiErrorDetails()
        {
        }

        public ApiErrorDetails(string message, string reason = null)
        {
            Message = message;
            Reason = reason;
        }

        public ApiErrorDetails(ApiErrorDetails apiErrorDetails)
        {
            Message = apiErrorDetails.Message;
            Reason = apiErrorDetails.Reason;
        }
    }
}
