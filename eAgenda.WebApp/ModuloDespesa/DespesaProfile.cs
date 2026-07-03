using AutoMapper;
using eAgenda.WebApp.ModuloDespesa.Aplicacao;
using eAgenda.WebApp.ModuloDespesa.Apresentacao;
using eAgenda.WebApp.ModuloDespesa.Dominio;

namespace eAgenda.WebApp.ModuloDespesa;

public class DespesaProfile : Profile
{
    public DespesaProfile()
    {
        CreateMap<Despesa, DespesaDto>();
        CreateMap<DespesaDto, DespesaViewModel>();
    }
}
