using AutoMapper;
using eAgenda.WebApp.ModuloDespesa.Aplicacao;
using Microsoft.AspNetCore.Mvc;

namespace eAgenda.WebApp.ModuloDespesa.Apresentacao;

public class DespesaController(ServicoDespesa servicoDespesa, IMapper mapper) : Controller
{
    public ActionResult Index()
    {
        var dtos = servicoDespesa.Selecionar();
        var vms = mapper.Map<List<DespesaViewModel>>(dtos);
        return View(vms);
    }

}
