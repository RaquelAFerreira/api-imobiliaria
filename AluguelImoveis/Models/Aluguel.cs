namespace AluguelImoveis.Models
{
    public class Aluguel
    {
        public int Id { get; set; }
        public int ImovelId { get; set; }
        public int LocatarioId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
    }
}