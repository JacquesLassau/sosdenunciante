using System.Web.Mvc;
using DLLsSosPernambucanas.BLL;
using DLLsSosPernambucanas.DML;
using DLLsSosPernambucanas.Infrastructure;
using System;

namespace SosDenunciante.Controllers
{
    public class AuthenticationController : Controller
    {
        #region Construtor        
        private BoUsuario _boUsuario;
        private BoDenunciante _boDenunciante;
        private BoToken _boToken;
        private Token _token;
        public AuthenticationController()
        {
            _boUsuario = new BoUsuario();
            _boDenunciante = new BoDenunciante();
            _boToken = new BoToken();
            _token = new Token();
        }
        #endregion

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Denunciante denunciante)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Denunciante usuDenunciante = _boDenunciante.IncluirDenunciante(denunciante);
                    if (usuDenunciante != null)
                    {
                        Session["sessaoId"] = usuDenunciante.IdUsuario;
                        Session["sessaoNome"] = usuDenunciante.Nome;
                        Session["sessaoTelefone"] = usuDenunciante.Telefone;
                        Session["sessaoEmail"] = usuDenunciante.Email;
                        Session["sessaoSenha"] = usuDenunciante.Senha;

                        return RedirectToAction("Index", "Denunciante");
                    }
                    else
                    {
                        TempData[Constantes.MENSAGEM_TEMP_DATA_TITULO] = Constantes.EMAIL_EXISTENTE_DENUNCIANTE;
                        TempData[Constantes.MENSAGEM_TEMP_DATA_DESCRICAO] = Constantes.EMAIL_EXISTENTE_DENUNCIANTE_DESCRICAO;
                        return RedirectToAction("Login");
                    }                   
                }
                catch (Exception ex)
                {
                    TempData[Constantes.MENSAGEM_TEMP_DATA_TITULO] = Constantes.ERRO_NAO_TRATADO;
                    TempData[Constantes.MENSAGEM_TEMP_DATA_DESCRICAO] = ex.Message;                    
                    return RedirectToAction("Login");
                }
            }
            else
            {
                TempData[Constantes.MENSAGEM_TEMP_DATA_TITULO] = Constantes.NAO_VALIDO;
                TempData[Constantes.MENSAGEM_TEMP_DATA_DESCRICAO] = Constantes.DESCRICAO_NAO_VALIDO;                
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Usuario usuario)
        {
            try
            {
                usuario.Tipo = Convert.ToInt32(Constantes.TipoUsuario.DENUNCIANTE);
                Usuario usuarioAutenticado = _boUsuario.AcessoUsuario(usuario);

                if (usuarioAutenticado != null)
                {
                    if (usuarioAutenticado.Tipo != Convert.ToInt32(Constantes.TipoUsuario.DENUNCIANTE))
                    {
                        TempData[Constantes.MENSAGEM_TEMP_DATA_TITULO] = Constantes.NAO_VALIDO;
                        TempData[Constantes.MENSAGEM_TEMP_DATA_DESCRICAO] = Constantes.FALHA_LOGIN;
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        Session["sessaoId"] = usuarioAutenticado.IdUsuario;
                        Session["sessaoEmail"] = usuarioAutenticado.Email;
                        return RedirectToAction("Index", "Denunciante");
                    }
                }
                else
                {
                    TempData[Constantes.MENSAGEM_TEMP_DATA_TITULO] = Constantes.NAO_VALIDO;
                    TempData[Constantes.MENSAGEM_TEMP_DATA_DESCRICAO] = Constantes.FALHA_LOGIN;
                    return RedirectToAction("Login");
                }
            }
            catch(Exception e)
            {
                TempData[Constantes.MENSAGEM_TEMP_DATA_TITULO] = Constantes.ERRO_NAO_TRATADO;
                TempData[Constantes.MENSAGEM_TEMP_DATA_DESCRICAO] = e.Message;
                return RedirectToAction("Login");
            }            
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult PasswordRecouver()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PasswordRecouver(Usuario usuario)
        {
            try
            {
                if (!_boUsuario.VerificarEmailUsuario(Convert.ToInt32(Constantes.TipoUsuario.DENUNCIANTE), usuario.Email))
                {
                    TempData[Constantes.MENSAGEM_TEMP_DATA_TITULO] = Constantes.EMAIL_NAO_ENCONTRADO;
                    TempData[Constantes.MENSAGEM_TEMP_DATA_DESCRICAO] = Constantes.EMAIL_NAO_ENCONTRADO_DESCRICAO;
                }
                else
                {
                    string token = Utilidades.StrTokenMD5(usuario.Email);

                    //Condição incluída para que sejam realizados testes locais. 
                    //Propriedade "Host" incluí a porta do servidor local.
                    string dominio = (Request.IsLocal) ? Request.Url.Authority : Request.Url.Host;
                    // ---------------------------------------------------------------------------

                    string url = dominio + "/Authentication/PasswordEdit?StrToken=" + token;

                    _token.Email = usuario.Email;
                    _token.UrlBase = url;
                    _token.StrToken = token;
                    _boToken.IncluirToken(_token);

                    Utilidades.EnviaEmailRecuperaSenha(usuario.Email, url);

                    TempData[Constantes.MENSAGEM_TEMP_DATA_TITULO] = Constantes.EMAIL_ENVIADO_SUCESSO;
                    TempData[Constantes.MENSAGEM_TEMP_DATA_DESCRICAO] = Constantes.EMAIL_ENVIADO_SUCESSO_DESCRICAO;
                }
            }
            catch (Exception e)
            {
                TempData[Constantes.MENSAGEM_TEMP_DATA_TITULO] = Constantes.ERRO_NAO_TRATADO;
                TempData[Constantes.MENSAGEM_TEMP_DATA_DESCRICAO] = e.Message;
                return RedirectToAction("Login");
            }

            return RedirectToAction("Login");
        }

        [HttpGet]        
        public ActionResult PasswordEdit()
        {
            try
            {
                string token = Request["StrToken"];
                Session["StrToken"] = token;

                if (string.IsNullOrEmpty(_boToken.BuscarValidadeToken(token)))
                {
                    TempData[Constantes.MENSAGEM_TEMP_DATA_TITULO] = Constantes.LINK_EXPIRADO;
                    TempData[Constantes.MENSAGEM_TEMP_DATA_DESCRICAO] = Constantes.LINK_EXPIRADO_DESCRICAO;
                    return RedirectToAction("Login");
                }
                else
                {
                    _boToken.GravarAcessoToken(token);

                    //Ajuste pontual para uso da hospedagem com protocolo HTTP.
                    UriBuilder builder = new UriBuilder();
                    builder.Scheme = "http";                    
                    // --------------------------------------------------------

                    return View();

                }                
            }
            catch(Exception e)
            {
                TempData[Constantes.MENSAGEM_TEMP_DATA_TITULO] = Constantes.ERRO_NAO_TRATADO;
                TempData[Constantes.MENSAGEM_TEMP_DATA_DESCRICAO] = e.Message;
                return RedirectToAction("Login");
            }            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PasswordEdit(Usuario usuario)
        {
            try
            {
                string token = Session["StrToken"].ToString();
                usuario.Email = _boToken.BuscarEmailToken(token);

                if (!string.IsNullOrEmpty(usuario.Email))
                {
                    usuario.Tipo = Convert.ToInt32(Constantes.TipoUsuario.DENUNCIANTE);
                    _boUsuario.AlterarSenhaUsuario(usuario);
                    TempData[Constantes.MENSAGEM_TEMP_DATA_TITULO] = Constantes.NOVA_SENHA_SUCESSO;
                    TempData[Constantes.MENSAGEM_TEMP_DATA_DESCRICAO] = Constantes.NOVA_SENHA_SUCESSO_DESCRICAO;                    
                }
                else
                {
                    TempData[Constantes.MENSAGEM_TEMP_DATA_TITULO] = Constantes.NAO_VALIDO;
                    TempData[Constantes.MENSAGEM_TEMP_DATA_DESCRICAO] = Constantes.ERRO_NAO_TRATADO;                    
                }
                
            }
            catch (Exception e)
            {
                TempData[Constantes.MENSAGEM_TEMP_DATA_TITULO] = Constantes.ERRO_NAO_TRATADO;
                TempData[Constantes.MENSAGEM_TEMP_DATA_DESCRICAO] = e.Message;                
            }

            return RedirectToAction("Login");
        }
    }
}