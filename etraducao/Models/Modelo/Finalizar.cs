using System.ComponentModel.DataAnnotations;

namespace etraducao.Models.Modelo
{
    public class Finalizar
    {
        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage = "Campo {0} deve ser um email válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        public string Telefone { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }
    }
}
