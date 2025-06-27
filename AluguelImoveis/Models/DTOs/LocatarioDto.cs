using System.ComponentModel.DataAnnotations;

namespace AluguelImoveis.Models.DTOs
{
    public class LocatarioDto
    {
        public string NomeCompleto { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
    }
}