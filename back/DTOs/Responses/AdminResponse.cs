using System;
using System.Text.Json.Serialization;

namespace EitechPfe.DTOs.Responses
{
    public class AdminResponse
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        [JsonPropertyName("api_key")]
        public string ApiKey { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdateAt { get; set; }
    }
}
