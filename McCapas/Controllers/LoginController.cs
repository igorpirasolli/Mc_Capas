using McCapas.Dto;
using McCapas.ServicesLogin;
using McCapas.ServicesLogin.SessaoService;
using Microsoft.AspNetCore.Mvc;

namespace McCapas.Controllers
{
    public class LoginController : Controller
    {
        private readonly IloginInterface _loginInterface;
        private readonly ISessaoInterface _sessao;
        public LoginController(IloginInterface iloginInterface, ISessaoInterface sessaoInterface)
        {
            _loginInterface = iloginInterface;
            _sessao = sessaoInterface;
        }
        public IActionResult Login()
        {
            var usuario = _sessao.BuscarSessao();
            if (usuario != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Logout()
        {
            _sessao.RemoverSessao();
            return RedirectToAction("Login");
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
                    
                }
                else
                {
                    TempData["MensagemErro"] = usuario.Mensagem;
                    return View(usuarioRegistroDto);
                }

                return RedirectToAction("Login");
            }

            else
            {
                return View(usuarioRegistroDto);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(UsuarioLoginDto usuarioLoginDto)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _loginInterface.Login(usuarioLoginDto);

                if (usuario.Status)
                {
                    TempData["MensagemSucesso"] = usuario.Mensagem;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["MensagemErro"] = usuario.Mensagem;
                    return View(usuarioLoginDto);
                }
            }
            else
            {
                return View(usuarioLoginDto);
            }

        }
    }
}
