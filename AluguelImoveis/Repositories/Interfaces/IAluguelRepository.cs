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
        Task<IEnumerable<Aluguel>> ObterAlugueisComDadosAsync();
        Task<IEnumerable<Aluguel>> BuscarPorImovelAsync(Guid imovelId);
    }
}