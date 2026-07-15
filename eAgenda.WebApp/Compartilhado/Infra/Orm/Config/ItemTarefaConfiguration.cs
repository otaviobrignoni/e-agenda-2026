using eAgenda.WebApp.ModuloTarefa.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eAgenda.WebApp.Compartilhado.Infra.Orm.Config;

public class ItemTarefaConfiguration : IEntityTypeConfiguration<ItemTarefa>
{
    public void Configure(EntityTypeBuilder<ItemTarefa> builder)
    {
         builder.ToTable("TBItemTarefa");

        builder.HasKey(i => i.Id)
            .HasName("PK_TBItemTarefa");

        builder.Property(i => i.Id)
            .ValueGeneratedNever();

        builder.Property(i => i.Titulo)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(i => i.EstaConcluido)
            .IsRequired();

        builder.Property<Guid>("TarefaId")
            .IsRequired();
    }
}
