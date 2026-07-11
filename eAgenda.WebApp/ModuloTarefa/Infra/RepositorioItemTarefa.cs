using AutoMapper;
using eAgenda.WebApp.Compartilhado.Infra;
using eAgenda.WebApp.Compartilhado.ModuloBase;
using eAgenda.WebApp.ModuloTarefa.Dominio;

namespace eAgenda.WebApp.ModuloTarefa.Infra;

public class RepositorioItemTarefa(ISqlConnectionFactory connectionFactory, IMapper mapper)
    : RepositorioSql<ItemTarefa, ItemTarefaRow>(connectionFactory, mapper), IRepositorioItemTarefa
{
    public List<ItemTarefa> Selecionar(Tarefa tarefa)
    {
        string sqlQuery = """
            SELECT Id, Titulo, EstaConcluido, TarefaId
            FROM dbo.TBItemTarefa
            WHERE TarefaId = @Id
            ORDER BY Titulo;
        """;

        return Query(
            sqlQuery,
            new { Id = tarefa.Id },
            ("Tarefa", tarefa)
        ).ToList();
    }

    public bool Cadastrar(ItemTarefa item)
    {
        const string sqlQuery = """
            INSERT INTO dbo.TBItemTarefa (Id, Titulo, EstaConcluido, TarefaId)
            VALUES (@Id, @Titulo, @EstaConcluido, @TarefaId);
        """;

        return Execute(sqlQuery, item);
    }

    public bool Excluir(ItemTarefa item)
    {
        const string sqlQuery = """
            DELETE FROM dbo.TBItemTarefa
            WHERE Id = @Id;
        """;

        return Execute(sqlQuery, item);
    }

    public bool EditarItens(
        IReadOnlyCollection<ItemTarefa> itensExcluidos,
        IReadOnlyCollection<ItemTarefa> itensAdicionados,
        IReadOnlyCollection<ItemTarefa> itensEditados)
    {
        const string sqlExcluir = """
            DELETE FROM dbo.TBItemTarefa
            WHERE Id = @Id;
        """;

        const string sqlCadastrar = """
            INSERT INTO dbo.TBItemTarefa (Id, Titulo, EstaConcluido, TarefaId)
            VALUES (@Id, @Titulo, @EstaConcluido, @TarefaId);
        """;

        const string sqlEditar = """
            UPDATE dbo.TBItemTarefa
            SET EstaConcluido = @EstaConcluido
            WHERE Id = @Id;
        """;

        var comandos = new List<(string SqlQuery, object? Parametros)>();

        AdicionarComandos(sqlExcluir, itensExcluidos);
        AdicionarComandos(sqlCadastrar, itensAdicionados);
        AdicionarComandos(sqlEditar, itensEditados);

        return Execute([.. comandos]);

        void AdicionarComandos(string sqlQuery, IReadOnlyCollection<ItemTarefa> itens)
        {
            comandos.AddRange(itens.Select(item => (sqlQuery, (object?)item)));
        }
    }
}

public class ItemTarefaRow
{
    public Guid Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public bool EstaConcluido { get; set; }
    public Guid TarefaId { get; set; }
}
