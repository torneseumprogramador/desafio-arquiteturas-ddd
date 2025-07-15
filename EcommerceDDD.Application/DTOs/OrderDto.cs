using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EcommerceDDD.Application.DTOs
{
    public class OrderDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("id_pessoa")]
        public Guid PersonId { get; set; }

        [JsonPropertyName("nome_pessoa")]
        public string PersonName { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("valor_total")]
        public decimal TotalAmount { get; set; }

        [JsonPropertyName("data_criacao")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("data_atualizacao")]
        public DateTime? UpdatedAt { get; set; }

        [JsonPropertyName("produtos")]
        public List<OrderProductDto> Products { get; set; }
    }

    public class OrderProductDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("id_produto")]
        public Guid ProductId { get; set; }

        [JsonPropertyName("nome_produto")]
        public string ProductName { get; set; }

        [JsonPropertyName("preco_unitario")]
        public decimal UnitPrice { get; set; }

        [JsonPropertyName("quantidade")]
        public int Quantity { get; set; }

        [JsonPropertyName("valor_total")]
        public decimal TotalPrice { get; set; }
    }
} 