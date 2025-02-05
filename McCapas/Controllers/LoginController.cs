using McCapas.Dto;
using McCapas.ServicesLogin;
using Microsoft.AspNetCore.Mvc;

namespace McCapas.Controllers
{
    public class LoginController : Controller
    {
        private readonly IloginInterface _loginInterface;
        public LoginController(IloginInterface iloginInterface)
        {
            _loginInterface = iloginInterface;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(UsuarioRegistroDto usuarioRegistroDto)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _loginInterface.Registrar(usuarioRegistroDto);

                if (usuario.Status)
                {
                    TempData["MensagemSucesso"] = usuario.Mensagem;
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["MensagemErro"] = usuario.Mensagem;
                    return View(usuarioRegistroDto);
                }

                //return RedirectToAction("Index");
            }

            else
            {
                return View(usuarioRegistroDto);
            }
        }
    }
}
