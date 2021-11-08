using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SosDenunciante.Models
{
    public class Denunciante : Usuario 
    {
        [DisplayName("Código:")]
        [StringLength(5, ErrorMessage = "São permitidos até 5 caracteres.")]
        public int IdDenunciante { get; set; }

        [Required(ErrorMessage = "O preenchimento é obrigatório", AllowEmptyStrings = false)]
        [DisplayName("Nome:")]
        [StringLength(200, ErrorMessage = "São permitidos até 200 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O preenchimento é obrigatório", AllowEmptyStrings = false)]
        [DisplayName("Telefone:")]
        [StringLength(14, ErrorMessage = "Digite no formato (00)00000-0000")]
        [MaxLength(14, ErrorMessage = "Digite no formato (00)00000-0000")]
        [MinLength(14, ErrorMessage = "Digite no formato (00)00000-0000")]
        [RegularExpression(@"\(?[0-9]{2}\)?[0-9]{5}\-?[0-9]{4}", ErrorMessage = "Digite no formato (00)00000-0000")]
        public string Telefone { get; set; }
    }
}