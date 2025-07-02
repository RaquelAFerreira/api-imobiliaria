using AluguelImoveis.Models;
using AluguelImoveis.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AluguelImoveis.Repositories.Interfaces
{
    public interface IAluguelRepository
    {
        Task<IEnumerable<Aluguel>> GetAllAsync();
        Task<Aluguel> GetByIdAsync(Guid id);
        Task<Aluguel> CreateAsync(Aluguel aluguel);
        Task UpdateAsync(Aluguel aluguel);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Aluguel>> GetAllDetailedAsync();
        Task<IEnumerable<Aluguel>> GetByImovelAsync(Guid imovelId);
        Task<bool> ExistsForImovelAsync(Guid imovelId);
        Task<bool> ExistsForLocatarioAsync(Guid locatarioId);
    }
}
