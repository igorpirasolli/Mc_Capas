﻿using ClosedXML.Excel;
using McCapas.Data;
using McCapas.Models;
using McCapas.ServicesLogin.SessaoService;
using McCapas.ServicesTapete;
using Microsoft.AspNetCore.Mvc;

namespace McCapas.Controllers
{
    public class TapeteController : Controller
    {
        private readonly ItapeteServices _TapeteServices;
        private readonly ISessaoInterface _sessaoInterface;

        public TapeteController(ItapeteServices itapeteServices, ISessaoInterface sessaoInterface)
        {
            _TapeteServices = itapeteServices;
            _sessaoInterface = sessaoInterface; 
        }

        public async Task<IActionResult> Index()
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }
            IEnumerable<Tapetes> tapetes = await _TapeteServices.PegarTodosOsTapetes();
            return View(tapetes);
        }

        [HttpGet]
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
        public async Task<IActionResult> Create(Tapetes tapetes)
        {
            
            if (ModelState.IsValid)
            {
                await _TapeteServices.AdicionarTapete(tapetes);

                TempData["MensagemSucesso"] = "Criação realizado com sucesso";

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id) 
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (id == null || id == 0) {
                return NotFound();
            }

            var tapetes = await _TapeteServices.PegarId(id.Value);

            if (tapetes == null)
            {
                return NotFound();
            }

            return View(tapetes);
        }

        
        public async Task<IActionResult> Edit(int? id)
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

            var tapetes = await _TapeteServices.PegarId(id.Value);

            if (tapetes == null)
            {
                return NotFound();
            }

            return View(tapetes);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            
            await _TapeteServices.Deletar(id);
           

            TempData["MensagemSucesso"] = "Remoção realizada com sucesso";

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Tapetes tapetes)
        {
            if (ModelState.IsValid)
            {
                await _TapeteServices.EditarTapete(tapetes);

                TempData["MensagemSucesso"] = "Edição realizada com sucesso";

                return RedirectToAction("Index");
            }
            return View(tapetes);
        }

        public async Task<IActionResult> Exportar()
        {
            var dados = await _TapeteServices.GetData();

            using (XLWorkbook workbook = new XLWorkbook())
            {
                workbook.AddWorksheet(dados, "Dados do Tapete");

                using (MemoryStream ms = new MemoryStream())
                {
                    workbook.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spredsheetml.sheet", "Tapetes.xls");
                }
            }
        }
    }
}
