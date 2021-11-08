using System;
using System.Collections.Generic;

namespace DLLsSosPernambucanas.DML
{
    public class Ocorrencia
    {
        public int IdOcorrencia { get; set; }        
        public string NomeDenunciante {get ;set;}        
        public string TelefoneDenunciante { get; set; }
        public int NumeroRegistroLigacao { get; set; }
        public string DescricaoRegistroLigacao { get; set; }
        public DateTime DataHoraRegistroLigacao { get; set; }
        public int SituacaoOcorrencia { get; set; }
        public string DescricaoOcorrencia { get; set; }

        public Ocorrencia() { }

        public Ocorrencia(string nomeDenunciante, string telefoneDenunciante, int numeroRegistroLigacao, string descricaoRegistroLigacao, string descricaoOcorrencia)
        {            
            this.NomeDenunciante = nomeDenunciante;
            this.TelefoneDenunciante = telefoneDenunciante;
            this.NumeroRegistroLigacao = numeroRegistroLigacao;
            this.DescricaoRegistroLigacao = descricaoRegistroLigacao;
            this.DescricaoOcorrencia = descricaoOcorrencia;
        }

        public Ocorrencia(int idOcorrencia, string nomeDenunciante, string telefoneDenunciante, int numeroRegistroLigacao, string descricaoRegistroLigacao, DateTime dataHoraRegistroLigacao, int situacaoOcorrencia) 
        {
            this.IdOcorrencia = idOcorrencia;
            this.NomeDenunciante = nomeDenunciante;
            this.TelefoneDenunciante = telefoneDenunciante;
            this.NumeroRegistroLigacao = numeroRegistroLigacao;
            this.DescricaoRegistroLigacao = descricaoRegistroLigacao;
            this.DataHoraRegistroLigacao = dataHoraRegistroLigacao;
            this.SituacaoOcorrencia = situacaoOcorrencia;            
        }
    }   

    public class OcorrenciaViewModelDML
    {
        public List<Ocorrencia> Ocorrencia { get; set; }

        public OcorrenciaViewModelDML()
        {
            this.Ocorrencia = new List<Ocorrencia>();
        }

        public OcorrenciaViewModelDML ConvertToListOcorrencias(List<Ocorrencia> listaOcorrencia)
        {
            OcorrenciaViewModelDML ocorrenciaViewModel = new OcorrenciaViewModelDML();
            if (listaOcorrencia != null)
            {
                // ocorrenciaViewModel.Ocorrencia = listaOcorrencia;
                // foreach está sendo usado para CASO deseje incluir validação no carregamento dos registros via conversão
                foreach (var ocorrencia in listaOcorrencia)
                {
                    ocorrenciaViewModel.Ocorrencia.Add(ocorrencia);
                }
            }

            return ocorrenciaViewModel;
        }
    }
}
