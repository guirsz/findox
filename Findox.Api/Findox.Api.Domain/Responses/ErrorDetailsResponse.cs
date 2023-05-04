using System.Text.Json;

namespace Findox.Api.Domain.Responses
{
    public class ErrorDetailsResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
