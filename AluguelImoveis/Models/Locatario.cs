using System.ComponentModel.DataAnnotations.Schema;

namespace AluguelImoveis.Models
{
    public class Locatario
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string NomeCompleto { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
    }
}