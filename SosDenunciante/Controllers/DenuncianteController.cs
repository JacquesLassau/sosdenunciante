using System.Web.Mvc;
using DLLsSosPernambucanas.BLL;
using DLLsSosPernambucanas.DML;
using SosDenunciante.Models;

namespace SosDenunciante.Controllers
{
    public class DenuncianteController : Controller
    {
        #region Construtor      
        private BoLocalApoio _boLocal;
        private BoRegistroLigacao _boRegistroLigacao;
        private LocalApoioViewModelDML _localApoioViewModelDML;        

        public DenuncianteController()
        {
            _boLocal = new BoLocalApoio();
            _boRegistroLigacao = new BoRegistroLigacao();
            _localApoioViewModelDML = new LocalApoioViewModelDML();
        }
        #endregion

        public ActionResult Index()
        {
            if (UsuarioValido.ValidUser())
                return View();
            else
                return RedirectToAction("Login", "Authentication");
        }

        public ActionResult Help()
        {
            if (UsuarioValido.ValidUser())
                return View();
            else
                return RedirectToAction("Login", "Authentication");
        }

        public ActionResult LocalHelp()
        {
            if (UsuarioValido.ValidUser())
                return View();
            else
                return RedirectToAction("Login", "Authentication");
        }     
        
        public JsonResult LigacaoAcionada(string telDiscado)
        {
            string emailUsuario = Session["sessaoEmail"].ToString();
            return Json(_boRegistroLigacao.CadastraRegistroLigacao(telDiscado, emailUsuario), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CarregaMapa()
        {
            LocalApoioViewModelDML listaLocaisApoio = _localApoioViewModelDML.ConvertToListLocals(_boLocal.ListaLocaisApoio());
            return Json(listaLocaisApoio, JsonRequestBehavior.AllowGet);
        }
    }
}