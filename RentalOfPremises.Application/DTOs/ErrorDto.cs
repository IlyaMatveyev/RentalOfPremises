using System.Text.Json;

namespace RentalOfPremises.Application.DTOs
{
    public class ErrorDto
    {
        public int StatusCode { get; set; } = 0;
        public string Message { get; set; } = string.Empty;

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
