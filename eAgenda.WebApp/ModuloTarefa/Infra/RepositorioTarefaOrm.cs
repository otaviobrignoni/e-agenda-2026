using eAgenda.WebApp.Compartilhado.Infra.Orm;
using eAgenda.WebApp.Compartilhado.ModuloBase;
using eAgenda.WebApp.ModuloTarefa.Dominio;
using Microsoft.EntityFrameworkCore;

namespace eAgenda.WebApp.ModuloTarefa.Infra;

public class RepositorioTarefaOrm(EAgendaDbContext dbContext) : RepositorioOrm<Tarefa>(dbContext), IRepositorioTarefa
{
    public bool AtualizarDataConclusao(Tarefa tarefa)
    {
        try
        {
            tarefa.AtualizarDataConclusao();
            DbContext.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }
    public override Tarefa? Selecionar(Guid id)
    {
        return registros
            .Include(t => t.Itens)
            .SingleOrDefault(t => t.Id == id);
    }
    public override List<Tarefa> Selecionar(Func<Tarefa, bool>? filtro = null)
    {
        return [.. registros
            .Include(t => t.Itens)
            .Where(filtro ?? (_ => true))
            .OrderBy(t => t.DataCriacao)
            .ThenBy(t => t.Titulo)
            .ThenBy(t => t.PercentualConcluido)
        ];
    }
}
