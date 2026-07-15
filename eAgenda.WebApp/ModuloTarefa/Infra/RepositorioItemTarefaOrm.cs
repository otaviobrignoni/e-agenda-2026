using eAgenda.WebApp.Compartilhado.Infra.Orm;
using eAgenda.WebApp.Compartilhado.ModuloBase;
using eAgenda.WebApp.ModuloTarefa.Dominio;

namespace eAgenda.WebApp.ModuloTarefa.Infra;

public class RepositorioItemTarefaOrm(EAgendaDbContext dbContext) : RepositorioOrm<ItemTarefa>(dbContext), IRepositorioItemTarefa
{
    public List<ItemTarefa> Selecionar(Tarefa tarefa)
    {
        return [.. registros
            .Where(i => i.Tarefa.Id == tarefa.Id)
            .OrderBy(i => i.Titulo)
        ];
    }

    public bool Excluir(ItemTarefa item)
    {
        try
        {
            registros.Remove(item);
            DbContext.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool Editar(IEnumerable<ItemTarefa> itensExcluidos, IEnumerable<ItemTarefa> itensAdicionados, IEnumerable<ItemTarefa> itensEditados)
    {
        try
        {
            registros.RemoveRange(itensExcluidos);
            registros.AddRange(itensAdicionados);

            foreach (var item in itensEditados)
                DbContext.Entry(item).Property(i => i.EstaConcluido).IsModified = true;

            DbContext.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }
}
