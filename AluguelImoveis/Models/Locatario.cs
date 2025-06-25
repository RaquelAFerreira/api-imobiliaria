namespace AluguelImoveis.Models
{
    public class Locatario
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        
        // public ICollection<Aluguel> Alugueis { get; set; }
    }
}