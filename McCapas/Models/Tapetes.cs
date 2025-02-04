using System.ComponentModel.DataAnnotations;

namespace McCapas.Models
{
    public class Tapetes
    {
        public int Id { get; set; }
        //[Required(ErrorMessage = "inserir marca")]
        public string Marca { get; set; }
        //[Required(ErrorMessage = "inserir modelo")]

        public string Modelo { get; set; }
        //[Required(ErrorMessage = "inserir cor")]

        public string Cor { get; set; }
        //[Required(ErrorMessage = "inserir quantidade")]

        public int Quantidade { get; set; }
        public DateTime DataAtual { get; set; }
    }
}
