using eAgenda.WebApp.ModuloContato.Dominio;
using FluentResults;

namespace eAgenda.WebApp.ModuloContato.Aplicacao;

public class ServicoContato(IRepositorioContato repositorioContato)
{
    public Result Cadastrar(ContatoDto dto)
    {
        string textoFalha = "Já existe um contato com esse ";
        var contatos = repositorioContato.Selecionar();
        var falhas = new List<IError>();

        if (contatos.Any(c => c.Email == dto.Email))
            falhas.Add(ErroDeCampo(nameof(dto.Email), textoFalha + "email"));

        if (contatos.Any(c => c.Telefone == dto.Telefone))
            falhas.Add(ErroDeCampo(nameof(dto.Telefone), textoFalha + "telefone"));

        if (falhas.Count > 0)
            return Result.Fail(falhas);

        var contato = new Contato(dto.Nome, dto.Email, dto.Telefone, dto.Cargo, dto.Empresa);
        if (!repositorioContato.Cadastrar(contato))
            return Result.Fail("Não foi possível cadastrar o contato.");

        return Result.Ok();
    }

    public Result Editar(ContatoDto dto)
    {
        string textoFalha = "Já existe um contato com esse ";
        var outrosContatos = repositorioContato.Selecionar(c => dto.Id != c.Id);
        var falhas = new List<IError>();

        if (outrosContatos.Any(c => c.Email == dto.Email))
            falhas.Add(ErroDeCampo(nameof(dto.Email), textoFalha + "email"));

        if (outrosContatos.Any(c => c.Telefone == dto.Telefone))
            falhas.Add(ErroDeCampo(nameof(dto.Telefone), textoFalha + "telefone"));

        if (falhas.Count > 0)
            return Result.Fail(falhas);

        var contatoEditado = new Contato(dto.Nome, dto.Email, dto.Telefone, dto.Cargo, dto.Empresa);
        if (!repositorioContato.Editar(dto.Id, contatoEditado))
            return Result.Fail("Contato não encontrado.");
        return Result.Ok();
    }
    public Result Excluir(Guid id)
    {
        if (!repositorioContato.Excluir(id))
            return Result.Fail("Contato não encontrado.");
        return Result.Ok();
    }

    public Result<ContatoDto> Selecionar(Guid id)
    {
        var contato = repositorioContato.Selecionar(id);
        if (contato is null)
            return Result.Fail("Contato não encontrado.");
        return Result.Ok(new ContatoDto(contato.Nome, contato.Email, contato.Telefone, contato.Cargo, contato.Empresa, contato.Id));
    }

    public List<ContatoDto> Selecionar()
    {
        return repositorioContato.Registros.Select(t =>
        {
            return new ContatoDto(t.Nome, t.Email, t.Telefone, t.Cargo, t.Empresa, t.Id);
        }).ToList();
    }

    private static IError ErroDeCampo(string campo, string mensagem)
    {
        return new Error(mensagem).WithMetadata("Campo", campo);
    }
}
