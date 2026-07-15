using eAgenda.WebApp.ModuloDespesa.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eAgenda.WebApp.Compartilhado.Infra.Orm.Config;

public class DespesaConfiguration : IEntityTypeConfiguration<Despesa>
{
    public void Configure(EntityTypeBuilder<Despesa> builder)
    {
        builder.ToTable("TBDespesa");

        builder.HasKey(d => d.Id)
            .HasName("PK_TBDespesa");

        builder.Property(d => d.Id)
            .ValueGeneratedNever();

        builder.Property(d => d.Descricao)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(d => d.Data)
            .HasColumnType("datetime2")
            .IsRequired();

        builder.Property(d => d.Valor)
            .HasColumnType("decimal(18, 2)")
            .IsRequired();

        builder.Property(d => d.FormaPagamento)
            .HasConversion<int>()
            .IsRequired();

        builder.HasMany(d => d.Categorias)
            .WithMany(c => c.Despesas);
    }
}
