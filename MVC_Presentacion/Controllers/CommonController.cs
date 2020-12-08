using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Presentacion.ServicioImportacionesRef;

namespace MVC_Presentacion.Controllers
{
    public class CommonController : Controller
    {

        public ActionResult ImportarUsuarios()
        {
            ServicioImportacionClient servicioSolicitantesClient = new ServicioImportacionClient();
            servicioSolicitantesClient.Open();
            var solicitantes = servicioSolicitantesClient.ImportarSolicitantes();

            return View(solicitantes);
        }

        public ActionResult ImportarProyectos()
        {
            ServicioImportacionClient servicioProyectosClient = new ServicioImportacionClient();
            servicioProyectosClient.Open();
            var proyectos = servicioProyectosClient.ImportarProyectos();

            return View(proyectos);
        }

        public ActionResult VolverAHome()
        {
            if (Session["tipoDeUsuario"] == null)
            {
                return RedirectToAction("Index", "HomeSinRegistrar");
            }

            if (Session["tipoDeUsuario"].ToString() == TiposDeUsuario.E_Nav.Inversor.ToString())
            {
                return RedirectToAction("Index", "HomeInversor");
            }
            else if (Session["tipoDeUsuario"].ToString() == TiposDeUsuario.E_Nav.Solicitante.ToString())
            {
                return RedirectToAction("Index", "HomeSolicitante");
            }
            else
            {
                return RedirectToAction("Index", "HomeSinRegistrar");
            }
        }

        public ActionResult Logout()
        {
            Session["tipoDeUsuario"] = TiposDeUsuario.E_Nav.NoRegistrado;
            Session["usuario"] = null;
            Session["idUsuario"] = null;
            return RedirectToAction("Index", "HomeSinRegistrar");
        }
    }
}