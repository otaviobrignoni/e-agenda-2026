namespace eAgenda.WebApp.ModuloTarefa.Dominio;

public interface IRepositorioItemTarefa
{
    List<ItemTarefa> Selecionar(Tarefa tarefa);
    bool Cadastrar(ItemTarefa item);
    bool Excluir(ItemTarefa item);
    bool EditarItens(
        IReadOnlyCollection<ItemTarefa> itensExcluidos,
        IReadOnlyCollection<ItemTarefa> itensAdicionados,
        IReadOnlyCollection<ItemTarefa> itensEditados);
}
