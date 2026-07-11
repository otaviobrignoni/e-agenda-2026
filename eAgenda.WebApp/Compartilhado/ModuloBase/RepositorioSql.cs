using AutoMapper;
using Dapper;
using eAgenda.WebApp.Compartilhado.Extensions;
using eAgenda.WebApp.Compartilhado.Infra;

namespace eAgenda.WebApp.Compartilhado.ModuloBase;

public abstract class RepositorioSql<TRegistro, TRow>(ISqlConnectionFactory connectionFactory, IMapper mapper) where TRegistro : EntidadeBase<TRegistro> where TRow : class
{
    protected bool Execute(string sqlQuery, TRegistro registro)
    {
        using var conexao = connectionFactory.CreateConnection();
        conexao.Open();
        using var transacao = conexao.BeginTransaction();

        try
        {
            if (conexao.Execute(sqlQuery, registro, transacao) <= 0)
            {
                transacao.Rollback();
                return false;
            }

            transacao.Commit();
            return true;
        }
        catch
        {
            transacao.Rollback();
            return false;
        }
    }

    protected bool Execute(string sqlQuery, Guid id)
    {
        using var conexao = connectionFactory.CreateConnection();
        conexao.Open();
        using var transacao = conexao.BeginTransaction();

        try
        {
            if (conexao.Execute(sqlQuery, new { Id = id }, transacao) <= 0)
            {
                transacao.Rollback();
                return false;
            }

            transacao.Commit();
            return true;
        }
        catch
        {
            transacao.Rollback();
            return false;
        }
    }

    protected bool Execute(string sqlQuery, object parametros)
    {
        using var conexao = connectionFactory.CreateConnection();
        conexao.Open();
        using var transacao = conexao.BeginTransaction();

        try
        {
            if (conexao.Execute(sqlQuery, parametros, transacao) <= 0)
            {
                transacao.Rollback();
                return false;
            }

            transacao.Commit();
            return true;
        }
        catch
        {
            transacao.Rollback();
            return false;
        }
    }

    protected bool Execute(params (string SqlQuery, object? Parametros)[] comandos)
    {
        using var conexao = connectionFactory.CreateConnection();
        conexao.Open();
        using var transacao = conexao.BeginTransaction();

        try
        {
            foreach (var comando in comandos)
            {
                if (conexao.Execute(comando.SqlQuery, comando.Parametros, transacao) > 0)
                    continue;

                transacao.Rollback();
                return false;
            }

            transacao.Commit();
            return true;
        }
        catch
        {
            transacao.Rollback();
            return false;
        }
    }

    protected IEnumerable<TRegistro> Query(string sqlQuery, Func<TRegistro, bool>? filtro = null)
    {
        using var conexao = connectionFactory.CreateConnection();
        conexao.Open();

        return conexao.Query<TRow>(sqlQuery).Select(Mapear).Where(filtro ?? (_ => true));
    }

    protected IEnumerable<TRegistro> Query(
        string sqlQuery,
        object parametros,
        params (string Key, object Value)[] items)
    {
        using var conexao = connectionFactory.CreateConnection();
        conexao.Open();

        return conexao
            .Query<TRow>(sqlQuery, parametros)
            .Select(row => mapper.MapWith<TRegistro>(row, items))
            .ToList();
    }

    protected IEnumerable<T> Query<T>(string sqlQuery, Guid? id = null)
    {
        using var conexao = connectionFactory.CreateConnection();
        conexao.Open();

        return conexao.Query<T>(sqlQuery, id is not null ? new { Id = id } : null);
    }

    protected IEnumerable<T> Query<T>(string sqlQuery, object parametros)
    {
        using var conexao = connectionFactory.CreateConnection();
        conexao.Open();

        return conexao.Query<T>(sqlQuery, parametros).ToList();
    }

    protected TRegistro? QuerySingle(string sqlQuery, Guid id)
    {
        using var conexao = connectionFactory.CreateConnection();
        conexao.Open();

        var row = conexao.QuerySingleOrDefault<TRow>(sqlQuery, new { Id = id });

        return row is null ? null : Mapear(row);
    }

    protected T QuerySingle<T>(string sqlQuery, Guid id)
    {
        using var conexao = connectionFactory.CreateConnection();
        conexao.Open();

        return conexao.QuerySingle<T>(sqlQuery, new { Id = id });
    }

    private TRegistro Mapear(TRow row)
    {
        if (row is TRegistro registro)
            return registro;

        return mapper.Map<TRegistro>(row);
    }
}
