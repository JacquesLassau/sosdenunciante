using DLLsSosPernambucanas.DAL;
using DLLsSosPernambucanas.DML;
using DLLsSosPernambucanas.Infrastructure;
using System;
using System.Collections.Generic;


namespace DLLsSosPernambucanas.BLL
{
    public class BoOcorrencia
    {
        /// <summary>
        /// Lista com ocorrencias na base de dados.
        /// </summary>
        /// <param></param>
        /// <returns>Propriedade do tipo List<Ocorrencia></returns>
        public List<Ocorrencia> ListaLocaisApoio()
        {
            DaoOcorrencia daoOcorrencia = new DaoOcorrencia();
            return daoOcorrencia.ListarOcorrenciasDb();
        }

        /// <summary>
        /// Altera situacao da ligação na ocorrência
        /// </summary>
        /// <param></param>
        /// <returns>booleano que indica se a situação foi alterada</returns>
        public bool AlterarSituacaoLigacao(int idOcorrencia)
        {
            DaoOcorrencia daoOcorrencia = new DaoOcorrencia();
            return daoOcorrencia.AlterarSituacaoLigacaoDb(idOcorrencia, Convert.ToInt32(Constantes.SituacaoLigacaoOcorrencia.EM_ANDAMENTO));
        }

        /// <summary>
        /// Cadastra a descrição da ocorrência
        /// </summary>
        /// <param></param>
        /// <returns>booleano que indica se a situação da ocorrência foi alterada</returns>
        public bool CadastrarOcorrenciaDenunciante(int idOcorrencia, string descricaoOcorrencia)
        {
            DaoOcorrencia daoOcorrencia = new DaoOcorrencia();
            return daoOcorrencia.CadastrarOcorrenciaDenuncianteDb(idOcorrencia, descricaoOcorrencia, Convert.ToInt32(Constantes.SituacaoLigacaoOcorrencia.FINALIZADAS));
        }

        /// <summary>
        /// Consulta de Ocorrência
        /// </summary>
        /// <param></param>
        /// <returns>Objeto do tipo Ocorrencia</returns>
        public Ocorrencia ConsultarOcorrencia(int idOcorrencia)
        {
            DaoOcorrencia daoOcorrencia = new DaoOcorrencia();
            Ocorrencia ocorrencia = daoOcorrencia.ConsultarOcorrenciaDb(idOcorrencia);
            return ocorrencia;
        }
    }
}
