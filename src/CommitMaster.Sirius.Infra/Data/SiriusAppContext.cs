using System.Linq;
using CommitMaster.Sirius.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommitMaster.Sirius.Infra.Data
{
    public class SiriusAppContext : DbContext
    {
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Plano> Planos { get; set; }
        public DbSet<Assinatura> Assinaturas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public SiriusAppContext(DbContextOptions options) : base(options){ }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SiriusAppContext).Assembly);

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                         e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(200)");
            
            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                         .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.SetNull;

            
            base.OnModelCreating(modelBuilder);
        }
    
    }
}
