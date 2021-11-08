using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using DLLsSosPernambucanas.DML;

namespace DLLsSosPernambucanas.DAL
{
    public class DaoLocalApoio: IDaoLocalApoio
    {
        public Conexao Conexao { get; set; }

        public DaoLocalApoio()
        {
            Conexao = new Conexao();
        }        

        public List<LocalApoio> ListarLocaisApoioDb()
        {
            using (SqlConnection conexao = Conexao.ConexaoDatabase())
            {
                conexao.Open();

                List<LocalApoio> locaisApoio = new List<LocalApoio>();

                SqlCommand comandoDML = new SqlCommand("SP_ListarLocaisApoio", conexao);
                comandoDML.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = comandoDML.ExecuteReader();

                while (dr.Read())
                {
                    int id = Convert.ToInt32(dr["IdLOCAL"]);
                    string latitude = Convert.ToString(dr["LATITUDE"]);
                    string longitude = Convert.ToString(dr["LONGITUDE"]);
                    string logradouro = Convert.ToString(dr["LOGRADOURO"]);
                    string numero = Convert.ToString(dr["NUMERO"]);
                    string bairro = Convert.ToString(dr["BAIRRO"]);
                    string cidade = Convert.ToString(dr["CIDADE"]);
                    string estado = Convert.ToString(dr["ESTADO"]);
                    string cep = Convert.ToString(dr["CEP"]);
                    string telefone = Convert.ToString(dr["TELEFONE"]);

                    locaisApoio.Add(new LocalApoio(id, latitude, longitude, logradouro, numero, bairro, cidade, estado, cep, telefone));
                }

                conexao.Close();
                return locaisApoio;
            }
        }

        public string IncluirLocarApoioDb(LocalApoio localApoio)
        {
            using (SqlConnection conexao = Conexao.ConexaoDatabase())
            {
                conexao.Open();

                try
                {
                    SqlCommand comandoDML = new SqlCommand("SP_CadastrarLocalApoio", conexao);
                    comandoDML.CommandType = CommandType.StoredProcedure;

                    comandoDML.Parameters.Add("@Latitude", SqlDbType.VarChar, 200);
                    comandoDML.Parameters["@latitude"].Value = localApoio.Latitude;

                    comandoDML.Parameters.Add("@Longitude", SqlDbType.VarChar, 200);
                    comandoDML.Parameters["@Longitude"].Value = localApoio.Longitude;

                    comandoDML.Parameters.Add("@Logradouro", SqlDbType.VarChar, 200);
                    comandoDML.Parameters["@Logradouro"].Value = localApoio.Endereco;

                    comandoDML.Parameters.Add("@Numero", SqlDbType.VarChar, 200);
                    comandoDML.Parameters["@Numero"].IsNullable = true;
                    comandoDML.Parameters["@Numero"].Value = localApoio.NumeroEndereco;

                    comandoDML.Parameters.Add("@Bairro", SqlDbType.VarChar, 200);
                    comandoDML.Parameters["@Bairro"].Value = localApoio.Bairro;

                    comandoDML.Parameters.Add("@Cidade", SqlDbType.VarChar, 200);
                    comandoDML.Parameters["@Cidade"].Value = localApoio.Cidade;

                    comandoDML.Parameters.Add("@Estado", SqlDbType.VarChar, 2);
                    comandoDML.Parameters["@Estado"].Value = localApoio.Estado;

                    comandoDML.Parameters.Add("@Cep", SqlDbType.VarChar, 9);
                    comandoDML.Parameters["@Cep"].Value = localApoio.Cep;

                    comandoDML.Parameters.Add("@Telefone", SqlDbType.VarChar, 200);
                    comandoDML.Parameters["@Telefone"].Value = localApoio.Telefone;

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

        public string ExcluirLocarApoioDb(int idLocal)
        {
            using (SqlConnection conexao = Conexao.ConexaoDatabase())
            {
                conexao.Open();

                try
                {
                    SqlCommand comandoDML = new SqlCommand("SP_ExcluirLocalApoio", conexao);
                    comandoDML.CommandType = CommandType.StoredProcedure;

                    comandoDML.Parameters.Add("@IdLocal", SqlDbType.Int);
                    comandoDML.Parameters["@IdLocal"].Value = idLocal;

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
    }
}


