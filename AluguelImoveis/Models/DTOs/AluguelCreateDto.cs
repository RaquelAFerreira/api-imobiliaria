using System;
using System.ComponentModel.DataAnnotations;

namespace AluguelImoveis.Models.DTOs
{
    public class AluguelCreateDto
    {
        [Required]
        public Guid ImovelId { get; set; }

        [Required]
        public Guid LocatarioId { get; set; }

        [Required]
        public DateTime DataInicio { get; set; }

        [Required]
        public DateTime DataTermino { get; set; }
    }
}