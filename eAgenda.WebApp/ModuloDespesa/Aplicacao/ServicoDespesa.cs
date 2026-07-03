using AutoMapper;
using eAgenda.WebApp.ModuloDespesa.Dominio;

namespace eAgenda.WebApp.ModuloDespesa.Aplicacao;

public class ServicoDespesa(IRepositorioDespesa repositorioDespesa, IMapper mapper)
{
    public List<DespesaDto> Selecionar()
    {
        return mapper.Map<List<DespesaDto>>(repositorioDespesa.Registros);
    }
}
