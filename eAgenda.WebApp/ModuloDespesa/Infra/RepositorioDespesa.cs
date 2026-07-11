using AutoMapper;
using eAgenda.WebApp.Compartilhado.Infra;
using eAgenda.WebApp.Compartilhado.ModuloBase;
using eAgenda.WebApp.ModuloCategoria.Dominio;
using eAgenda.WebApp.ModuloDespesa.Dominio;

namespace eAgenda.WebApp.ModuloDespesa.Infra;

public class RepositorioDespesa(ISqlConnectionFactory connectionFactory, IMapper mapper) : RepositorioSql<Despesa, Despesa>(connectionFactory, mapper), IRepositorioDespesa
{
    public List<Despesa> Registros => Selecionar();

    public bool Cadastrar(Despesa despesa)
    {
        const string sqlDespesa = """
            INSERT INTO dbo.TBDespesa (Id, Descricao, Data, Valor, FormaPagamento)
            VALUES (@Id, @Descricao, @Data, @Valor, @FormaPagamento)
        """;

        const string sqlCategoria = """
            INSERT INTO dbo.TBCategoriaDespesa (CategoriaId, DespesaId)
            VALUES (@CategoriaId, @DespesaId)
        """;

        var comandos = new List<(string SqlQuery, object? Parametros)>
        {
            (sqlDespesa, despesa)
        };

        comandos.AddRange(despesa.Categorias.Select(c =>
            (sqlCategoria, (object?)new { CategoriaId = c.Id, DespesaId = despesa.Id })));

        return Execute([.. comandos]);
    }

    public bool Editar(Guid id, Despesa despesaEditada)
    {
        despesaEditada.Id = id;

        const string sqlDespesa = """
            UPDATE dbo.TBDespesa
            SET
                Descricao = @Descricao,
                Data = @Data,
                Valor = @Valor,
                FormaPagamento = @FormaPagamento
            WHERE Id = @Id;
        """;

        const string sqlExcluirCategorias = """
            DELETE FROM dbo.TBCategoriaDespesa
            WHERE DespesaId = @Id;
        """;

        const string sqlAdicionarCategoria = """
            INSERT INTO dbo.TBCategoriaDespesa (CategoriaId, DespesaId)
            VALUES (@CategoriaId, @DespesaId)
        """;

        var comandos = new List<(string SqlQuery, object? Parametros)>
        {
            (sqlDespesa, despesaEditada),
            (sqlExcluirCategorias, new { Id = id })
        };

        comandos.AddRange(despesaEditada.Categorias.Select(c =>
            (sqlAdicionarCategoria, (object?)new { CategoriaId = c.Id, DespesaId = id })));

        return Execute([.. comandos]);
    }

    public bool Excluir(Guid id)
    {
        const string sqlCategorias = """
            DELETE FROM dbo.TBCategoriaDespesa
            WHERE DespesaId = @Id;
        """;

        const string sqlDespesa = """
            DELETE FROM dbo.TBDespesa
            WHERE Id = @Id;
        """;

        return Execute(
            (sqlCategorias, new { Id = id }),
            (sqlDespesa, new { Id = id }));
    }

    public Despesa? Selecionar(Guid id)
    {
        string sqlQuery = """
            SELECT  Id, Descricao, Data, Valor, FormaPagamento
            FROM dbo.TBDespesa
            WHERE Id = @Id
        """;

        var despesa = QuerySingle(sqlQuery, id);

        if (despesa is null) return null;

        despesa.Categorias = SelecionarCategorias(despesa.Id);

        return despesa;
    }

    public List<Despesa> Selecionar(Func<Despesa, bool>? filtro = null)
    {
        string sqlQuery = """
            SELECT Id, Descricao, Data, Valor, FormaPagamento
            FROM dbo.TBDespesa
            ORDER BY Descricao;
        """;

        var despesas = Query(sqlQuery).ToList();

        foreach (var despesa in despesas)
            despesa.Categorias = SelecionarCategorias(despesa.Id);

        return [.. despesas.Where(filtro ?? (_ => true))];
    }

    private List<Categoria> SelecionarCategorias(Guid despesaId)
    {
        string sqlQuery = """
            SELECT CategoriaId
            FROM dbo.TBCategoriaDespesa
            WHERE DespesaId = @Id;
        """;

        var categoriaIds = Query<Guid>(sqlQuery, despesaId);

        if (!categoriaIds.Any())
            return [];

        sqlQuery = """
            SELECT Id, Titulo
            FROM dbo.TBCategoria
            WHERE Id IN @Ids
            ORDER BY Titulo;
        """;

        return [.. Query<Categoria>(sqlQuery, new { Ids = categoriaIds })];
    }
}
