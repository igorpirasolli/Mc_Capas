using McCapas.Dto;
using McCapas.Models;

namespace McCapas.ServicesLogin
{
    public interface IloginInterface
    {
        Task<ResponseModel<UsuarioModel>> Registrar(UsuarioRegistroDto registrodto);

        Task<ResponseModel<UsuarioLoginDto>> Login(UsuarioLoginDto loginDto);
    }
}
