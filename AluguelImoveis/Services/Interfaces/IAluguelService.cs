using AluguelImoveis.Models;
using AluguelImoveis.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AluguelImoveis.Services.Interfaces
{
    public interface IAluguelService
    {
        Task<IEnumerable<Aluguel>> GetAllAsync();
        Task<Aluguel> GetByIdAsync(Guid id);
        Task<Aluguel> CreateAsync(Aluguel aluguel);
        Task UpdateAsync(Aluguel aluguel);
        Task DeleteAsync(Guid id);
        Task<List<AluguelDetalhadoDto>> ObterAlugueisDetalhadosAsync();
    }
}