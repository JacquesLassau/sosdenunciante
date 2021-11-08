using System.Data.SqlClient;

namespace DLLsSosPernambucanas.DAL
{
    public class Conexao
    {
        private const string stringConn = @"";
        public SqlConnection ConexaoDatabase()
        {
            return new SqlConnection(stringConn);
        }
    }
}
