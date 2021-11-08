using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DLLsSosPernambucanas.DML
{
    public class LocalApoio
    {
        [DisplayName("Código:")]
        [Required(ErrorMessage = "O preenchimento é obrigatório", AllowEmptyStrings = false)]
        public int IdLocal { get; set; }

        [DisplayName("Latitude:")]        
        [Required(ErrorMessage = "O preenchimento é obrigatório", AllowEmptyStrings = false)]
        [StringLength(200, ErrorMessage = "São permitidos até 200 caracteres.")]
        public string Latitude { get; set; }

        [DisplayName("Longitude:")]
        [Required(ErrorMessage = "O preenchimento é obrigatório", AllowEmptyStrings = false)]
        [StringLength(200, ErrorMessage = "São permitidos até 200 caracteres.")]
        public string Longitude { get; set; }

        [DisplayName("Número:")]
        [StringLength(14, ErrorMessage = "São permitidos até 14 caracteres.")]
        public string NumeroEndereco { get; set; }

        [DisplayName("Endereço:")]
        [Required(ErrorMessage = "O preenchimento é obrigatório", AllowEmptyStrings = false)]
        [StringLength(200, ErrorMessage = "São permitidos até 200 caracteres.")]
        public string Endereco { get; set; }
        
        [DisplayName("Bairro")]
        [Required(ErrorMessage = "O preenchimento é obrigatório", AllowEmptyStrings = false)]
        [StringLength(200, ErrorMessage = "São permitidos até 200 caracteres.")]
        public string Bairro { get; set; }
        
        [DisplayName("Cidade")]
        [Required(ErrorMessage = "O preenchimento é obrigatório", AllowEmptyStrings = false)]
        [StringLength(200, ErrorMessage = "São permitidos até 200 caracteres.")]
        public string Cidade { get; set; }
        
        [DisplayName("Estado")]
        [Required(ErrorMessage = "O preenchimento é obrigatório", AllowEmptyStrings = false)]
        [StringLength(200)]
        public string Estado { get; set; }
        
        [DisplayName("CEP")]
        [Required(ErrorMessage = "O preenchimento é obrigatório", AllowEmptyStrings = false)]
        [StringLength(9, ErrorMessage = "São permitidos até 9 caracteres.")]
        [RegularExpression(@"[0-9]{5}\-?[0-9]{3}", ErrorMessage = "Digite no formato 00000-000")]
        public string Cep { get; set; }
                
        [DisplayName("Telefone")]
        [StringLength(14, ErrorMessage = "Devem ser incluídos 14 caracteres.")]
        [MaxLength(14, ErrorMessage = "Preenchimento máximo de 14 caracteres.")]
        [MinLength(14, ErrorMessage = "Preenchimento mínimo de 14 caracteres.")]
        [RegularExpression(@"\(?[0-9]{2}\)?[0-9]{5}\-?[0-9]{4}", ErrorMessage = "Digite no formato (00)00000-0000")]
        public string Telefone { get; set; }

        public LocalApoio() { }

        public LocalApoio(int id,string latitude, string longitude, string logradouro, string numero, string bairro, string cidade, string estado, string cep, string telefone)
        {
            this.IdLocal = id;
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.Endereco = logradouro;
            this.NumeroEndereco = numero;
            this.Bairro = bairro;
            this.Cidade = cidade;
            this.Estado = estado;
            this.Cep = cep;
            this.Telefone = telefone;
        }        
    }

    public class LocalApoioViewModelDML
    {
        public List<LocalApoio> LocalApoio { get; set; }

        public LocalApoioViewModelDML()
        {
            this.LocalApoio = new List<LocalApoio>();
        }

        public LocalApoioViewModelDML ConvertToListLocals(List<LocalApoio> listaLocaisApoio)
        {
            LocalApoioViewModelDML localApoioViewModel = new LocalApoioViewModelDML();
            if (listaLocaisApoio != null)
            {
                // localApoioControllerModel.LocalApoio = listaLocaisApoio;
                // foreach está sendo usado para CASO deseje incluir validação no carregamento dos registros via conversão
                foreach (var locaisApoio in listaLocaisApoio)
                {
                    localApoioViewModel.LocalApoio.Add(locaisApoio);
                }
            }

            return localApoioViewModel;
        }
    }
}
