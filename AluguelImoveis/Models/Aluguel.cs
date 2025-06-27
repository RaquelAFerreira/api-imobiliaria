using System.ComponentModel.DataAnnotations.Schema;

namespace AluguelImoveis.Models
{
    public class Aluguel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }

        public Guid ImovelId { get; set; }
        public Imovel Imovel { get; set; }

        public Guid LocatarioId { get; set; }
        public Locatario Locatario { get; set; }
    }
}