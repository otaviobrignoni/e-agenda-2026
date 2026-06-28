namespace eAgenda.WebApp.ModuloTarefa.Dominio;

public class ItemTarefa(string titulo, Tarefa tarefa, bool estaConcluido = false)
{
    public string Titulo { get; set; } = titulo;
    public bool EstaConcluido { get; set; } = estaConcluido;
    public Tarefa Tarefa { get; set; } = tarefa;
    public Guid TarefaId => Tarefa.Id;
}
