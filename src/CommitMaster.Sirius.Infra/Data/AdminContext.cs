using CommitMaster.Sirius.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommitMaster.Sirius.Infra.Data
{
    public class AdminContext : DbContext
    {
        public DbSet<Plano> Planos { get; set; }

        public AdminContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(SiriusAppContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

    }
}
