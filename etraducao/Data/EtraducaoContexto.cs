using System;
using etraducao.Models.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace etraducao.Data
{
    public class EtraducaoContexto : DbContext
    {
        private readonly IConfiguration configuration;

        public EtraducaoContexto(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.Database.SetCommandTimeout(100);
        }

        public DbSet<Cliente> Cliente { get; set; }

        public DbSet<ControleDeValores> ControleDeValores { get; set; }

        public DbSet<Documento> Documento { get; set; }

        public DbSet<Pagamento> Pagamento { get; set; }

        public DbSet<Solicitacao> Solicitacao { get; set; }

        public DbSet<Tradutor> Tradutor { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ControleDeValores>().Ignore(x => x.CoefSoma);
            modelBuilder.Entity<ControleDeValores>().Ignore(x => x.CoefReal);
            modelBuilder.Entity<ControleDeValores>().Ignore(x => x.CoefInterno);
            modelBuilder.Entity<ControleDeValores>().Ignore(x => x.DeadLineReal);
            modelBuilder.Entity<ControleDeValores>().Ignore(x => x.DeadLineSugerido);
            modelBuilder.Entity<ControleDeValores>().Ignore(x => x.QuantidadeDeLaudas);
            modelBuilder.Entity<ControleDeValores>().Ignore(x => x.QuantidadeDeDiasParaEntrega);

            modelBuilder.Entity<Cliente>().OwnsOne(x => x.Cnpj)
                .Property(x => x.Codigo)
                .HasColumnName("Cnpj");

            modelBuilder.Entity<Cliente>()
                .HasIndex(x => x.Email)
                .HasName("Unique Email")
                .IsUnique();

            modelBuilder.Entity<Cliente>().OwnsOne(x => x.Cpf)
                .Property(x => x.Codigo)
                .HasColumnName("Cpf");

            modelBuilder.Entity<Pagamento>().HasOne(x => x.Solicitacao)
                .WithOne(x => x.Pagamento)
                .HasForeignKey<Pagamento>(x => x.SolicitacaoId);

            modelBuilder.Entity <Solicitacao>().HasOne(x => x.Tradutor)
                .WithMany(x => x.Solicitacoes)
                .HasForeignKey(x => x.TradutorId);

            modelBuilder.Entity<Solicitacao>()
                .Property(e => e.TipoDeSolicitacao)
                .HasConversion(v => v.ToString(), v => (TiposDeSolicitacao)Enum.Parse(typeof(TiposDeSolicitacao), v));

            modelBuilder.Entity<Pagamento>()
                .Property(e => e.FormaDePagamento)
                .HasConversion(v => v.ToString(), v => (FormaDePagamento)Enum.Parse(typeof(FormaDePagamento), v));

            modelBuilder.Entity<Pagamento>()
                .Property(e => e.StatusDePagamento)
                .HasConversion(v => v.ToString(), v => (StatusDePagamento)Enum.Parse(typeof(StatusDePagamento), v));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("EtraducaoContexto"));
            base.OnConfiguring(optionsBuilder);
        }
    }
}
