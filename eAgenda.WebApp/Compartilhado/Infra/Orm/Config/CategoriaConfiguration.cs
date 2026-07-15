using eAgenda.WebApp.ModuloCategoria.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eAgenda.WebApp.Compartilhado.Infra.Orm.Config;

public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.ToTable("TBCategoria");

        builder.HasKey(c=> c.Id)
            .HasName("PK_TBCategoria");

        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(c => c.Titulo)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(c => c.Titulo)
            .IsUnique()
            .HasDatabaseName("UQ_TBCategoria_Titulo");
    }
}
