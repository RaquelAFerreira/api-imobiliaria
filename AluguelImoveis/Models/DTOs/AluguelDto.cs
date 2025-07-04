namespace AluguelImoveis.Models.DTOs
{
    public class AluguelDto
    {
        public Guid AluguelId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public int TotalDias { get; set; }
        public int DiasRestantes { get; set; }
        public ImovelDto Imovel { get; set; } = new ImovelDto();
        public LocatarioDto Locatario { get; set; } = new LocatarioDto();
    }
}