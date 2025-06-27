public class ImovelDto
{
    public string Tipo { get; set; } = string.Empty;
    public string Endereco { get; set; } = string.Empty;
    public decimal ValorLocacao { get; set; }
    public bool Disponivel { get; set; } = true;
    public string Codigo { get; set; } = string.Empty;
}