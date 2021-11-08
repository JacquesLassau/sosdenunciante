using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using DLLsSosPernambucanas.DML;

namespace DLLsSosPernambucanas.DAL
{
    public class DaoOcorrencia: IDaoOcorrencia
    {
        public Conexao Conexao { get; set; }

        public DaoOcorrencia()
        {
            Conexao = new Conexao();
        }

        public List<Ocorrencia> ListarOcorrenciasDb()
        {
            using (SqlConnection conexao = Conexao.ConexaoDatabase())
            {
                conexao.Open();

                List<Ocorrencia> ocorrencias = new List<Ocorrencia>();

                SqlCommand comandoDML = new SqlCommand("SP_ListarOcorrencias", conexao);
                comandoDML.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = comandoDML.ExecuteReader();

                while (dr.Read())
                {
                    int id = Convert.ToInt32(dr["IdOCORRENCIA"]);
                    string nome = Convert.ToString(dr["NOMeDENUNCIANTE"]);
                    string telefone = Convert.ToString(dr["TELEFONeDENUNCIANTE"]);
                    int numero = Convert.ToInt32(dr["NUMERoTELEFONeLIGACAO"]);
                    string descricao = Convert.ToString(dr["DESCRICAoTELEFONeLIGACAO"]);                    
                    DateTime dataHora = Convert.ToDateTime(dr["DATaHORaLIGACAO"]);
                    int situacao = Convert.ToInt32(dr["SITUACAoLIGACAO"]);                    

                    ocorrencias.Add(new Ocorrencia(id, nome, telefone, numero, descricao, dataHora, situacao));
                }

                conexao.Close();
                return ocorrencias;
            }
        }

        public bool AlterarSituacaoLigacaoDb(int idOcorrencia, int situacaoLigacaoOcorrencia)
        {
            using (SqlConnection conexao = Conexao.ConexaoDatabase())
            {
                conexao.Open();

                SqlCommand comandoDML = new SqlCommand("SP_AlterarSituacaoLigacao", conexao);
                comandoDML.CommandType = CommandType.StoredProcedure;

                comandoDML.Parameters.Add("@IdOcorrencia", SqlDbType.Int);
                comandoDML.Parameters["@IdOcorrencia"].Value = idOcorrencia;

                comandoDML.Parameters.Add("@SituacaoLigacao", SqlDbType.Int);
                comandoDML.Parameters["@SituacaoLigacao"].Value = situacaoLigacaoOcorrencia;

                SqlDataReader dr = comandoDML.ExecuteReader();
                bool ocorrenciaAlterada = dr.HasRows;
                                
                conexao.Close();
                return ocorrenciaAlterada;
            }
        }

        public bool CadastrarOcorrenciaDenuncianteDb(int idOcorrencia, string descricaoOcorrencia, int situacaoLigacaoOcorrencia)
        {
            using (SqlConnection conexao = Conexao.ConexaoDatabase())
            {
                conexao.Open();

                SqlCommand comandoDML = new SqlCommand("SP_CadastrarOcorrenciaDenunciante", conexao);
                comandoDML.CommandType = CommandType.StoredProcedure;

                comandoDML.Parameters.Add("@IdOcorrencia", SqlDbType.Int);
                comandoDML.Parameters["@IdOcorrencia"].Value = idOcorrencia;

                comandoDML.Parameters.Add("@DescricaoOcorrencia", SqlDbType.VarChar, 8000);
                comandoDML.Parameters["@DescricaoOcorrencia"].Value = descricaoOcorrencia;

                comandoDML.Parameters.Add("@SituacaoLigacao", SqlDbType.Int);
                comandoDML.Parameters["@SituacaoLigacao"].Value = situacaoLigacaoOcorrencia;

                SqlDataReader dr = comandoDML.ExecuteReader();
                bool ocorrenciaAlterada = dr.HasRows;

                conexao.Close();
                return ocorrenciaAlterada;
            }
        }

        public Ocorrencia ConsultarOcorrenciaDb(int idOcorrencia)
        {
            using (SqlConnection conexao = Conexao.ConexaoDatabase())
            {
                conexao.Open();

                List<Ocorrencia> ocorrencias = new List<Ocorrencia>();

                SqlCommand comandoDML = new SqlCommand("SP_ConsultarOcorrencia", conexao);

                comandoDML.Parameters.Add("@IdOcorrencia", SqlDbType.Int);
                comandoDML.Parameters["@IdOcorrencia"].Value = idOcorrencia;

                comandoDML.CommandType = CommandType.StoredProcedure;               

                SqlDataReader dr = comandoDML.ExecuteReader();
                bool consultaOcorrencia = dr.HasRows;

                Ocorrencia ocorrencia = new Ocorrencia();
                if (consultaOcorrencia)
                {
                    while (dr.Read())
                    {                        
                        string nome = Convert.ToString(dr["NOMeDENUNCIANTE"]);
                        string telefone = Convert.ToString(dr["TELEFONeDENUNCIANTE"]);
                        int numero = Convert.ToInt32(dr["NUMERoTELEFONeLIGACAO"]);
                        string descricao = Convert.ToString(dr["DESCRICAoTELEFONeLIGACAO"]);
                        string descricaoOcorrencia = Convert.ToString(dr["DESCRICAoOCORRENCIaLIGACAO"]);                                                

                        ocorrencia = new Ocorrencia(nome, telefone, numero, descricao, descricaoOcorrencia);
                    }
                }
                else                
                    ocorrencia = null;
                                

                conexao.Close();
                return ocorrencia;
            }
        }
    }
}
