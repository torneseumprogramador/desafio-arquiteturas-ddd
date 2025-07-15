using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EcommerceDDD.Application.DTOs
{
    public class CreateOrderDto
    {
        [Required(ErrorMessage = "ID da pessoa é obrigatório")]
        [JsonPropertyName("id_pessoa")]
        public Guid PersonId { get; set; }

        [Required(ErrorMessage = "Produtos são obrigatórios")]
        [MinLength(1, ErrorMessage = "Pedido deve ter pelo menos um produto")]
        [JsonPropertyName("produtos")]
        public List<CreateOrderProductDto> Products { get; set; }
    }

    public class CreateOrderProductDto
    {
        [Required(ErrorMessage = "ID do produto é obrigatório")]
        [JsonPropertyName("id_produto")]
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "Quantidade é obrigatória")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantidade deve ser maior que zero")]
        [JsonPropertyName("quantidade")]
        public int Quantity { get; set; }
    }
} 