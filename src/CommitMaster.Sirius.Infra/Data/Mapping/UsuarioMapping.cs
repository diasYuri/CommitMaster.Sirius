using CommitMaster.Sirius.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommitMaster.Sirius.Infra.Data.Mapping
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {

        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(p => p.Email)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.HasIndex(p => p.Email)
                .IsUnique();

            builder.Property(p => p.Senha)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(p => p.DataExpiracao)
                .IsRequired();


            builder.HasOne(a => a.Aluno);

            builder.Property(a => a.CreatedAt)
                .IsRequired();
        }
    }
}
