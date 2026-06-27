using eAgenda.WebApp.Compartilhado.ModuloBase;

namespace eAgenda.WebApp.ModuloTarefa.Dominio;

public class Tarefa(string titulo, PrioridadeTarefa prioridade) : EntidadeBase<Tarefa>
{
    public string Titulo { get; set; } = titulo;
    public PrioridadeTarefa Prioridade { get; set; } = prioridade;
    public DateTime DataCriacao { get; set; } = DateTime.Now;
    public DateTime? DataConclusao { get; set; }
    public bool EstaConcluida => Itens.All(it => it.EstaConcluido == true);
    public float PercentualConcluido => Itens.Count(it => it.EstaConcluido == true) / (float)Itens.Count;
    public List<ItemTarefa> Itens { get; set; } = [];

    public override void Atualizar(Tarefa tarefaEditada)
    {
        Titulo = tarefaEditada.Titulo;
        Prioridade = tarefaEditada.Prioridade;
    }

    public void AdicionarItem(ItemTarefa item)
    {
        Itens.Add(item);
    }

    public void RemvoerItem(ItemTarefa item)
    {
        Itens.Remove(item);
    }
}
