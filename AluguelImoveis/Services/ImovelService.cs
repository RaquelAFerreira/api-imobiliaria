using AluguelImoveis.Models;
using AluguelImoveis.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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
            try
            {
                var existing = await _repository.GetByIdAsync(id);
                if (existing == null)
                {
                    throw new KeyNotFoundException("Imóvel não encontrado");
                }

                await _repository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar excluir o imóvel.", ex);
            }
        }

        public async Task<IEnumerable<Imovel>> ListarDisponiveisAsync()
        {
            return await _repository.ListarDisponiveisAsync();
        }
    }
}
