using eAgenda.WebApp.ModuloCategoria.Dominio;
using FluentResults;

namespace eAgenda.WebApp.ModuloCategoria.Aplicacao;

public class ServicoCategoria(IRepositorioCategoria repositorioCategoria)
{
    public Result Cadastrar(CategoriaDto dto)
    {
        if (repositorioCategoria.Selecionar().Any(c => c.Titulo == dto.Titulo))
            return Falha(nameof(dto.Titulo), "Já existe uma categoria com esse título.");

        var categoria = new Categoria(dto.Titulo);
        if (!repositorioCategoria.Cadastrar(categoria))
            return Result.Fail("Não foi possível cadastrar a categoria.");

        return Result.Ok();
    }

    public Result Editar(CategoriaDto dto)
    {
        if (repositorioCategoria.Selecionar(c => c.Id != dto.Id).Any(c => c.Titulo == dto.Titulo))
            return Falha(nameof(dto.Titulo), "Já existe uma categoria com esse título.");

        var categoriaEditada = new Categoria(dto.Titulo);
        if (!repositorioCategoria.Editar(dto.Id, categoriaEditada))
            return Result.Fail("Categoria não encontrada.");
        return Result.Ok();
    }
    public Result Excluir(Guid id)
    {
        if (repositorioCategoria.PossuiDespesas(id))
            return Result.Fail("A categoria está vinculada a uma ou mais despesas.");

        if (!repositorioCategoria.Excluir(id))
            return Result.Fail("Categoria não encontrada.");
        return Result.Ok();
    }

    public Result<CategoriaDto> Selecionar(Guid id)
    {
        var categoria = repositorioCategoria.Selecionar(id);
        if (categoria is null)
            return Result.Fail("Categoria não encontrada.");
        return Result.Ok(new CategoriaDto(categoria.Titulo, categoria.Id));
    }

    public List<CategoriaDto> Selecionar()
    {
        return repositorioCategoria.Registros.Select(t =>
        {
            return new CategoriaDto(t.Titulo, t.Id);
        }).ToList();
    }

    private static Result Falha(string campo, string mensagem)
    {
        return Result.Fail(new Error(mensagem).WithMetadata("Campo", campo));
    }
}
