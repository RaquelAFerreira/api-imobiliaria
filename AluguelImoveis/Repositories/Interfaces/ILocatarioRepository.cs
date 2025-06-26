using AluguelImoveis.Models;

namespace AluguelImoveis.Repositories.Interfaces
{
    public interface ILocatarioRepository
    {
        Task<IEnumerable<Locatario>> GetAllAsync();
        Task<Locatario?> GetByIdAsync(Guid id);
        Task<Locatario> AddAsync(Locatario locatario);
        Task UpdateAsync(Locatario locatario);
        Task DeleteAsync(Guid id);
        Task<bool> CpfExistsAsync(string cpf);
    }
}