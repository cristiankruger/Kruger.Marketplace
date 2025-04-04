using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Kruger.Marketplace.CrossCutting.ViewModels.CadastroBasico.Categoria
{
    public class CategoriaViewModel : EntityViewModel
    {
        [DisplayName("Categoria")]
        [Required(ErrorMessage = "Informe o campo Nome da Categoria.")]
        [MaxLength(100, ErrorMessage = "O campo Nome da Categoria deve ter no máximo 100 caracteres.")]
        public string Nome{ get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage ="Informe o campo Descrição.")]
        [MaxLength(500, ErrorMessage = "O campo Descrição deve ter no máximo 500 caracteres.")]
        public string Descricao { get; set; }
    }
}
