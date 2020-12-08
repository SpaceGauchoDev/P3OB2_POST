using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Presentacion.Models;
using Dominio.Entidades;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MVC_Presentacion.Controllers
{
    public class BusquedaProyectosController : CommonController
    {
        private HttpClient cliente = new HttpClient();
        private HttpResponseMessage response = new HttpResponseMessage();
        private Uri proyectoUri = null;

        public BusquedaProyectosController()
        {
            cliente.BaseAddress = new Uri("http://localhost:63369");
            proyectoUri = new Uri("http://localhost:63369/api/proyectos");
            cliente.DefaultRequestHeaders.Accept.Clear();
            cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public ActionResult Index()
        {
            /*security check*/
            if (Session["tipoDeUsuario"] == null)
            {
                return RedirectToAction("Logout", "Common");
            }
            else if (Session["tipoDeUsuario"].ToString() == TiposDeUsuario.E_Nav.Solicitante.ToString())
            {
                return View("BusquedaPorSolicitantes");
            }
            else if (Session["tipoDeUsuario"].ToString() == TiposDeUsuario.E_Nav.Inversor.ToString())
            {
                return View("BusquedaPorInversores");
            }
            else
            {
                return RedirectToAction("Logout", "Common");
            }
        }
        
        [HttpPost]
        public ActionResult BusquedaPorInversores(BusquedaProyectosModel pBusquedaData)
        {
            //security check
            if (Session["tipoDeUsuario"] == null)
            {
                return RedirectToAction("Logout", "Common");
            }
            else if (Session["tipoDeUsuario"].ToString() != TiposDeUsuario.E_Nav.Inversor.ToString())
            {
                return RedirectToAction("Logout", "Common");
            }

            bool valido = true;

            // checkeamos que las fechas sean validas
            if (pBusquedaData.FechaFin != null &&
                pBusquedaData.FechaInicio != null)
            {
                if (pBusquedaData.FechaFin <= pBusquedaData.FechaInicio)
                {
                    ViewData["Mensaje"] = "Rango de fechas de presentacion invalido.";
                    valido = false;
                }
            }

            // checkeamos que el formato de la CI sea valido
            if (!String.IsNullOrEmpty(pBusquedaData.CISolicitante))
            {
                Inversor i = new Inversor();
                if (!i.ValidarCI(pBusquedaData.CISolicitante))
                {
                    ViewData["Mensaje"] = "Formato de CI invalido.";
                    valido = false;
                }
            }

            if (valido)
            {
                // ir a resultado de busqueda
                return RedirectToAction("ResultadosDeBusqueda", pBusquedaData);
            }
            else
            {
                // en caso de que hubieran errores en los datos ingresados recargamos esta vista
                return View();
            }
        }
        

        [HttpPost]
        public ActionResult BusquedaPorSolicitantes(BusquedaProyectosModel pBusquedaData)
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

            bool valido = true;

            // checkeamos que las fechas sean validas
            if (pBusquedaData.FechaFin != null &&
                pBusquedaData.FechaInicio != null)
            {
                if (pBusquedaData.FechaFin <= pBusquedaData.FechaInicio)
                {
                    ViewData["Mensaje"] = "Rango de fechas de presentacion invalido.";
                    valido = false;
                }
            }

            if (valido)
            {
                // agregar ID del solicitante actualmente registrado a los terminos de busqueda
                pBusquedaData.CISolicitante = Session["idUsuario"].ToString();
                // ir a resultado de busqueda
                return RedirectToAction("ResultadosDeBusqueda", pBusquedaData);
            }
            else
            {
                // en caso de que hubieran errores en los datos ingresados recargamos esta vista
                return View();
            }
        }


        public ActionResult ResultadosDeBusqueda(BusquedaProyectosModel pBusquedaData)
        {
            /*security check*/
            if (Session["tipoDeUsuario"] == null)
            {
                return RedirectToAction("Logout", "Common");
            }
            else if (Session["tipoDeUsuario"].ToString() == TiposDeUsuario.E_Nav.NoRegistrado.ToString())
            {
                return RedirectToAction("Logout", "Common");
            }

            string ruta = $"{proyectoUri}/busqueda/?" +
                            $"fechaInicio={pBusquedaData.FechaInicio}" +
                            $"&fechaFin={pBusquedaData.FechaFin}" +
                            $"&contieneEnTitulo={pBusquedaData.ContieneEnTitulo}" +
                            $"&contieneEnDescripcion={pBusquedaData.ContieneEnDescripcion}" +
                            $"&estado={pBusquedaData.Estado}" +
                            $"&montoMaximo={pBusquedaData.MontoMaximo}" +
                            $"&ciSolicitante={pBusquedaData.CISolicitante}" +
                            $"&tipoDeBusqueda={pBusquedaData.TipoDeBusqueda}";
            Uri uri = new Uri(ruta);

            var response = cliente.GetAsync(uri).Result;
            if (response.IsSuccessStatusCode)
            {
                ViewData["Mensaje"] = ruta + "Se encontraron resultados";
                
                var lista = response.Content.ReadAsAsync<IEnumerable<ItemResultadoBusquedaProyectoModel>>().Result;
                ViewBag.Mensaje = $"Se encontraron {lista.Count()} resultados";
                return View(lista);
            }
            else
            {
                ViewData["Mensaje"] = ruta + "No se encontraron resultados";
            }

            return View();
        }
    }
}