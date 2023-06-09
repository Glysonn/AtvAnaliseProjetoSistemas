using Microsoft.EntityFrameworkCore;
using AttAnalise.Models;

namespace AttAnalise.Context
{
    public class LojaContext : DbContext
    {
        public LojaContext(DbContextOptions<LojaContext> options) : base (options)
        {
            // administrador padrao pra att. o banco já se inicia com ele cadastrado
            if(!this.Administradores.Any())
            {
                Administrador admPadrao = new Administrador("Glyson", "kauaglyson@gmail.com", "ueblers");
                this.Administradores.Add(admPadrao);
                this.SaveChanges();
            }
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Administrador> Administradores { get; set;}
        public DbSet<Periferico> Perifericos { get; set; }
        public DbSet<Peca> Pecas { get; set; }
        public DbSet<Acessorio> Acessorios { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // HERDAM DE USUARIO
            modelBuilder.Entity<Cliente>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<Administrador>()
                .HasKey(u => u.Id);
            
            // HERDAM DE PRODUTO
            modelBuilder.Entity<Peca>()
                .HasKey(u => u.Codigo);

            modelBuilder.Entity<Periferico>()
                .HasKey(u => u.Codigo);

            modelBuilder.Entity<Acessorio>()
                .HasKey(u => u.Codigo);
        }
    }
}