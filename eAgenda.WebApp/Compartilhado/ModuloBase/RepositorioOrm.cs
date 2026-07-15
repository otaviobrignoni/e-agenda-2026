using eAgenda.WebApp.Compartilhado.Infra.Orm;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;

namespace eAgenda.WebApp.Compartilhado.ModuloBase;

public class RepositorioOrm<T>(EAgendaDbContext dbContext) : IRepositorio<T> where T : EntidadeBase<T>
{
    protected readonly DbSet<T> registros = dbContext.Set<T>();
    protected EAgendaDbContext DbContext { get; } = dbContext;

    public bool Cadastrar(T registro)
    {
        try
        {
            registros.Add(registro);
            DbContext.SaveChanges();
        }
        catch
        {
            return false;
        }
        return true;
    }

    public bool Editar(Guid id, T registroEditado)
    {
        var registro = Selecionar(id);

        if (registro is null)
            return false;

        registro.Atualizar(registroEditado);

        try
        {
            DbContext.SaveChanges();
        }
        catch
        {
            return false;
        }
        return true;
    }

    public bool Excluir(Guid id)
    {
        var registro = Selecionar(id);

        if (registro is null)
            return false;

        try
        {
            registros.Remove(registro);
            DbContext.SaveChanges();
        }
        catch
        {
            return false;
        }

        return true;
    }

    public virtual T? Selecionar(Guid id) => registros.SingleOrDefault(r => r.Id == id);

    public virtual List<T> Selecionar(Func<T, bool>? filtro = null) => [.. registros.Where(filtro ?? (_ => true))];
}
