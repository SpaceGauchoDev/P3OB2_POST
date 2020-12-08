using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Presentacion.ServicioImportacionesRef;

namespace MVC_Presentacion.Controllers
{
    public class HomeInversorController : CommonController
    {
        public ActionResult Index()
        {
            /*security check*/
            if (Session["tipoDeUsuario"] == null)
            {
                return RedirectToAction("Logout", "Common");
            }
            else if (Session["tipoDeUsuario"].ToString() != TiposDeUsuario.E_Nav.Inversor.ToString())
            {
                return RedirectToAction("Logout", "Common");
            }

            return View("VerHomeInversor");
        }

        public ActionResult VerHomeInversor()
        {
            /*security check*/
            if (Session["tipoDeUsuario"] == null)
            {
                return RedirectToAction("Logout", "Common");
            }
            else if (Session["tipoDeUsuario"].ToString() != TiposDeUsuario.E_Nav.Inversor.ToString())
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
            else if (Session["tipoDeUsuario"].ToString() != TiposDeUsuario.E_Nav.Inversor.ToString())
            {
                return RedirectToAction("Logout", "Common");
            }

            //TODOMDA: agregar vista de busqueda con filtros para inversores
            return RedirectToAction("Index", "BusquedaProyectos");
        }

        public ActionResult VerFinanciaciones()
        {
            /*security check*/
            if (Session["tipoDeUsuario"] == null)
            {
                return RedirectToAction("Logout", "Common");
            }
            else if (Session["tipoDeUsuario"].ToString() != TiposDeUsuario.E_Nav.Inversor.ToString())
            {
                return RedirectToAction("Logout", "Common");
            }



            //TODOMDA: agregar vista de financiaciones de este inversor
            return RedirectToAction("Index", "VerFinanciaciones");
        }

    }
}