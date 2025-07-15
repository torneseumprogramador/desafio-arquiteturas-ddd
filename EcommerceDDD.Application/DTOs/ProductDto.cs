using System;
using System.Text.Json.Serialization;

namespace EcommerceDDD.Application.DTOs
{
    public class ProductDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("nome")]
        public string Name { get; set; }

        [JsonPropertyName("descricao")]
        public string Description { get; set; }

        [JsonPropertyName("preco")]
        public decimal Price { get; set; }

        [JsonPropertyName("estoque")]
        public int Stock { get; set; }

        [JsonPropertyName("data_criacao")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("data_atualizacao")]
        public DateTime? UpdatedAt { get; set; }
    }
} 