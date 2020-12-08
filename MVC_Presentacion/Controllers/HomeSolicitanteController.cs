using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Presentacion.ServicioImportacionesRef;

namespace MVC_Presentacion.Controllers
{
    public class HomeSolicitanteController : CommonController
    {
        public ActionResult Index()
        {
            /*security check*/
            if (Session["tipoDeUsuario"] == null)
            {
                return RedirectToAction("Logout", "Common");
            }
            else if (Session["tipoDeUsuario"].ToString() != TiposDeUsuario.E_Nav.Solicitante.ToString())
            {
                return RedirectToAction("Logout", "Common");
            }



            return View("VerHomeSolicitante");
        }

        public ActionResult VerHomeSolicitante()
        {
            /*security check*/
            if (Session["tipoDeUsuario"] == null)
            {
                return RedirectToAction("Logout", "Common");
            }
            else if (Session["tipoDeUsuario"].ToString() != TiposDeUsuario.E_Nav.Solicitante.ToString())
            {
                return RedirectToAction("Logout", "Common");
            }


            return View();
        }

        public ActionResult BuscarProyectos()
        {
            /*security check*/
            if (Session["tipoDeUsuario"] == null)
            {
                return RedirectToAction("Logout", "Common");
            }
            else if (Session["tipoDeUsuario"].ToString() != TiposDeUsuario.E_Nav.Solicitante.ToString())
            {
                return RedirectToAction("Logout", "Common");
            }


            //TODOMDA: agregar vista de busqueda con filtros para solicitantes
            return RedirectToAction("Index", "BusquedaProyectos");
        }
    }
}