using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public class ProAgilContext: DbContext
    {
        public ProAgilContext (DbContextOptions<ProAgilContext> options): base(options){}
        
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Orador> Oradores { get; set; }
        public DbSet<OradorEvento> OradoresEventos { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<RedeSocial> RedesSociais { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OradorEvento>()
                .HasKey(OE => new { OE.EventoId, OE.OradorId});
        }
    }
}