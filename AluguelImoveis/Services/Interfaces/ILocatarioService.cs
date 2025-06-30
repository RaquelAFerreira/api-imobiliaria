using AluguelImoveis.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AluguelImoveis.Services
{
    public interface ILocatarioService
    {
        Task<IEnumerable<Locatario>> GetAllAsync();
        Task<Locatario?> GetByIdAsync(Guid id);
        Task<Locatario> CreateAsync(Locatario locatario);
        Task UpdateAsync(Locatario locatario);
        Task DeleteAsync(Guid id);
    }
}