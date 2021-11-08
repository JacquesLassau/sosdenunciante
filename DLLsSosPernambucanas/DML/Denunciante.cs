using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DLLsSosPernambucanas.DML
{
    public class Denunciante: Usuario
    {
        public int IdDenunciante { get; set; }

        [Required(ErrorMessage = "O preenchimento é obrigatório", AllowEmptyStrings = false)]
        [DisplayName("Nome:")]
        [StringLength(200, ErrorMessage = "São permitidos até 200 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O preenchimento é obrigatório", AllowEmptyStrings = false)]
        [DisplayName("Telefone")]
        [StringLength(14, ErrorMessage = "Digite no formato (00)00000-0000")]
        [MaxLength(14, ErrorMessage = "Digite no formato (00)00000-0000")]
        [MinLength(14, ErrorMessage = "Digite no formato (00)00000-0000")]
        [RegularExpression(@"\(?[0-9]{2}\)?[0-9]{5}\-?[0-9]{4}", ErrorMessage = "Digite no formato (00)00000-0000")]
        public string Telefone { get; set; }

        public Denunciante() { }
        public Denunciante(int id, string nome, string telefone, string email, string senha)
        {
            this.IdDenunciante = id;
            this.Nome = nome;
            this.Telefone = telefone;
            this.Email = email;
            this.Senha = senha;
        }
    }

    public class DenuncianteViewModelDML
    {
        public List<Denunciante> Denunciante { get; set; }

        public DenuncianteViewModelDML()
        {
            this.Denunciante = new List<Denunciante>();
        }

        public DenuncianteViewModelDML ConvertToListDenunciantes(List<Denunciante> listaDenunciante)
        {
            DenuncianteViewModelDML denuncianteControllerModel = new DenuncianteViewModelDML();
            if (listaDenunciante != null)
            {
                // denuncianteControllerModel.Denunciante = listaDenunciante;
                // foreach está sendo usado para CASO deseje incluir validação no carregamento dos registros via conversão
                foreach (var denunciante in listaDenunciante)
                {
                    denuncianteControllerModel.Denunciante.Add(denunciante);
                }
            }

            return denuncianteControllerModel;
        }
    }
}

