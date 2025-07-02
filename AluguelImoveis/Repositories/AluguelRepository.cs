using AluguelImoveis.Data;
using AluguelImoveis.Models;
using AluguelImoveis.Models.DTOs;
using AluguelImoveis.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AluguelImoveis.Repositories
{
    public class AluguelRepository : IAluguelRepository
    {
        private readonly AppDbContext _context;

        public AluguelRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Aluguel> CreateAsync(Aluguel aluguel)
        {
            await _context.Alugueis.AddAsync(aluguel);
            await _context.SaveChangesAsync();
            return aluguel;
        }

        public async Task DeleteAsync(Guid id)
        {
            var aluguel = await _context.Alugueis.FindAsync(id);
            if (aluguel != null)
            {
                _context.Alugueis.Remove(aluguel);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Aluguel>> GetAllAsync()
        {
            return await _context.Alugueis.ToListAsync();
        }

        public async Task<Aluguel> GetByIdAsync(Guid id)
        {
            return await _context.Alugueis.FindAsync(id);
        }
        public async Task UpdateAsync(Aluguel aluguel)
        {
            var tracked = await _context.Alugueis.FindAsync(aluguel.Id);
            if (tracked != null)
            {
                _context.Entry(aluguel).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Aluguel>> GetAllDetailedAsync()
        {
            return await _context.Alugueis
                .Include(a => a.Imovel)
                .Include(a => a.Locatario)
                .ToListAsync();
        }

        public async Task<IEnumerable<Aluguel>> BuscarPorImovelAsync(Guid imovelId)
        {
            return await _context.Alugueis.Where(a => a.ImovelId == imovelId).ToListAsync();
        }
    }
}
