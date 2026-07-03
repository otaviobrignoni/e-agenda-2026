using System.ComponentModel.DataAnnotations;
using eAgenda.WebApp.ModuloCategoria.Apresentacao;
using eAgenda.WebApp.ModuloDespesa.Dominio;

namespace eAgenda.WebApp.ModuloDespesa.Apresentacao;

public record class DespesaViewModel(
    Guid Id,
    [Required(ErrorMessage = "O campo \"\" deve ser preenchido")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Descrição\" deve conter entre 2 e 100 caracteres.")]
    string Descricao,
    DateTime Data,
    [Required(ErrorMessage = "O campo \"Valor\" deve ser preenchido")]
    decimal Valor,
    FormaPagamento FormaPagamento,
    List<CategoriaViewModel> Categorias
);

