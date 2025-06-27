using System;

namespace AluguelImoveis.Models.DTOs
{
    public class AluguelAtivoDto
    {
        public Guid AluguelId { get; set; }
        public string ImovelEndereco { get; set; }
        public string LocatarioNome { get; set; }
        public int DiasAlugado { get; set; }
        public int TotalDiasContrato { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
    }
}