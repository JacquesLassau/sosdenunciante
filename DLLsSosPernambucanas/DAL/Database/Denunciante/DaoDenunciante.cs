using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using DLLsSosPernambucanas.DML;

namespace DLLsSosPernambucanas.DAL
{
    public class DaoDenunciante: IDaoDenunciante
    {
        public Conexao Conexao { get; set; }

        public DaoDenunciante()
        {
            Conexao = new Conexao();
        }

        public Denunciante IncluirDenuncianteDb(Denunciante denunciante)
        {
            DaoUsuario daoUsuario = new DaoUsuario();
            daoUsuario.IncluirUsuarioDb(denunciante);

            using (SqlConnection conexao = Conexao.ConexaoDatabase())
            {
                conexao.Open();

                try
                {
                    SqlCommand comandoDML = new SqlCommand("SP_CadastrarDenunciante", conexao);
                    comandoDML.CommandType = CommandType.StoredProcedure;

                    comandoDML.Parameters.Add("@Email", SqlDbType.VarChar, 200);
                    comandoDML.Parameters["@Email"].Value = denunciante.Email;

                    comandoDML.Parameters.Add("@Nome", SqlDbType.VarChar, 200);
                    comandoDML.Parameters["@Nome"].Value = denunciante.Nome;

                    comandoDML.Parameters.Add("@Telefone", SqlDbType.VarChar, 200);
                    comandoDML.Parameters["@Telefone"].Value = denunciante.Telefone;

                    SqlDataReader dr = comandoDML.ExecuteReader();
                    bool consultarExistenciaDenunciante = dr.HasRows;

                    if (consultarExistenciaDenunciante)
                    {
                        while (dr.Read())
                        {
                            denunciante.IdUsuario = Convert.ToInt32(dr["USUARIO"]);
                            denunciante.Nome = Convert.ToString(dr["NOME"]);
                            denunciante.Telefone = Convert.ToString(dr["TELEFONE"]);
                            denunciante.Email = Convert.ToString(dr["EMAIL"]);
                            denunciante.Senha = Convert.ToString(dr["SENHA"]);
                        }
                    }

                    conexao.Close();
                    return denunciante;
                }
                catch (Exception e)
                {
                    SqlCommand comandoDML = new SqlCommand("SP_CadastrarLogSistema", conexao);
                    comandoDML.CommandType = CommandType.StoredProcedure;

                    comandoDML.Parameters.Add("@Descricao", SqlDbType.VarChar, 200);
                    comandoDML.Parameters["@Descricao"].Value = e.Message;
                    comandoDML.ExecuteNonQuery();

                    return null;
                }
            }
        }

        public string EditarDenuncianteDb(Denunciante denunciante)
        {
            using (SqlConnection conexao = Conexao.ConexaoDatabase())
            {
                conexao.Open();

                try
                {
                    SqlCommand comandoDML = new SqlCommand("SP_EditarDenunciante", conexao);
                    comandoDML.CommandType = CommandType.StoredProcedure;

                    comandoDML.Parameters.Add("@IdDenunciante", SqlDbType.Int);
                    comandoDML.Parameters["@IdDenunciante"].Value = denunciante.IdDenunciante;

                    comandoDML.Parameters.Add("@Nome", SqlDbType.VarChar, 200);
                    comandoDML.Parameters["@Nome"].Value = denunciante.Nome;

                    comandoDML.Parameters.Add("@Telefone", SqlDbType.VarChar, 200);
                    comandoDML.Parameters["@Telefone"].Value = denunciante.Telefone;

                    comandoDML.Parameters.Add("@Email", SqlDbType.VarChar, 200);
                    comandoDML.Parameters["@Email"].Value = denunciante.Email;

                    comandoDML.Parameters.Add("@Senha", SqlDbType.VarChar, 200);
                    comandoDML.Parameters["@Senha"].Value = denunciante.Senha;

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

        public string ExcluirDenuncianteDb(Denunciante denunciante)
        {
            using (SqlConnection conexao = Conexao.ConexaoDatabase())
            {
                conexao.Open();

                try
                {
                    SqlCommand comandoDML = new SqlCommand("SP_ExcluirDenunciante", conexao);
                    comandoDML.CommandType = CommandType.StoredProcedure;

                    comandoDML.Parameters.Add("@IdDenunciante", SqlDbType.Int);
                    comandoDML.Parameters["@IdDenunciante"].Value = denunciante.IdDenunciante;

                    comandoDML.Parameters.Add("@Email", SqlDbType.VarChar, 200);
                    comandoDML.Parameters["@Email"].Value = denunciante.Email;

                    comandoDML.Parameters.Add("@Situacao", SqlDbType.Int);
                    comandoDML.Parameters["@Situacao"].Value = denunciante.Situacao;

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

        public Denunciante SelecionarDenuncianteDb(int idDenunciante)
        {
            using (SqlConnection conexao = Conexao.ConexaoDatabase())
            {
                conexao.Open();

                Denunciante denunciante = new Denunciante();

                SqlCommand comandoDML = new SqlCommand("SP_SelecionarDenunciante", conexao);
                comandoDML.CommandType = CommandType.StoredProcedure;

                comandoDML.Parameters.Add("@IdDenunciante", SqlDbType.Int);
                comandoDML.Parameters["@IdDenunciante"].Value = idDenunciante;

                SqlDataReader dr = comandoDML.ExecuteReader();
                bool consultarExistenciaDenunciante = dr.HasRows;

                if (consultarExistenciaDenunciante)
                {
                    while (dr.Read())
                    {
                        denunciante.IdDenunciante = Convert.ToInt32(dr["IdDENUNCIANTE"]);
                        denunciante.Nome = Convert.ToString(dr["NOME"]);
                        denunciante.Telefone = Convert.ToString(dr["TELEFONE"]);
                        denunciante.Email = Convert.ToString(dr["EMAIL"]);
                        denunciante.Senha = Convert.ToString(dr["SENHA"]);
                    }
                }

                conexao.Close();
                return denunciante;
            }
        }

        public List<Denunciante> ListarDenunciantesDb()
        {
            using (SqlConnection conexao = Conexao.ConexaoDatabase())
            {
                conexao.Open();

                List<Denunciante> denunciantes = new List<Denunciante>();

                SqlCommand comandoDML = new SqlCommand("SP_ListarDenunciantes", conexao);
                comandoDML.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = comandoDML.ExecuteReader();

                while (dr.Read())
                {
                    int id = Convert.ToInt32(dr["IdDENUNCIANTE"]);
                    string nome = Convert.ToString(dr["NOME"]);
                    string telefone = Convert.ToString(dr["TELEFONE"]);
                    string email = Convert.ToString(dr["EMAIL"]);
                    string senha = Convert.ToString(dr["SENHA"]);

                    denunciantes.Add(new Denunciante(id, nome, telefone, email, senha));
                }

                conexao.Close();
                return denunciantes;
            }
        }        
    }
}

