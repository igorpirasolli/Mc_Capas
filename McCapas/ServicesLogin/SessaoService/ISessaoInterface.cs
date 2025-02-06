using McCapas.Models;

namespace McCapas.ServicesLogin.SessaoService
{
    public interface ISessaoInterface
    {
        UsuarioModel BuscarSessao();
        void CriarSessao(UsuarioModel usuarioModel);
        void RemoverSessao();
    }
}
