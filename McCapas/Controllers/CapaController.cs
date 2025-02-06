using ClosedXML.Excel;
using McCapas.Data;
using McCapas.Models;
using McCapas.Services;
using McCapas.ServicesLogin.SessaoService;
using Microsoft.AspNetCore.Mvc;

namespace McCapas.Controllers
{
    public class CapaController : Controller
    {
        private readonly ICapaService _capaService;
        private readonly ISessaoInterface _sessaoInterface;

        public CapaController( ICapaService capaService, ISessaoInterface sessaoInterface)
        {
            _capaService = capaService;
            _sessaoInterface = sessaoInterface;
        }

        public async Task<IActionResult> Index()
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var capas = await _capaService.GetAllCapas();
            return View(capas);
        }

        public IActionResult Create()
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Capas capas)
        {
            if (ModelState.IsValid) 
            {
                await _capaService.AddProdutoAsync(capas);

                TempData["MensagemSucesso"] = "Criação realizado com sucesso";

                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (id == null || id == 0) {
                return NotFound();
            }

            var capa = await _capaService.GetProdutoByAsyncCapas(id.Value);

            if (capa == null)
            {
                return NotFound();
            }

            return View(capa);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Capas capas)
        {
            if (ModelState.IsValid)
            {
                await _capaService.UpdateProdutoAsync(capas);

                TempData["MensagemSucesso"] = "Edição realizada com sucesso";

                return RedirectToAction("Index");
            }
            return View(capas);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var capa = await _capaService.GetProdutoByAsyncCapas(id.Value); 

            if (capa == null)
            {
                return NotFound();
            }

            return View(capa);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {

            await _capaService.DeleteProdutoAsync(id);
           

            TempData["MensagemSucesso"] = "Remoção realizada com sucesso";

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Exportar()
        {
            var dados = await _capaService.GetData();

            using (XLWorkbook workbook = new XLWorkbook())
            {
                workbook.AddWorksheet(dados, "Dados do Tapete");

                using (MemoryStream ms = new MemoryStream())
                {
                    workbook.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spredsheetml.sheet", "Capas.xls");
                }
            }
        }
    }
}
