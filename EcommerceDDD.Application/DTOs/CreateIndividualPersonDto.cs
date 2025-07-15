using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EcommerceDDD.Application.DTOs
{
    public class CreateIndividualPersonDto
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(200, ErrorMessage = "Nome deve ter no máximo 200 caracteres")]
        [JsonPropertyName("nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [StringLength(200, ErrorMessage = "Email deve ter no máximo 200 caracteres")]
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Senha deve ter entre 6 e 100 caracteres")]
        [JsonPropertyName("senha")]
        public string Password { get; set; }

        [Required(ErrorMessage = "CPF é obrigatório")]
        [StringLength(14, ErrorMessage = "CPF deve ter 14 caracteres")]
        [JsonPropertyName("cpf")]
        public string CPF { get; set; }
    }
} 