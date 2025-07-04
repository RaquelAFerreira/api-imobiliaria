using System.ComponentModel.DataAnnotations;

namespace AluguelImoveis.Models.DTOs
{
    public class LocatarioDto
    {
        public string NomeCompleto { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
    }
}