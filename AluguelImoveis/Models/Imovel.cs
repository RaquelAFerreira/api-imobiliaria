using System.ComponentModel.DataAnnotations.Schema;
using AluguelImoveis.Models.Enums;

namespace AluguelImoveis.Models
{
    public class Imovel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public TipoImovel Tipo { get; set; }
        public string Endereco { get; set; } = String.Empty;
        public decimal ValorLocacao { get; set; }
        public bool Disponivel { get; set; } = true;

        // public ICollection<Aluguel> Alugueis { get; set; }
    }
}