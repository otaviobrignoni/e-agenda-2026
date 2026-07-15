using eAgenda.WebApp.Compartilhado.ModuloBase;

namespace eAgenda.WebApp.ModuloTarefa.Dominio;

public class ItemTarefa(string titulo, Tarefa tarefa, bool estaConcluido = false) : EntidadeBase<ItemTarefa>
{
    public string Titulo { get; set; } = titulo;
    public bool EstaConcluido { get; set; } = estaConcluido;
    public Tarefa Tarefa { get; set; } = tarefa;

    public ItemTarefa() : this(string.Empty, null!) { }

    public override void Atualizar(ItemTarefa registroEditado)
    {
        Titulo = registroEditado.Titulo;
        EstaConcluido = registroEditado.EstaConcluido;
    }
}
