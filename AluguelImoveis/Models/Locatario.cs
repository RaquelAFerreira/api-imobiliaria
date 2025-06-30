using System.ComponentModel.DataAnnotations.Schema;

namespace AluguelImoveis.Models
{
    public class Locatario : BaseEntity
    {
        public string NomeCompleto { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
    }
}