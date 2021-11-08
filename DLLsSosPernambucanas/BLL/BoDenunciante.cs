using DLLsSosPernambucanas.DAL;
using DLLsSosPernambucanas.DML;
using DLLsSosPernambucanas.Infrastructure;
using System.Collections.Generic;
using System;

namespace DLLsSosPernambucanas.BLL
{
    public class BoDenunciante
    {
        /// <summary>
        /// Cadastra um novo denunciante.
        /// </summary>
        /// <param name="denunciante"></param>
        /// <returns>Cadeia de caracteres contendo log da ação.</returns>
        public Denunciante IncluirDenunciante(Denunciante denunciante)
        {
            denunciante.Situacao = Convert.ToInt32(Constantes.SituacaoUsuario.ATIVO);
            denunciante.Tipo = Convert.ToInt32(Constantes.TipoUsuario.DENUNCIANTE);

            BoUsuario boUsuario = new BoUsuario();

            bool existeEmailusuario = boUsuario.VerificarEmailUsuario(denunciante.Tipo, denunciante.Email);

            if (!existeEmailusuario)
            {
                DaoDenunciante daoDenunciante = new DaoDenunciante();
                Denunciante usuDenunciante  = daoDenunciante.IncluirDenuncianteDb(denunciante);

                return usuDenunciante;
            }
            else            
                return null;
                       
        }

        /// <summary>
        /// Edita um denunciante.
        /// </summary>
        /// <param name="denunciante"></param>
        /// <returns>Cadeia de caracteres contendo log da ação.</returns>
        public string EditarDenunciante(Denunciante denunciante)
        {
            DaoDenunciante daoDenunciante = new DaoDenunciante();
            string result = daoDenunciante.EditarDenuncianteDb(denunciante);

            if (string.IsNullOrEmpty(result))
                return Constantes.ALTERACAO_DENUNCIANTE_SUCESSO;
            else
                return result;
        }

        /// <summary>
        /// Exclui logicamente um denunciante.
        /// </summary>
        /// <param name="denunciante"></param>
        /// <returns>Cadeia de caracteres contendo log da ação.</returns>
        public string ExcluirDenunciante(Denunciante denunciante)
        {
            denunciante.Situacao = Convert.ToInt32(Constantes.SituacaoUsuario.INATIVO);           

            DaoDenunciante daoDenunciante = new DaoDenunciante();
            string result = daoDenunciante.ExcluirDenuncianteDb(denunciante);

            if (result == null)
                return Constantes.EXCLUSAO_DENUNCIANTE_SUCESSO;
            else
                return result;
        }

        /// <summary>
        /// Lista de denunciantes da base de dados.
        /// </summary>
        /// <param></param>
        /// <returns>Propriedade do tipo List<Denunciante></returns>
        public List<Denunciante> ListaDenunciantes()
        {
            DaoDenunciante daoDenunciante = new DaoDenunciante();
            return daoDenunciante.ListarDenunciantesDb();
        }

        /// <summary>
        /// Seleciona um denunciante da base de dados.
        /// </summary>
        /// <param name="idDenunciante"></param>
        /// <returns>Propriedade do tipo Denunciante</returns>
        public Denunciante SelecionaDenunciante(int idDenunciante)
        {
            DaoDenunciante daoDenunciante = new DaoDenunciante();
            return daoDenunciante.SelecionarDenuncianteDb(idDenunciante);
        }
    }
}

