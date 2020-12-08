using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Datos.Repositorios;
using MVC_Presentacion.Models;
using Dominio.Entidades;

namespace MVC_Presentacion.Controllers
{
    public class CambiarPassController : CommonController
    {
        // GET: CambiarPass
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(CambiarPassModel pCambiarPassData)
        {
            if (Session["tipoDeUsuario"].ToString() == TiposDeUsuario.E_Nav.Solicitante.ToString())
            {
                RepositorioSolicitante rS = new RepositorioSolicitante();
                Solicitante s = rS.FindById(int.Parse(Session["idUsuario"].ToString()));

                if (s == null) {
                    return RedirectToAction("Index", "HomeSinRegistrar");
                }
                else if (s.Pass != pCambiarPassData.Pass)
                {
                    if (s.ValidarContrasenia(pCambiarPassData.Pass))
                    {
                        s.Pass = pCambiarPassData.Pass;
                        s.TienePassTemporal = false;
                        if (rS.Update(s))
                        {
                            // si el cambio de contraseña es exitoso lo deslogeamos y que se logee nuevamente
                            return RedirectToAction("Logout", "Common");
                            //ViewData["Mensaje"] = "Contraseña cambiada correctamente.";
                        }
                        else
                        {
                            ViewData["Mensaje"] = "Error al interactuar con la base de datos.";
                        }
                    }
                    else
                    {
                        ViewData["Mensaje"] = "Contraseña en formato inválido.";
                    }
                }
                else
                {
                    ViewData["Mensaje"] = "Contraseña nueva no puede ser igual a contraseña anterior.";
                }
            }
            else if (Session["tipoDeUsuario"].ToString() == TiposDeUsuario.E_Nav.Inversor.ToString())
            {
                RepositorioInversor rI = new RepositorioInversor();
                Inversor i = rI.FindById(int.Parse(Session["idUsuario"].ToString()));

                if (i == null)
                {
                    return RedirectToAction("Index", "HomeSinRegistrar");
                }
                else if (i.Pass != pCambiarPassData.Pass)
                {
                    if (i.ValidarContrasenia(pCambiarPassData.Pass))
                    {
                        i.Pass = pCambiarPassData.Pass;
                        i.TienePassTemporal = false;
                        if (rI.Update(i))
                        {
                            // si el cambio de contraseña es exitoso lo deslogeamos y que se logee nuevamente
                            return RedirectToAction("Logout", "Common");
                        }
                        else
                        {
                            ViewData["Mensaje"] = "Error al interactuar con la base de datos.";
                        }
                    }
                    else
                    {
                        ViewData["Mensaje"] = "Contraseña en formato inválido.";
                    }
                }
                else
                {
                    ViewData["Mensaje"] = "Contraseña nueva no puede ser igual a contraseña anterior.";
                }
            }
            else
            {
                return RedirectToAction("Index", "HomeSinRegistrar");
            }

            return View();
        }











    }
}