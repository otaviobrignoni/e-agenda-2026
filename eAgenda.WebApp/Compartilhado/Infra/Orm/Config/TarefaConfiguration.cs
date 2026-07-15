using eAgenda.WebApp.ModuloTarefa.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eAgenda.WebApp.Compartilhado.Infra.Orm.Config;

public class TarefaConfiguration : IEntityTypeConfiguration<Tarefa>
{
    public void Configure(EntityTypeBuilder<Tarefa> builder)
    {
        builder.ToTable("TBTarefa");

        builder.HasKey(t => t.Id)
            .HasName("PK_TBTarefa");

        builder.Property(t => t.Id)
            .ValueGeneratedNever();

        builder.Property(t => t.Titulo)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(t => t.Prioridade)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(t => t.DataCriacao)
            .HasColumnType("datetime2")
            .IsRequired();

        builder.Property(t => t.DataConclusao)
            .HasColumnType("datetime2");

        builder.HasMany(t => t.Itens)
            .WithOne(i => i.Tarefa)
            .HasForeignKey("TarefaId")
            .HasConstraintName("FK_TBItemTarefa_TBTarefa")
            .OnDelete(DeleteBehavior.ClientCascade);
    }
}
