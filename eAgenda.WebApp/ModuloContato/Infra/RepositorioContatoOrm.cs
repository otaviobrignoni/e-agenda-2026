using eAgenda.WebApp.Compartilhado.Infra.Orm;
using eAgenda.WebApp.Compartilhado.ModuloBase;
using eAgenda.WebApp.ModuloContato.Dominio;

namespace eAgenda.WebApp.ModuloContato.Infra;

public class RepositorioContatoOrm(EAgendaDbContext dbContext) : RepositorioOrm<Contato>(dbContext), IRepositorioContato
{
    public bool PossuiCompromissos(Guid id) => DbContext.Compromissos.Any(c => c.Contato != null && c.Contato.Id == id);
}
