using System;
using System.Text.Json.Serialization;

namespace EcommerceDDD.Application.DTOs
{
    public class PersonDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("nome")]
        public string Name { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("tipo_pessoa")]
        public string PersonType { get; set; }

        [JsonPropertyName("documento")]
        public string Document { get; set; }

        [JsonPropertyName("data_criacao")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("data_atualizacao")]
        public DateTime? UpdatedAt { get; set; }
    }
} 