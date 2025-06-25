using System.ComponentModel.DataAnnotations;
using AluguelImoveis.Models.Enums;

public class ImovelCreateDto
{
    [Required(ErrorMessage = "O tipo do imóvel é obrigatório")]
    public TipoImovel Tipo { get; set; }

    [Required(ErrorMessage = "O endereço é obrigatório")]
    [StringLength(200, ErrorMessage = "O endereço não pode exceder 200 caracteres")]
    public string Endereco { get; set; } = string.Empty;

    [Required(ErrorMessage = "O valor da locação é obrigatório")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
    public decimal ValorLocacao { get; set; }

    public bool Disponivel { get; set; } = true;
}