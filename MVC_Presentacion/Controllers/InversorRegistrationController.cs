using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Presentacion.Models;
using Dominio.Entidades;
using Datos.Repositorios;

namespace MVC_Presentacion.Controllers
{
    public class InversorRegistrationController : Controller
    {
        
        // GET: InversorRegistration
        public ActionResult Index()
        {
            return View();
        }
        

        [HttpPost]
        public ActionResult Index(InversorRegistrationModel pRegistrationData)
        {
            RepositorioSolicitante rS = new RepositorioSolicitante();
            RepositorioInversor rI = new RepositorioInversor();

            Inversor inversor = new Inversor
            {
                IdUsuario = int.Parse(pRegistrationData.NombreDeUsuario),
                Nombre = pRegistrationData.Nombre,
                Apellido = pRegistrationData.Apellido,
                Pass = pRegistrationData.Pass,
                FechaDeNacimiento = pRegistrationData.FechaDeNacimiento,
                Email = pRegistrationData.Email,
                Cell = pRegistrationData.Cell,
                TienePassTemporal = false,
                MaxInvPorProyecto = pRegistrationData.MaxInvPorProyecto,
                PresentacionInversor = pRegistrationData.PresentacionInversor
            };

            if (rS.FindById(int.Parse(pRegistrationData.NombreDeUsuario)) != null)
            {
                ViewData["Mensaje"] = "Solicitante con misma CI ya existe en el sistema.";
            }
            else if (rS.ExistsByEmail(pRegistrationData.Email) || rI.ExistsByEmail(pRegistrationData.Email))
            {
                ViewData["Mensaje"] = "Usuario con el mismo Email ya existe en el sistema.";
            }
            else if (!inversor.ValidarParaRepositorio())
            {
                ViewData["Mensaje"] = "Uno o mas campos incorrectos.";
            }
            else
            {
                if (rI.Add(inversor))
                {
                    ViewData["Mensaje"] = "Inversor registrado correctamente.";
                }
                else
                {
                    ViewData["Mensaje"] = "Error de ingreso, intente nuevamente.";
                }
            }

            return View();
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
    }
}