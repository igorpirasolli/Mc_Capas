using McCapas.Data;
using McCapas.Dto;
using McCapas.Models;
using McCapas.ServicesLogin.SenhaService;
using McCapas.ServicesLogin.SessaoService;

namespace McCapas.ServicesLogin
{
    public class LoginService : IloginInterface
    {
        private readonly AppDbContext _context;
        private readonly ISenhaInterface _senhaInterface;
        private readonly ISessaoInterface _sessaoInterface;

        public LoginService(AppDbContext context, ISenhaInterface senhaInterface, ISessaoInterface sessaoInterface)
        {
            _context = context;
            _senhaInterface = senhaInterface;
            _sessaoInterface = sessaoInterface;
        }

        public async Task<ResponseModel<UsuarioLoginDto>> Login(UsuarioLoginDto loginDto)
        {
            ResponseModel<UsuarioLoginDto> response = new ResponseModel<UsuarioLoginDto>();

            try
            {
                var usuario = _context.usuarios.FirstOrDefault(x => x.Email == loginDto.Email);

                if (usuario == null)
                {
                    response.Mensagem = "Credenciais Inválidas!";
                    response.Status = false;
                    return response;
                }

                if (!_senhaInterface.VerificaSenha(loginDto.Senha, usuario.SenhaHash, usuario.SenhaSalt))
                {
                    response.Mensagem = "Credenciais Inválidas";
                    response.Status = false;
                    return response;
                }

                //criando sessão
                _sessaoInterface.CriarSessao(usuario);
                response.Mensagem = "Você esta logado!";
                return response;
                
            }
            catch (Exception ex)
            {
                response.Mensagem = "Credenciais inválidas!";
                response.Status = false;
                return response;
            }
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
