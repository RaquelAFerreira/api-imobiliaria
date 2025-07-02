using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace AluguelImoveis.Models
{
    public class Aluguel : BaseEntity
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public Guid ImovelId { get; set; }
        public Imovel Imovel { get; set; }
        public Guid LocatarioId { get; set; }
        public Locatario Locatario { get; set; }
    }
}