using AluguelImoveis.Models;
using Microsoft.EntityFrameworkCore;

namespace AluguelImoveis.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Imovel> Imoveis { get; set; }
        public DbSet<Locatario> Locatarios { get; set; }
        public DbSet<Aluguel> Alugueis { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { }
    }
}
