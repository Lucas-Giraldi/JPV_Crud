using CRUD.Domain.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.Domain.Infra
{
    public class ServerContext : DbContext
    {

        //Configuração do meu contexto e regras da tabela. Criação do objeto Pessoa para acessar direto do meu contexto
        public ServerContext(DbContextOptions<ServerContext> options) : base(options)
        {
        }
        public DbSet<Pessoa> Pessoa { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {

            modelBuilder.Entity<Pessoa>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Pessoa>()
            .HasKey(p => p.Id);

            modelBuilder.Entity<Pessoa>()
                .Property(p => p.Nome)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Pessoa>()
                .HasIndex(p => p.CPF)
                .IsUnique();
        }
    }
}
