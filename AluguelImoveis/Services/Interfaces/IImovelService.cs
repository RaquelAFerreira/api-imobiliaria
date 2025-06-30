using AluguelImoveis.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AluguelImoveis.Services
{
    public interface IImovelService
    {
        Task<IEnumerable<Imovel>> GetAllAsync();
        Task<Imovel?> GetByIdAsync(Guid id);
        Task<Imovel> CreateAsync(Imovel imovel);
        Task UpdateAsync(Imovel imovel);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Imovel>> ListarDisponiveisAsync();
    }
}