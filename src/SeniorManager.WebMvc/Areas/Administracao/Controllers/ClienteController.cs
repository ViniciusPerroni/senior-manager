using Microsoft.AspNetCore.Mvc;
using SeniorManager.Crosscutting.Settings;
using SeniorManager.WebMvc.Areas.Administracao.Models.Cliente;
using System.Threading.Tasks;
using Cliente = SeniorManager.Application.Comum.UseCases.Cliente;

namespace SeniorManager.WebMvc.Areas.Administracao.Controllers
{
    [Area("Administracao")]
    public class ClienteController : Controller
    {
        private readonly IReaderSettingsWebMvc readerSettings;
        private readonly Cliente.BuscarPorId.IUseCase buscarClientePorId;
        private readonly Cliente.Listar.IUseCase listarClientes;

        public ClienteController(IReaderSettingsWebMvc readerSettings, Cliente.BuscarPorId.IUseCase buscarClientePorId, Cliente.Listar.IUseCase listarClientes)
        {
            this.readerSettings = readerSettings;
            this.buscarClientePorId = buscarClientePorId;
            this.listarClientes = listarClientes;
        }

        public async Task<IActionResult> Index(Cliente.Listar.Input input)
        {
            input.BaseUrl = readerSettings.BaseUrl() + "Administracao/Index";
            var model = new ListagemClientes
            {
                Output = await listarClientes.Execute(input)
            };

            return model.Output.Ok ? View(model) : View();
        }
    }
}