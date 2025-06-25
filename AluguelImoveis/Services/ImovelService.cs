using AluguelImoveis.Models;
using AluguelImoveis.Repositories.Interfaces;

namespace AluguelImoveis.Services
{
    public class ImovelService
    {
        private readonly IImovelRepository _imovelRepository;

        public ImovelService(IImovelRepository imovelRepository)
        {
            _imovelRepository = imovelRepository;
        }

        public async Task<IEnumerable<Imovel>> GetAllImoveisAsync()
        {
            return await _imovelRepository.GetAllAsync();
        }

        public async Task<Imovel> GetImovelByIdAsync(Guid id)
        {
            return await _imovelRepository.GetByIdAsync(id);
        }

        public async Task<Imovel> AddImovelAsync(Imovel imovel)
        {
            return await _imovelRepository.AddAsync(imovel);
        }

        public async Task UpdateImovelAsync(Imovel imovel)
        {
            await _imovelRepository.UpdateAsync(imovel);
        }

        public async Task DeleteImovelAsync(Guid id)
        {
            await _imovelRepository.DeleteAsync(id);
        }
    }
}