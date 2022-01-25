using CommitMaster.Sirius.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommitMaster.Sirius.Infra.Data.Mapping
{
    public class AlunoMapping : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Nome)
                .IsRequired()
                .HasColumnType("varchar(100)");
            
            builder.Property(p => p.Email)
                .IsRequired()
                .HasColumnType("varchar(100)");
            
            builder.HasIndex(p => p.Email)
                .IsUnique();
            
            builder.OwnsOne(p => p.Cpf)
                .Property(p => p.Numero)
                .HasColumnName("cpf")
                .IsRequired()
                .HasColumnType("varchar(20)");
            
            builder.OwnsOne(p => p.Telefone)
                .Property(p => p.Numero)
                .HasColumnName("numero_telefone")
                .IsRequired()
                .HasColumnType("varchar(20)");
            
            builder.Property(a => a.DataAniversario)
                .IsRequired();

            builder.HasOne(a => a.Assinatura)
                .WithOne(a => a.Aluno)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Property(a => a.CreatedAt)
                .IsRequired();
        }
    }
}