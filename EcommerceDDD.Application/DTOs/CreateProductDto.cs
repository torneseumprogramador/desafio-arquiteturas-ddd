using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EcommerceDDD.Application.DTOs
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(200, ErrorMessage = "Nome deve ter no máximo 200 caracteres")]
        [JsonPropertyName("nome")]
        public string Name { get; set; }

        [StringLength(1000, ErrorMessage = "Descrição deve ter no máximo 1000 caracteres")]
        [JsonPropertyName("descricao")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Preço é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Preço deve ser maior que zero")]
        [JsonPropertyName("preco")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Estoque é obrigatório")]
        [Range(0, int.MaxValue, ErrorMessage = "Estoque deve ser maior ou igual a zero")]
        [JsonPropertyName("estoque")]
        public int Stock { get; set; }
    }
} 