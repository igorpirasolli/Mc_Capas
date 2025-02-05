using McCapas.Data;
using McCapas.Dto;
using McCapas.Models;
using McCapas.ServicesLogin.SenhaService;

namespace McCapas.ServicesLogin
{
    public class LoginService : IloginInterface
    {
        private readonly AppDbContext _context;
        private readonly ISenhaInterface _senhaInterface;

        public LoginService(AppDbContext context, ISenhaInterface senhaInterface)
        {
            _context = context;
            _senhaInterface = senhaInterface;
        }

        public async Task<ResponseModel<UsuarioModel>> Registrar(UsuarioRegistroDto registrodto)
        {
            ResponseModel<UsuarioModel> responseModel = new ResponseModel<UsuarioModel>();

            try
            {
                if (VerificarSeEmailExiste(registrodto))
                {
                    responseModel.Mensagem = "Email já cadastrado!";
                    responseModel.Status = false;
                    return responseModel;
                }

                _senhaInterface.CriarSenhaHash(registrodto.Senha, out byte[] senhaHash, out byte[] senhaSalt);

                var usuario = new UsuarioModel
                {
                    Nome = registrodto.Nome,
                    Sobrenome = registrodto.Sobrenome,
                    Email = registrodto.Email,
                    SenhaHash = senhaHash,
                    SenhaSalt = senhaSalt
                };

                _context.Add(usuario);
                await _context.SaveChangesAsync();

                responseModel.Mensagem = "Usuário cadastrado com sucesso!";
                responseModel.Status = true;

                return responseModel;
            }
            catch (Exception ex)
            {
                responseModel.Mensagem = ex.Message;
                responseModel.Status = false;
                return responseModel;
            }
        }

        private bool VerificarSeEmailExiste(UsuarioRegistroDto registrodto)
        {
            var usuario = _context.usuarios.FirstOrDefault(x => x.Email == registrodto.Email);

            if (usuario == null) 
            {
                return false;
            }

            return true;
        }
    }
}
