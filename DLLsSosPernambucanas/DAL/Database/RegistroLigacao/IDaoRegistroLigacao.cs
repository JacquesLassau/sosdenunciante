using DLLsSosPernambucanas.DML;

namespace DLLsSosPernambucanas.DAL
{
    public interface IDaoRegistroLigacao
    {
        bool CadastraRegistroLigacaoDb(RegistroLigacao registroLigacao, string emailUsuario);        
    }
}
