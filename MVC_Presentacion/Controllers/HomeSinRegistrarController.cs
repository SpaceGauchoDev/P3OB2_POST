using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Presentacion.ServicioImportacionesRef;

namespace MVC_Presentacion.Controllers
{
    public class HomeSinRegistrarController : CommonController
    {
        // GET: HomeSinRegistrar
        public ActionResult Index()
        {
            // para cuando entra por primera vez a la pagina
            if (Session["tipoDeUsuario"] == null)
            {
                Session["tipoDeUsuario"] = TiposDeUsuario.E_Nav.NoRegistrado;
            }
            return View("VerHomeSinRegistrar");
        }

        public ActionResult VerHomeSinRegistrar()
        {
            /*security check*/
            if (Session["tipoDeUsuario"] == null)
            {
                return RedirectToAction("Logout", "Common");
            }
            else if (Session["tipoDeUsuario"].ToString() != TiposDeUsuario.E_Nav.NoRegistrado.ToString())
            {
                return RedirectToAction("Logout", "Common");
            }


            return View();
        }

        public ActionResult IrARegistrarInversor()
        {
            /*security check*/
            if (Session["tipoDeUsuario"] == null)
            {
                return RedirectToAction("Logout", "Common");
            }
            else if (Session["tipoDeUsuario"].ToString() != TiposDeUsuario.E_Nav.NoRegistrado.ToString())
            {
                return RedirectToAction("Logout", "Common");
            }


            return RedirectToAction("Index", "InversorRegistration");
        }

        public ActionResult IrALogin()
        {
            /*security check*/
            if (Session["tipoDeUsuario"] == null)
            {
                return RedirectToAction("Logout", "Common");
            }
            else if (Session["tipoDeUsuario"].ToString() != TiposDeUsuario.E_Nav.NoRegistrado.ToString())
            {
                return RedirectToAction("Logout", "Common");
            }



            return RedirectToAction("Index", "Login");
        }
    }
}