using AluguelImoveis.Models;

namespace AluguelImoveis.Repositories.Interfaces
{
    public interface IImovelRepository
    {
        Task<IEnumerable<Imovel>> GetAllAsync();
        Task<Imovel> GetByIdAsync(Guid id);
        Task<Imovel> AddAsync(Imovel imovel);
        Task UpdateAsync(Imovel imovel);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Imovel>> ListarDisponiveisAsync();
    }
}