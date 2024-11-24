using Microsoft.EntityFrameworkCore;
using SisEngeman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SisEngeman.Data
{
    public class ApplicationDbContext : DbContext
    {
       public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<OrdAnexoModel> OrdAnexo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrdAnexoModel>()
                .HasKey(o => new { o.codEmp, o.codOrd, o.descricao }); // Define a chave primária composta

            modelBuilder.Entity<OrdAnexoModel>()
                .Property(o => o.anexo)
                .HasColumnType("image"); // Define o tipo de coluna para 'anexo'

            modelBuilder.Entity<OrdAnexoModel>()
                .Property(o => o.codEmp)
                .HasColumnType("numeric(18,0)");

            modelBuilder.Entity<OrdAnexoModel>()
                .Property(o => o.codOrd)
                .HasColumnType("numeric(18,0)");

            modelBuilder.Entity<OrdAnexoModel>()
                .Property(o => o.datAlt)
                .HasColumnType("datetime");

            modelBuilder.Entity<OrdAnexoModel>()
                .Property(o => o.descricao)
                .HasColumnType("varchar(100)");

            modelBuilder.Entity<OrdAnexoModel>()
                .Property(o => o.descricaores)
                .HasColumnType("varchar(250)");
        }

    }
}
