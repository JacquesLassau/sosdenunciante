using DLLsSosPernambucanas.DAL;
using DLLsSosPernambucanas.DML;
using DLLsSosPernambucanas.Infrastructure;
using System;

namespace DLLsSosPernambucanas.BLL
{
    public class BoToken
    {
        /// <summary>
        /// Cadastra um novo token.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public void IncluirToken(Token token)
        {
            DaoToken daoToken = new DaoToken();
            daoToken.IncluirTokenDb(token);
        }

        /// <summary>
        /// Acesso do usuário ao link enviado por e-mail.
        /// </summary>
        /// <param name="token"></param>        
        /// <returns></returns>
        public void GravarAcessoToken(string token)
        {
            DaoToken daoToken = new DaoToken();
            daoToken.GravarAcessoTokenDb(token);
        }

        /// <summary>
        /// Verifica e-mail vinculado ao token.
        /// </summary>
        /// <param name="token"></param>        
        /// <returns>Retorna e-mail do usuário vinculado ao token.</returns>
        public string BuscarEmailToken(string token)
        {
            DaoToken daoToken = new DaoToken();
            return daoToken.BuscarEmailTokenDb(token);
        }

        /// <summary>
        /// Verifica e-mail vinculado ao token.
        /// </summary>
        /// <param name="token"></param>        
        /// <returns>Retorna se o token foi usado ou não.</returns>
        public string BuscarValidadeToken(string token)
        {
            DaoToken daoToken = new DaoToken();
            return daoToken.BuscarValidadeTokenDb(token);
        }

    }
}

