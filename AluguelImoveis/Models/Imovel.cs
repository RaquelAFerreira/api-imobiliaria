using System.ComponentModel.DataAnnotations.Schema;

namespace AluguelImoveis.Models
{
    public class Imovel : BaseEntity
    {
        public string Tipo { get; set; } = String.Empty;
        public string Endereco { get; set; } = String.Empty;
        public decimal ValorLocacao { get; set; }
        public bool Disponivel { get; set; } = true;
        public string Codigo { get; set; } = String.Empty;
    }
}