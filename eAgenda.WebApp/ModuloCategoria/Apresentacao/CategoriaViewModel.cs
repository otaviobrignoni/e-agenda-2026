using System.ComponentModel.DataAnnotations;

namespace eAgenda.WebApp.ModuloCategoria.Apresentacao;

public record CategoriaViewModel
(
    [StringLength(100, ErrorMessage = "O campo \"Título\" deve conter no máximo 100 caracteres.")]
    string Titulo,
    Guid Id
);


