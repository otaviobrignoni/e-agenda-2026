namespace eAgenda.WebApp.ModuloTarefa.Dominio;

public class ItemTarefa(string titulo, Tarefa tarefa, bool? estaConcluido = null)
{
    public string Titulo { get; set; } = titulo;
    public bool EstaConcluido { get; set; } = estaConcluido ?? false;
    public Tarefa Tarefa { get; set; } = tarefa;
}
