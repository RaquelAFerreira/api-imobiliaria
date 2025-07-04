using System.ComponentModel.DataAnnotations;

namespace AluguelImoveis.Models.DTOs
{
    public class LocatarioCreateDto
    {
        [Required(ErrorMessage = "Nome completo é obrigatório")]
        [StringLength(100, ErrorMessage = "Nome não pode exceder 100 caracteres")] //DEV colocar no WPF
        public string NomeCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "CPF é obrigatório")]
        [CPF(ErrorMessage = "CPF inválido.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "CPF deve ter 11 caracteres")]
        public string CPF { get; set; } = string.Empty;

        [Required(ErrorMessage = "Telefone é obrigatório")]
        [StringLength(20, ErrorMessage = "Telefone não pode exceder 20 caracteres")]
        public string Telefone { get; set; } = string.Empty;
    }
}