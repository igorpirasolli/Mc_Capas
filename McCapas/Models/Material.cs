using System.ComponentModel.DataAnnotations;

namespace McCapas.Models
{
    public class Material
    {
        public int Id { get; set; }
        //[Required(ErrorMessage = "inserir dados!")]
        public string TipoDoMaterial { get; set; }
        //[Required(ErrorMessage = "inserir dados!")]

        public string Cor { get; set; }
        //[Required(ErrorMessage = "inserir dados!")]

        public int Quantidade { get; set; }
        public DateTime DataAtual { get; set; }
    }
}
