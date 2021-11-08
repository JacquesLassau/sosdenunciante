using DLLsSosPernambucanas.DML;
using System.Collections.Generic;

namespace DLLsSosPernambucanas.DAL
{
    public interface IDaoDenunciante
    {
        Denunciante IncluirDenuncianteDb(Denunciante denunciante);
        Denunciante SelecionarDenuncianteDb(int idDenunciante);
        string EditarDenuncianteDb(Denunciante denunciante);
        List<Denunciante> ListarDenunciantesDb();
        string ExcluirDenuncianteDb(Denunciante denunciante);
    }
}
