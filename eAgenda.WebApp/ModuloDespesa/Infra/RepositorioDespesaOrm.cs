using eAgenda.WebApp.Compartilhado.Infra.Orm;
using eAgenda.WebApp.Compartilhado.ModuloBase;
using eAgenda.WebApp.ModuloDespesa.Dominio;
using Microsoft.EntityFrameworkCore;

namespace eAgenda.WebApp.ModuloDespesa.Infra;

public class RepositorioDespesaOrm(EAgendaDbContext dbContext) : RepositorioOrm<Despesa>(dbContext), IRepositorioDespesa
{
    public override Despesa? Selecionar(Guid id)
    {
        return registros
            .Include(d => d.Categorias)
            .ThenInclude(c => c.Despesas)
            .SingleOrDefault(d => d.Id == id);
    }
    public override List<Despesa> Selecionar(Func<Despesa, bool>? filtro = null)
    {
        return [.. registros
            .Include(d=> d.Categorias)
            .ThenInclude(c => c.Despesas)
            .Where(filtro ?? (_ => true))
            .OrderByDescending(d => d.Data)
            .ThenBy(d => d.Descricao)
        ];
    }
}
