using CommitMaster.Sirius.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommitMaster.Sirius.Infra.Data.Mapping
{
    public class AssinaturaMapping : IEntityTypeConfiguration<Assinatura>
    {

        public void Configure(EntityTypeBuilder<Assinatura> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.EstadoAssinatura)
                .HasConversion<int>();

            builder.HasOne(a => a.Aluno)
                .WithOne(a => a.Assinatura);

            builder.HasOne(a => a.Plano);
        }
    }
}
