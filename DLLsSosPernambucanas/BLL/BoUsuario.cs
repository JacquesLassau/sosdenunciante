using DLLsSosPernambucanas.DAL;
using DLLsSosPernambucanas.DML;
using DLLsSosPernambucanas.Infrastructure;

namespace DLLsSosPernambucanas.BLL
{
    public class BoUsuario
    {
        /// <summary>
        /// Cadastra um novo usuario.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public void IncluirUsuario(Usuario usuario)
        {
            DaoUsuario daoUsuario = new DaoUsuario();
            daoUsuario.IncluirUsuarioDb(usuario);
        }

        /// <summary>
        /// Acesso do usuário no sistema.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns>Instancia do usuario</returns>
        public Usuario AcessoUsuario(Usuario usuario)
        {
            DaoUsuario daoUsuario = new DaoUsuario();
            return daoUsuario.AcessoUsuarioDb(usuario);
        }

        /// <summary>
        /// Editar um usuario.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Cadeia de caracteres contendo log da ação.</returns>
        public bool VerificarEmailUsuario(int tipo, string email)
        {
            DaoUsuario daoUsuario = new DaoUsuario();
            if (daoUsuario.VerificarEmailUsuarioDb(tipo, email))
                return true;
            else
                return false;
        }        

        /// <summary>
        /// Lista de usuarios da base de dados.
        /// </summary>
        /// <param></param>
        /// <param name="usuario"></param>
        /// <param name="novaSenhaUsuario"></param>
        /// <returns></returns>
        public void AlterarSenhaUsuario(Usuario usuario)
        {
            DaoUsuario daoUsuario = new DaoUsuario();
            daoUsuario.AlterarSenhaUsuarioDb(usuario);
        }
    }
}

