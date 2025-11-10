using Microsoft.EntityFrameworkCore;
using SalaReuniao.Api.Infrastructure.Entities;

namespace SalaReuniao.Api.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<ResponsavelEntity> Responsaveis => Set<ResponsavelEntity>();
        public DbSet<ServicoEntity> Servicos => Set<ServicoEntity>();
        public DbSet<SalaDeReuniaoEntity> Salas => Set<SalaDeReuniaoEntity>();
        public DbSet<SalaServicoOferecidoEntity> ServicosOferecidos => Set<SalaServicoOferecidoEntity>();
        public DbSet<ReuniaoAgendadaEntity> Reunioes => Set<ReuniaoAgendadaEntity>();
        public DbSet<ServicoAgendadoEntity> ServicosAgendados => Set<ServicoAgendadoEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Responsavel -> Salas
            modelBuilder.Entity<ResponsavelEntity>()
                .HasMany(r => r.Salas)
                .WithOne(s => s.Responsavel)
                .HasForeignKey(s => s.IdResponsavel);

            // Responsavel -> Servicos
            modelBuilder.Entity<ResponsavelEntity>()
                .HasMany(r => r.ServicosCadastrados)
                .WithOne(s => s.Responsavel)
                .HasForeignKey(s => s.IdResponsavel);

            // Sala -> Reuniões
            modelBuilder.Entity<SalaDeReuniaoEntity>()
                .HasMany(s => s.ReunioesAgendadas)
                .WithOne(r => r.SalaReuniao)
                .HasForeignKey(r => r.IdSalaReuniao);

            // Sala -> Serviços oferecidos
            modelBuilder.Entity<SalaDeReuniaoEntity>()
                .HasMany(s => s.ServicosOferecidos)
                .WithOne(ss => ss.Sala)
                .HasForeignKey(ss => ss.IdSala);

            // Serviço -> Serviços oferecidos
            modelBuilder.Entity<ServicoEntity>()
                .HasMany(s => s.ServicosOferecidos)
                .WithOne(ss => ss.Servico)
                .HasForeignKey(ss => ss.IdServico);

            // Serviço oferecido -> Serviço agendado
            modelBuilder.Entity<SalaServicoOferecidoEntity>()
                .HasMany(ss => ss.ServicosAgendados)
                .WithOne(sa => sa.SalaServicoOferecido)
                .HasForeignKey(sa => sa.IdSalaServicoOferecido);

            // Reunião -> Serviços agendados
            modelBuilder.Entity<ReuniaoAgendadaEntity>()
                .HasMany(r => r.ServicosAgendados)
                .WithOne(sa => sa.ReuniaoAgendada)
                .HasForeignKey(sa => sa.IdReuniaoAgendada);
        }
    }
}
