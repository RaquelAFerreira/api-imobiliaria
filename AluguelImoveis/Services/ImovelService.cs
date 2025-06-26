using AluguelImoveis.Models;
using AluguelImoveis.Repositories.Interfaces;

namespace AluguelImoveis.Services
{
    public class ImovelService
    {
        private readonly IImovelRepository _repository;

        public ImovelService(IImovelRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Imovel>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Imovel?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Imovel> CreateAsync(Imovel imovel)
        {
            return await _repository.AddAsync(imovel);
        }

        public async Task UpdateAsync(Imovel imovel)
        {
            var existing = await _repository.GetByIdAsync(imovel.Id);
            if (existing == null)
            {
                throw new KeyNotFoundException("Imóvel não encontrado");
            }

            await _repository.UpdateAsync(imovel);
        }

        public async Task DeleteAsync(Guid id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
            {
                throw new KeyNotFoundException("Imóvel não encontrado");
            }

            await _repository.DeleteAsync(id);
        }
    }
}
