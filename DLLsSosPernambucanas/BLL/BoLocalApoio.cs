using DLLsSosPernambucanas.DAL;
using DLLsSosPernambucanas.DML;
using DLLsSosPernambucanas.Infrastructure;
using System.Collections.Generic;

namespace DLLsSosPernambucanas.BLL
{
    public class BoLocalApoio
    {
        /// <summary>
        /// Lista com locais de apoio na base de dados.
        /// </summary>
        /// <param></param>
        /// <returns>Propriedade do tipo List<LocalApoio></returns>
        public List<LocalApoio> ListaLocaisApoio()
        {
            DaoLocalApoio daoLocal = new DaoLocalApoio();
            return daoLocal.ListarLocaisApoioDb();
        }

        /// <summary>
        /// Lista com locais de apoio na base de dados.
        /// </summary>
        /// <param></param>
        /// <returns>Propriedade do tipo List<Denunciante></returns>
        public string IncluirLocalApoio(LocalApoio localApoio)
        {
            DaoLocalApoio daoLocal = new DaoLocalApoio();

            string result = daoLocal.IncluirLocarApoioDb(localApoio);

            if (result == null)
                return Constantes.CADASTRO_LOCAL_APOIO_SUCESSO;
            else
                return result;            
        }

        /// <summary>
        /// Lista com locais de apoio na base de dados.
        /// </summary>
        /// <param></param>
        /// <returns>Propriedade do tipo List<Denunciante></returns>
        public string ExcluirLocalApoio(int idLocal)
        {
            DaoLocalApoio daoLocal = new DaoLocalApoio();

            string result = daoLocal.ExcluirLocarApoioDb(idLocal);

            if (result == null)
                return Constantes.EXCLUSAO_LOCAL_APOIO_SUCESSO;
            else
                return result;

        }
    }
}
