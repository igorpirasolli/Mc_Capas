using System.ComponentModel.DataAnnotations;

namespace McCapas.Dto
{
    public class UsuarioRegistroDto
    {
        [Required(ErrorMessage = "digite seu Nome!")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "digite seu Sobrenome!")]
        public string Sobrenome { get; set; }
        [Required(ErrorMessage = "digite seu Email!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "digite sua Senha!")]
        public string Senha { get; set; }
        [Required(ErrorMessage = "Confirme sua Senha!"),
            Compare("Senha", ErrorMessage = "As senhas não estão iguais!")]
        public string ConfirmarSenha { get; set; }
    }
}
