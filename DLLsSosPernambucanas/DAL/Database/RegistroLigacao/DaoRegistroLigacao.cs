using System.Data.SqlClient;
using System.Data;
using DLLsSosPernambucanas.DML;
using System;

namespace DLLsSosPernambucanas.DAL
{
    public class DaoRegistroLigacao: IDaoRegistroLigacao
    {
        public Conexao Conexao { get; set; }

        public DaoRegistroLigacao()
        {
            Conexao = new Conexao();
        }

        public bool CadastraRegistroLigacaoDb(RegistroLigacao registroLigacao, string emailUsuario)
        {
            using (SqlConnection conexao = Conexao.ConexaoDatabase())
            {
                conexao.Open();

                try
                {
                    SqlCommand comandoDML = new SqlCommand("SP_CadastrarRegistroLigacao", conexao);
                    comandoDML.CommandType = CommandType.StoredProcedure;

                    comandoDML.Parameters.Add("@Email", SqlDbType.VarChar, 200);
                    comandoDML.Parameters.Add("@Numero", SqlDbType.VarChar, 200);
                    comandoDML.Parameters.Add("@Descricao", SqlDbType.VarChar, 200);

                    comandoDML.Parameters["@Email"].Value = emailUsuario;
                    comandoDML.Parameters["@Numero"].Value = registroLigacao.Numero;
                    comandoDML.Parameters["@Descricao"].Value = registroLigacao.Descricao;

                    SqlDataReader dr = comandoDML.ExecuteReader();
                    bool registroCadastrado = dr.HasRows;

                    conexao.Close();
                    return registroCadastrado;                    
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
                    return false;
                }
            }
        }
    }
}
