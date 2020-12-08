using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Presentacion.Models;
using Datos.Repositorios;
using Dominio.Entidades;

namespace MVC_Presentacion.Controllers
{
    public class LoginController : CommonController
    {
        // GET: Login
        public ActionResult Index()
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

        //TODOMDA: REMOVE ON DELIVER
        public ActionResult TestingLogInInversor()
        {
            // ingresar como inversor
            Session["tipoDeUsuario"] = TiposDeUsuario.E_Nav.Inversor;
            Session["idUsuario"] = "96712031";
            return RedirectToAction("Index", "HomeInversor");
        }

        //TODOMDA: REMOVE ON DELIVER
        public ActionResult TestingLogInSolicitante()
        {
            // ingresar como solicitante
            Session["tipoDeUsuario"] = TiposDeUsuario.E_Nav.Solicitante;
            Session["idUsuario"] = "54776025";
            return RedirectToAction("Index", "HomeSolicitante");
        }

        [HttpPost]
        public ActionResult Index(LoginDataModel pLoginData)
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

            RepositorioSolicitante rS = new RepositorioSolicitante();
            RepositorioInversor rI = new RepositorioInversor();

            Inversor i = rI.LoginAttempt(pLoginData.NombreDeUsuario, pLoginData.Pass);
            Solicitante s = rS.LoginAttempt(pLoginData.NombreDeUsuario, pLoginData.Pass);

            if (s != null )
            {
                // ingresar como solicitante
                Session["tipoDeUsuario"] = TiposDeUsuario.E_Nav.Solicitante;
                Session["idUsuario"] = pLoginData.NombreDeUsuario;

                if (s.TienePassTemporal)
                {
                    return RedirectToAction("Index", "CambiarPass");
                }
                else
                {
                    return RedirectToAction("Index", "HomeSolicitante");
                }
            }
            else if (i != null)
            {
                // ingresar como inversor
                Session["tipoDeUsuario"] = TiposDeUsuario.E_Nav.Inversor;
                Session["idUsuario"] = pLoginData.NombreDeUsuario;
                return RedirectToAction("Index", "HomeInversor");
            }
            else
            {
                Session["tipoDeUsuario"] = TiposDeUsuario.E_Nav.NoRegistrado;
                ViewData["Mensaje"] = "Nombre de usuario y/o contraseña incorrectos.";
            }

            return View();
        }

    }
}