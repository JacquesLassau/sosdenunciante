using DLLsSosPernambucanas.DML;

namespace DLLsSosPernambucanas.DAL
{
    public interface IDaoToken
    {
        void IncluirTokenDb(Token token);
        void GravarAcessoTokenDb(string token);
        string BuscarEmailTokenDb(string token);
        string BuscarValidadeTokenDb(string pToken);
    }
}
