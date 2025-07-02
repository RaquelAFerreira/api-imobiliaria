using AluguelImoveis.Models;
using AluguelImoveis.Data;
using AluguelImoveis.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace AluguelImoveis.Repositories
{
    public class ImovelRepository : IImovelRepository
    {
        private readonly AppDbContext _context;

        public ImovelRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Imovel>> GetAllAsync()
        {
            return await _context.Imoveis.ToListAsync();
        }

        public async Task<Imovel> GetByIdAsync(Guid id)
        {
            return await _context.Imoveis.FindAsync(id);
        }

        public async Task<Imovel> AddAsync(Imovel imovel)
        {
            _context.Imoveis.Add(imovel);
            await _context.SaveChangesAsync();
            return imovel;
        }

        public async Task UpdateAsync(Imovel imovel)
        {
            var tracked = await _context.Imoveis.FindAsync(imovel.Id);
            if (tracked != null)
            {
                _context.Entry(tracked).CurrentValues.SetValues(imovel);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var imovel = await _context.Imoveis.FindAsync(id);
            if (imovel != null)
            {
                _context.Imoveis.Remove(imovel);
                await _context.SaveChangesAsync();
            }

        }

        public async Task<IEnumerable<Imovel>> GetDisponiveisAsync()
        {
            var hoje = DateTime.Today;

            return await _context.Imoveis
                .Where(i => i.Disponivel)
                .Where(
                    i => !_context.Alugueis.Any(a => a.ImovelId == i.Id && a.DataTermino >= hoje)
                )
                .ToListAsync();
        }

        public async Task<bool> CodigoExistsAsync(string codigo, Guid? ignoreId = null)
        {
            var query = _context.Imoveis.Where(i => i.Codigo == codigo);

            if (ignoreId.HasValue)
            {
                query = query.Where(i => i.Id != ignoreId.Value);
            }

            return await query.AnyAsync();
        }
    }
}
