using eAgenda.WebApp.Compartilhado.Infra.Orm.Config;
using eAgenda.WebApp.ModuloCategoria.Dominio;
using eAgenda.WebApp.ModuloCompromisso.Dominio;
using eAgenda.WebApp.ModuloContato.Dominio;
using eAgenda.WebApp.ModuloDespesa.Dominio;
using eAgenda.WebApp.ModuloTarefa.Dominio;
using Microsoft.EntityFrameworkCore;

namespace eAgenda.WebApp.Compartilhado.Infra.Orm;

public class EAgendaDbContext(DbContextOptions<EAgendaDbContext> options) : DbContext(options)
{
    public DbSet<Contato> Contatos => Set<Contato>();
    public DbSet<Compromisso> Compromissos => Set<Compromisso>();
    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Despesa> Despesas => Set<Despesa>();
    public DbSet<ItemTarefa> ItensTarefa => Set<ItemTarefa>();
    public DbSet<Tarefa> Tarefas => Set<Tarefa>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EAgendaDbContext).Assembly);
    }
}
