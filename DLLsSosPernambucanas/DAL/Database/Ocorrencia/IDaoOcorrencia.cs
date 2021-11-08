
using DLLsSosPernambucanas.DML;
using System.Collections.Generic;

namespace DLLsSosPernambucanas.DAL
{
    public interface IDaoOcorrencia
    {
        List<Ocorrencia> ListarOcorrenciasDb();
        bool AlterarSituacaoLigacaoDb(int idOcorrencia, int situacaoLigacaoOcorrencia);

        Ocorrencia ConsultarOcorrenciaDb(int idOcorrencia);
    }
}
