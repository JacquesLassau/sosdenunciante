using System;

namespace DLLsSosPernambucanas.DML
{
    public class Token: Usuario
    {
        public string UrlBase { get; set; }
        public string StrToken { get; set; }
        public DateTime DataAcesso { get; set; }
    }
}
