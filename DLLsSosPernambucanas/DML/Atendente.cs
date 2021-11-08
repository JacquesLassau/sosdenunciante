using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DLLsSosPernambucanas.DML
{
    public class Atendente: Usuario
    {
        public int IdAtendente { get; set; }

        [Required(ErrorMessage = "O preenchimento é obrigatório", AllowEmptyStrings = false)]
        [DisplayName("Matricula:")]
        [StringLength(200, ErrorMessage = "São permitidos até 200 caracteres.")]
        public string Matricula { get; set; }

        [Required(ErrorMessage = "O preenchimento é obrigatório", AllowEmptyStrings = false)]
        [DisplayName("Nome:")]
        [StringLength(200, ErrorMessage = "São permitidos até 200 caracteres.")]
        public string Nome { get; set; }

        public Atendente() { }
        public Atendente(int id, string matricula, string nome, string email, string senha)
        {
            this.IdAtendente = id;
            this.Matricula = matricula;
            this.Nome = nome;
            this.Email = email;
            this.Senha = senha;
        }
    }

    public class AtendenteViewModelDML
    {
        public List<Atendente> Atendente { get; set; }

        public AtendenteViewModelDML()
        {
            this.Atendente = new List<Atendente>();
        }

        public AtendenteViewModelDML ConvertToListAtendentes(List<Atendente> listaAtendente)
        {
            AtendenteViewModelDML atendenteViewModel = new AtendenteViewModelDML();
            if (listaAtendente != null)
            {
                // atendenteViewModel.Atendente = listaAtendente;
                // foreach está sendo usado para CASO deseje incluir validação no carregamento dos registros via conversão
                foreach (var atendente in listaAtendente)
                {
                    atendenteViewModel.Atendente.Add(atendente);
                }
            }

            return atendenteViewModel;
        }
    }
}
