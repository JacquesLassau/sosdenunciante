using System.Data.SqlClient;
using System.Data;
using System;
using DLLsSosPernambucanas.DML;

namespace DLLsSosPernambucanas.DAL
{
    public class DaoUsuario : IDaoUsuario
    {
        public Conexao Conexao { get; set; }

        public DaoUsuario()
        {
            Conexao = new Conexao();
        }

        public void IncluirUsuarioDb(Usuario usuario)
        {
            using (SqlConnection conexao = Conexao.ConexaoDatabase())
            {
                conexao.Open();

                try
                {
                    SqlCommand comandoDML = new SqlCommand("SP_CadastrarUsuario", conexao);
                    comandoDML.CommandType = CommandType.StoredProcedure;

                    comandoDML.Parameters.Add("@Email", SqlDbType.VarChar, 200);
                    comandoDML.Parameters.Add("@Senha", SqlDbType.VarChar, 200);
                    comandoDML.Parameters.Add("@Tipo", SqlDbType.VarChar, 200);
                    comandoDML.Parameters.Add("@Situacao", SqlDbType.Int);

                    comandoDML.Parameters["@Email"].Value = usuario.Email;
                    comandoDML.Parameters["@Senha"].Value = usuario.Senha;
                    comandoDML.Parameters["@Tipo"].Value = usuario.Tipo;
                    comandoDML.Parameters["@Situacao"].Value = usuario.Situacao;

                    comandoDML.ExecuteReader();
                    conexao.Close();
                }
                catch (Exception e)
                {
                    SqlCommand comandoDML = new SqlCommand("SP_CadastrarLogSistema", conexao);
                    comandoDML.CommandType = CommandType.StoredProcedure;

                    comandoDML.Parameters.Add("@Descricao", SqlDbType.VarChar, 200);
                    comandoDML.Parameters["@Descricao"].Value = e.Message;
                    comandoDML.ExecuteNonQuery();

                    LogSistema logSistema = new LogSistema();
                    logSistema.Descricao = e.Message;

                    conexao.Close();                    
                }
            }
        }

        public Usuario AcessoUsuarioDb(Usuario usuario)
        {
            using (SqlConnection conexao = Conexao.ConexaoDatabase())
            {
                conexao.Open();

                SqlCommand comandoDML = new SqlCommand("SP_AcessoUsuario", conexao);
                comandoDML.CommandType = CommandType.StoredProcedure;

                comandoDML.Parameters.Add("@Email", SqlDbType.VarChar, 200);
                comandoDML.Parameters["@Email"].Value = usuario.Email;

                comandoDML.Parameters.Add("@Senha", SqlDbType.VarChar, 200);
                comandoDML.Parameters["@Senha"].Value = usuario.Senha;

                comandoDML.Parameters.Add("@Tipo", SqlDbType.VarChar, 200);
                comandoDML.Parameters["@Tipo"].Value = usuario.Tipo;

                SqlDataReader dr = comandoDML.ExecuteReader();

                bool verificarUsuario = dr.HasRows;

                if (!verificarUsuario)                
                    usuario = null;                
                else
                {
                    while (dr.Read())
                    {
                        int id = Convert.ToInt32(dr["IdUSUARIO"]);
                        string email = Convert.ToString(dr["EMAIL"]);
                        int tipo = Convert.ToInt32(dr["TIPO"]);
                        usuario = new Usuario(id, email, tipo);
                    }
                }

                conexao.Close();
                return usuario;
            }
        }

        public bool VerificarEmailUsuarioDb(int tipo, string email)
        {
            using (SqlConnection conexao = Conexao.ConexaoDatabase())
            {
                conexao.Open();                             

                SqlCommand comandoDML = new SqlCommand("SP_VerificarEmailUsuario", conexao);
                comandoDML.CommandType = CommandType.StoredProcedure;

                comandoDML.Parameters.Add("@Tipo", SqlDbType.Int);
                comandoDML.Parameters["@Tipo"].Value = tipo;

                comandoDML.Parameters.Add("@Email", SqlDbType.VarChar, 200);
                comandoDML.Parameters["@Email"].Value = email;

                SqlDataReader dr = comandoDML.ExecuteReader();
                bool consultarEmailUsuario = dr.HasRows;

                conexao.Close();                
                return consultarEmailUsuario;
            }
        }

        public void AlterarSenhaUsuarioDb(Usuario usuario)
        {
            using (SqlConnection conexao = Conexao.ConexaoDatabase())
            {
                conexao.Open();

                SqlCommand comandoDML = new SqlCommand("SP_AlterarSenhaUsuario", conexao);
                comandoDML.CommandType = CommandType.StoredProcedure;

                comandoDML.Parameters.Add("@Email", SqlDbType.VarChar, 200);
                comandoDML.Parameters["@Email"].Value = usuario.Email;

                comandoDML.Parameters.Add("@NovaSenha", SqlDbType.VarChar, 200);               
                comandoDML.Parameters["@NovaSenha"].Value = usuario.Senha;

                comandoDML.Parameters.Add("@Tipo", SqlDbType.Int);
                comandoDML.Parameters["@Tipo"].Value = usuario.Tipo;

                comandoDML.ExecuteNonQuery();
                conexao.Close();
            }
        }
    }
}
