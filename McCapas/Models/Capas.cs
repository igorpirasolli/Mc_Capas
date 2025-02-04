using System.ComponentModel.DataAnnotations;

namespace McCapas.Models
{
    public class Capas
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "favor, inserir marca.")]
        public string Marca { get; set; }
        [Required(ErrorMessage = "favor, inserir modelo.")]
        public string Modelo { get; set; }
        [Required(ErrorMessage = "favor, inserir cor.")]
        public string Cor { get; set; }
        [Required(ErrorMessage = "favor, inserir quantidade.")]
        public int Quantidade { get; set; }
        public DateTime DataAtual { get; set; }
    }
}
