using AluguelImoveis.Models;
using AluguelImoveis.Repositories.Interfaces;

namespace AluguelImoveis.Services
{
    public class LocatarioService 
    {
        private readonly ILocatarioRepository _repository;

        public LocatarioService(ILocatarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Locatario>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Locatario?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Locatario> CreateAsync(Locatario locatario)
        {
            if (await _repository.CpfExistsAsync(locatario.CPF))
            {
                throw new InvalidOperationException("Já existe um locatário com este CPF");
            }

            return await _repository.AddAsync(locatario);
        }

        public async Task UpdateAsync(Locatario locatario)
        {
            var existing = await _repository.GetByIdAsync(locatario.Id);
            if (existing == null)
            {
                throw new KeyNotFoundException("Locatário não encontrado");
            }

            if (existing.CPF != locatario.CPF && await _repository.CpfExistsAsync(locatario.CPF))
            {
                throw new InvalidOperationException("Já existe um locatário com este CPF");
            }

            await _repository.UpdateAsync(locatario);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}