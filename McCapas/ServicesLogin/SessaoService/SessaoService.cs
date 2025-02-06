using McCapas.Models;
using Newtonsoft.Json;

namespace McCapas.ServicesLogin.SessaoService
{
    public class SessaoService : ISessaoInterface
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public SessaoService(IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;
        }

        public UsuarioModel BuscarSessao()
        {
            var sessaoUsuario = _contextAccessor.HttpContext.Session.GetString("sessaoUsuario");

            if (string.IsNullOrWhiteSpace(sessaoUsuario))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);
        }

        public void CriarSessao(UsuarioModel usuarioModel)
        {
            var usuarioJson = JsonConvert.SerializeObject(usuarioModel);

            _contextAccessor.HttpContext.Session.SetString("sessaoUsuario", usuarioJson);
        }

        public void RemoverSessao()
        {
            _contextAccessor.HttpContext.Session.Remove("sessaoUsuario");
        }
    }
}
