namespace DLLsSosPernambucanas.DML
{
    public class RegistroLigacao
    {
        public string Numero { get; set; }
        public string Descricao { get; set; }

        public RegistroLigacao() { }
        public RegistroLigacao(string numero, string descricao)
        {
            this.Numero = numero;
            this.Descricao = descricao;            
        }
    }
}
