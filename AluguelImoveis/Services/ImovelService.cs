using AluguelImoveis.Models;
using AluguelImoveis.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AluguelImoveis.Services
{
    public class ImovelService : IImovelService
    {
        private readonly IImovelRepository _repository;
        private readonly IAluguelRepository _aluguelRepository;

        public ImovelService(IImovelRepository repository, IAluguelRepository aluguelRepository)
        {
            _repository = repository;
            _aluguelRepository = aluguelRepository;
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
            if (await _repository.CodigoExistsAsync(imovel.Codigo))
            {
                throw new InvalidOperationException(
                    "Já existe um imóvel com este código cadastrado"
                );
            }

            return await _repository.AddAsync(imovel);
        }

        public async Task UpdateAsync(Imovel imovel)
        {
            var existing = await _repository.GetByIdAsync(imovel.Id);
            if (existing == null)
            {
                throw new KeyNotFoundException("Imóvel não encontrado");
            }

            if (
                existing.Codigo != imovel.Codigo
                && await _repository.CodigoExistsAsync(imovel.Codigo, imovel.Id)
            )
            {
                throw new InvalidOperationException(
                    "Já existe um imóvel com este código cadastrado"
                );
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

            var possuiAlugueis = await _aluguelRepository.ExistsForImovelAsync(id);
            if (possuiAlugueis)
            {
                throw new InvalidOperationException(
                    "Não é possível excluir o imóvel porque ele está vinculado a um ou mais aluguéis."
                );
            }

            await _repository.DeleteAsync(id);
        }


        public async Task<IEnumerable<Imovel>> GetDisponiveisAsync()
        {
            return await _repository.GetDisponiveisAsync();
        }
    }
}
