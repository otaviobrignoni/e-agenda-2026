using eAgenda.WebApp.Compartilhado.ModuloBase;
using eAgenda.WebApp.ModuloContato.Dominio;

namespace eAgenda.WebApp.ModuloCompromisso.Dominio;

public class Compromisso : EntidadeBase<Compromisso>
{
    public string Assunto { get; set; } = string.Empty;
    public DateOnly Data { get; set; }
    public TimeOnly HoraInicio { get; set; }
    public TimeOnly HoraTermino { get; set; }
    public TipoCompromisso Tipo { get; set; }
    public string LocalOuLink { get; set; } = string.Empty;
    public Contato? Contato { get; set; }
    public Guid? ContatoId => Contato?.Id;


    public Compromisso() { }
    public Compromisso(string assunto, DateOnly data, TimeOnly horaInicio, TimeOnly horaTermino, TipoCompromisso tipo, string localOuLink, Contato? contato = null)
    {
        Assunto = assunto;
        Data = data;
        HoraInicio = horaInicio;
        HoraTermino = horaTermino;
        Tipo = tipo;
        LocalOuLink = localOuLink;
        Contato = contato;
    }

    public override void Atualizar(Compromisso compromissoEditado)
    {
        Assunto = compromissoEditado.Assunto;
        Data = compromissoEditado.Data;
        HoraInicio = compromissoEditado.HoraInicio;
        HoraTermino = compromissoEditado.HoraTermino;
        Tipo = compromissoEditado.Tipo;
        LocalOuLink = compromissoEditado.LocalOuLink;
        Contato = compromissoEditado.Contato;
    }
}
