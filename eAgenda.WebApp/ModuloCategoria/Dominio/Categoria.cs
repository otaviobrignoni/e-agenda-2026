using eAgenda.WebApp.Compartilhado.ModuloBase;
using eAgenda.WebApp.ModuloDespesa.Dominio;

namespace eAgenda.WebApp.ModuloCategoria.Dominio;

public class Categoria : EntidadeBase<Categoria>
{
    public string Titulo { get; set; } = string.Empty;
    public List<Despesa> Despesas { get; set; } = [];

    public Categoria() { }

    public Categoria(string titulo)
    {
        Titulo = titulo;
    }

    public override void Atualizar(Categoria entidadeAtualizada)
    {
        Titulo = entidadeAtualizada.Titulo;
    }
}
