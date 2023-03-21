using Microsoft.EntityFrameworkCore;
using AttAnalise.Models;

namespace AttAnalise.Context
{
    public class LojaContext : DbContext
    {
        public LojaContext(DbContextOptions<LojaContext> options) : base (options)
        {
            //
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Administrador> Administradores { get; set;}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<Administrador>()
                .HasKey(u => u.Id);
        }
    }
}