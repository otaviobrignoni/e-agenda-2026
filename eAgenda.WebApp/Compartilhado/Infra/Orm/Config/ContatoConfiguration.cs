using eAgenda.WebApp.ModuloContato.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eAgenda.WebApp.Compartilhado.Infra.Orm.Config;

public sealed class ContatoConfiguration : IEntityTypeConfiguration<Contato>
{
    public void Configure(EntityTypeBuilder<Contato> builder)
    {
        builder.ToTable("TBContato");

        builder.HasKey(c => c.Id)
            .HasName("PK_TBContato");

        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(c => c.Nome)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.Email)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(c => c.Telefone)
            .HasMaxLength(15)
            .IsRequired();

        builder.Property(c => c.Cargo)
            .HasMaxLength(50);
        
        builder.Property(c => c.Empresa)
            .HasMaxLength(50);

        builder.HasIndex(c => c.Email)
            .IsUnique()
            .HasDatabaseName("UQ_TBContato_Email");

        builder.HasIndex(c => c.Telefone)
            .IsUnique()
            .HasDatabaseName("UQ_TBContato_Telefone");
    }
}
