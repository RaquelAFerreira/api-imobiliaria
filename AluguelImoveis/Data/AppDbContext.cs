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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Imovel>().Property(i => i.Tipo).HasConversion<string>();

            // modelBuilder.Entity<Aluguel>()
            //     .HasOne(a => a.Imovel)
            //     .WithMany(i => i.Alugueis)
            //     .HasForeignKey(a => a.ImovelId);

            // modelBuilder.Entity<Aluguel>()
            //     .HasOne(a => a.Locatario)
            //     .WithMany(l => l.Alugueis)
            //     .HasForeignKey(a => a.LocatarioId);
        }
    }
}
