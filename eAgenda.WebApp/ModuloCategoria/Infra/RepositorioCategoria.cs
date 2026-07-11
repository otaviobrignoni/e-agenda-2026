using AutoMapper;
using eAgenda.WebApp.Compartilhado.Infra;
using eAgenda.WebApp.Compartilhado.ModuloBase;
using eAgenda.WebApp.ModuloCategoria.Dominio;

namespace eAgenda.WebApp.ModuloCategoria.Infra;

public class RepositorioCategoria : RepositorioSql<Categoria, Categoria>, IRepositorioCategoria
{
    public RepositorioCategoria(ISqlConnectionFactory connectionFactory, IMapper mapper) : base(connectionFactory, mapper)
    {
    }

    public List<Categoria> Registros => Selecionar();

    public bool Cadastrar(Categoria registro)
    {
        string sqlQuery = """
            INSERT INTO dbo.TBCategoria (Id, Titulo)
            VALUES (@Id, @Titulo)
        """;

        return Execute(sqlQuery, registro);
    }

    public bool Editar(Guid id, Categoria registroEditado)
    {
        registroEditado.Id = id;

        string sqlQuery = """
            UPDATE dbo.TBCategoria
            SET 
                Titulo = @Titulo
            WHERE Id = @Id;
        """;

        return Execute(sqlQuery, registroEditado);
    }

    public bool Excluir(Guid id)
    {
        string sqlQuery = """
            DELETE FROM dbo.TBCategoria
            WHERE Id = @Id;
        """;

        return Execute(sqlQuery, id);
    }

    public Categoria? Selecionar(Guid id)
    {
        string sqlQuery = """
            SELECT Id, Titulo
            FROM dbo.TBCategoria
            WHERE Id = @Id
        """;

        return QuerySingle(sqlQuery, id);
    }

    public List<Categoria> Selecionar(Func<Categoria, bool>? filtro = null)
    {
        string sqlQuery = """
            SELECT Id, Titulo
            FROM dbo.TBCategoria       
            ORDER BY Titulo;
        """;

        return Query(sqlQuery).Where(filtro ?? (t => true)).ToList();
    }

    public bool PossuiDespesas(Guid id)
    {
        const string sqlQuery = """
            SELECT COUNT(1)
            FROM dbo.TBCategoriaDespesa
            WHERE CategoriaId = @Id;
        """;

        return QuerySingle<int>(sqlQuery, id) > 0;
    }
}
