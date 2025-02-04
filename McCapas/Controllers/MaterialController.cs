﻿using ClosedXML.Excel;
using McCapas.Data;
using McCapas.Models;
using McCapas.ServicesMaterial;
using Microsoft.AspNetCore.Mvc;

namespace McCapas.Controllers
{
    public class MaterialController : Controller
    {
        private readonly IMaterialService _materialService;

        public MaterialController(IMaterialService materialService) => _materialService = materialService;
        
        public async Task<IActionResult> Index()
        {
           var materials = await _materialService.GetMaterialsAsync();
            return View(materials);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Material material)
        {
            if (ModelState.IsValid)
            {
                await _materialService.AddAsync(material);

                TempData["MensagemSucesso"] = "Criação realizado com sucesso";

                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult >Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var material = await _materialService.GetByIdAsync(id.Value);

            if (material == null)
            {
                return NotFound();
            }

            return View(material);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Material material)
        {
            if (ModelState.IsValid)
            {
                await _materialService.UpdateAsync(material);

                TempData["MensagemSucesso"] = "Edição realizada com sucesso";

                return RedirectToAction("Index");
            }
            return View(material);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var material = await _materialService.GetByIdAsync(id.Value);

            if (material == null)
            {
                return NotFound();
            }

            return View(material);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _materialService.DeleteAsync(id);

            TempData["MensagemSucesso"] = "Remoção realizada com sucesso";

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Exportar()
        {
            var dados = await _materialService.GetData();

            using (XLWorkbook workbook = new XLWorkbook())
            {
                workbook.AddWorksheet(dados, "Dados do Tapete");

                using (MemoryStream ms = new MemoryStream())
                {
                    workbook.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spredsheetml.sheet", "Material.xls");
                }
            }
        }
    }
}
