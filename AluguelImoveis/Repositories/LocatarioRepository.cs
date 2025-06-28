using AluguelImoveis.Data;
using AluguelImoveis.Models;
using AluguelImoveis.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AluguelImoveis.Repositories
{
    public class LocatarioRepository : ILocatarioRepository
    {
        private readonly AppDbContext _context;

        public LocatarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Locatario>> GetAllAsync()
        {
            return await _context.Locatarios.ToListAsync();
        }

        public async Task<Locatario?> GetByIdAsync(Guid id)
        {
            return await _context.Locatarios.FindAsync(id);
        }

        public async Task<Locatario> AddAsync(Locatario locatario)
        {
            await _context.Locatarios.AddAsync(locatario);
            await _context.SaveChangesAsync();
            return locatario;
        }

        public async Task UpdateAsync(Locatario locatario)
        {
            var tracked = await _context.Locatarios.FindAsync(locatario.Id);
            if (tracked != null)
            {
                _context.Entry(locatario).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var locatario = await _context.Locatarios.FindAsync(id);
            if (locatario != null)
            {
                _context.Locatarios.Remove(locatario);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> CpfExistsAsync(string cpf)
        {
            return await _context.Locatarios.AnyAsync(l => l.CPF == cpf);
        }
    }
}