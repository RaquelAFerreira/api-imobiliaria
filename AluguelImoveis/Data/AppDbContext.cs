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

            // modelBuilder.Entity<Aluguel>(entity =>
            // {
            //     entity.HasKey(a => a.Id);

            //     entity.HasOne(a => a.Imovel)
            //         .WithMany() // ou .WithMany(i => i.Alugueis) se você tiver a coleção
            //         .HasForeignKey(a => a.ImovelId)
            //         .OnDelete(DeleteBehavior.Restrict); // ou Cascade, dependendo do seu domínio

            //     entity.HasOne(a => a.Locatario)
            //         .WithMany()
            //         .HasForeignKey(a => a.LocatarioId)
            //         .OnDelete(DeleteBehavior.Restrict);
            // });
        }
    }
}
