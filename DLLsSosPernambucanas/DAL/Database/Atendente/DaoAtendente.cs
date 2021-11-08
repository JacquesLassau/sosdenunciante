using DLLsSosPernambucanas.DML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DLLsSosPernambucanas.DAL
{
    public class DaoAtendente: IDaoAtendente
    {
        public Conexao Conexao { get; set; }

        public DaoAtendente()
        {
            Conexao = new Conexao();
        }

        public string IncluirAtendenteDb(Atendente atendente)
        {
            DaoUsuario daoUsuario = new DaoUsuario();
            daoUsuario.IncluirUsuarioDb(atendente);

            using (SqlConnection conexao = Conexao.ConexaoDatabase())
            {
                conexao.Open();

                try
                {
                    SqlCommand comandoDML = new SqlCommand("SP_CadastrarAtendente", conexao);
                    comandoDML.CommandType = CommandType.StoredProcedure;

                    comandoDML.Parameters.Add("@Email", SqlDbType.VarChar, 200);
                    comandoDML.Parameters["@Email"].Value = atendente.Email;

                    comandoDML.Parameters.Add("@Matricula", SqlDbType.VarChar, 200);
                    comandoDML.Parameters["@Matricula"].Value = atendente.Matricula;

                    comandoDML.Parameters.Add("@Nome", SqlDbType.VarChar, 200);
                    comandoDML.Parameters["@Nome"].Value = atendente.Nome;

                    comandoDML.ExecuteNonQuery();

                    conexao.Close();
                    return null;
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
                    return logSistema.Descricao;
                }
            }
        }

        public string EditarAtendenteDb(Atendente atendente)
        {
            using (SqlConnection conexao = Conexao.ConexaoDatabase())
            {
                conexao.Open();

                try
                {
                    SqlCommand comandoDML = new SqlCommand("SP_EditarAtendente", conexao);
                    comandoDML.CommandType = CommandType.StoredProcedure;

                    comandoDML.Parameters.Add("@IdAtendente", SqlDbType.VarChar, 200);
                    comandoDML.Parameters["@IdAtendente"].Value = atendente.IdAtendente;

                    comandoDML.Parameters.Add("@Matricula", SqlDbType.VarChar, 200);
                    comandoDML.Parameters["@Matricula"].Value = atendente.Matricula;

                    comandoDML.Parameters.Add("@Nome", SqlDbType.VarChar, 200);
                    comandoDML.Parameters["@Nome"].Value = atendente.Nome;

                    comandoDML.Parameters.Add("@Email", SqlDbType.VarChar, 200);
                    comandoDML.Parameters["@Email"].Value = atendente.Email;

                    comandoDML.Parameters.Add("@Senha", SqlDbType.VarChar, 200);
                    comandoDML.Parameters["@Senha"].Value = atendente.Senha;

                    comandoDML.ExecuteNonQuery();

                    conexao.Close();
                    return null;
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
                    return logSistema.Descricao;
                }
            }
        }

        public string ExcluirAtendenteDb(Atendente atendente)
        {
            using (SqlConnection conexao = Conexao.ConexaoDatabase())
            {
                conexao.Open();

                try
                {
                    SqlCommand comandoDML = new SqlCommand("SP_ExcluirAtendente", conexao);
                    comandoDML.CommandType = CommandType.StoredProcedure;

                    comandoDML.Parameters.Add("@IdAtendente", SqlDbType.VarChar, 200);
                    comandoDML.Parameters["@IdAtendente"].Value = atendente.IdAtendente;

                    comandoDML.Parameters.Add("@Email", SqlDbType.VarChar, 200);
                    comandoDML.Parameters["@Email"].Value = atendente.Email;

                    comandoDML.Parameters.Add("@Situacao", SqlDbType.VarChar, 200);
                    comandoDML.Parameters["@Situacao"].Value = atendente.Situacao;

                    comandoDML.ExecuteNonQuery();

                    conexao.Close();
                    return null;
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
                    return logSistema.Descricao;
                }
            }
        }

        public Atendente SelecionarAtendenteDb(int idAtendente)
        {
            using (SqlConnection conexao = Conexao.ConexaoDatabase())
            {
                conexao.Open();                

                SqlCommand comandoDML = new SqlCommand("SP_SelecionarAtendente", conexao);
                comandoDML.CommandType = CommandType.StoredProcedure;

                comandoDML.Parameters.Add("@idAtendente", SqlDbType.Int);
                comandoDML.Parameters["@idAtendente"].Value = idAtendente;

                SqlDataReader dr = comandoDML.ExecuteReader();
                bool consultarExistenciaAtendente = dr.HasRows;

                Atendente atendente = new Atendente();
                if (consultarExistenciaAtendente)
                {
                    while (dr.Read())
                    {
                        atendente.IdAtendente = Convert.ToInt32(dr["IdATENDENTE"]);
                        atendente.Matricula = Convert.ToString(dr["MATRICULA"]);
                        atendente.Nome = Convert.ToString(dr["NOME"]);
                        atendente.Email = Convert.ToString(dr["EMAIL"]);
                        atendente.Senha = Convert.ToString(dr["SENHA"]);
                    }
                }

                conexao.Close();
                return atendente;
            }
        }

        public List<Atendente> ListarAtendentesDb()
        {
            using (SqlConnection conexao = Conexao.ConexaoDatabase())
            {
                conexao.Open();

                List<Atendente> atendentes = new List<Atendente>();

                SqlCommand comandoDML = new SqlCommand("SP_ListarAtendentes", conexao);
                comandoDML.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = comandoDML.ExecuteReader();

                while (dr.Read())
                {
                    int idAtendente = Convert.ToInt32(dr["IdATENDENTE"]);
                    string matricula = Convert.ToString(dr["MATRICULA"]);
                    string nome = Convert.ToString(dr["NOME"]);
                    string email = Convert.ToString(dr["EMAIL"]);
                    string senha = Convert.ToString(dr["SENHA"]);

                    atendentes.Add(new Atendente(idAtendente, matricula, nome, email, senha));
                }

                conexao.Close();
                return atendentes;
            }
        }        
    }
}
