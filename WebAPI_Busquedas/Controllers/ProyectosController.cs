using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Datos.Repositorios;
using Dominio.Entidades;

namespace WebAPI_Busquedas.Controllers
{
    [RoutePrefix("api/Proyectos")]
    public class ProyectosController : ApiController
    {
        private RepositorioProyecto rP = new RepositorioProyecto();

        /*
        // GET: api/Proyectos
        [Route("")]
        public IHttpActionResult Get()
        {
            var proyectos = rP.FindAll();
            if (proyectos != null)
            {
                return Ok(proyectos);
            }
            else
            {
                return NotFound();
            }

        }

        
        // GET: api/Proyectos/5
        [Route("{id:int}", Name = "GetById")]
        public IHttpActionResult Get(int id)
        {
            var proyectos = rP.FindById(id);
            if (proyectos != null)
            {
                return Ok(proyectos);
            }
            else
            {
                return NotFound();
            }
        }

        
        
        // POST: api/Proyectos
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Proyectos/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Proyectos/5
        public void Delete(int id)
        {
        }
        */

        [HttpGet]
        [Route("busqueda")]
        public IHttpActionResult Buscar([FromUri] Models.BusquedaApiModel datos)
        {
            if (String.IsNullOrEmpty(datos.TipoDeBusqueda))
            {
                return BadRequest("Tipo de busqueda invalido debe ser 'or' o 'and' ");
            }
            else if (datos.TipoDeBusqueda != "or" && datos.TipoDeBusqueda != "and")
            {
                return BadRequest("Tipo de busqueda invalido debe ser 'or' o 'and' ");
            }

            IEnumerable<Proyecto> resultado = null;

            if (datos.TipoDeBusqueda == "and")
            {
                //Búsqueda por refinaciones sucesivas (AND)
                resultado = rP.BuscarAnd(   datos.FechaInicio,
                                            datos.FechaFin,
                                            datos.ContieneEnTitulo,
                                            datos.ContieneEnDescripcion,
                                            datos.Estado,
                                            datos.MontoMaximo,
                                            datos.CISolicitante);
            }
            else
            {
                //Búsqueda por cualquier match sucesivas (OR)
                resultado = rP.BuscarOr(    datos.FechaInicio,
                                            datos.FechaFin,
                                            datos.ContieneEnTitulo,
                                            datos.ContieneEnDescripcion,
                                            datos.Estado,
                                            datos.MontoMaximo,
                                            datos.CISolicitante);
            }

            if (resultado != null)
            {
                return Ok(resultado.Select(p => new Models.ProyectoModel
                {
                    Estado = p.Estado,
                    TipoDeEquipo = p.TipoDeEquipo,
                    Titulo = p.Titulo,
                    Descripcion = p.Descripcion,
                    ImgURL = p.ImgURL,
                    FechaDePresentacion = p.FechaDePresentacion,
                    CantidadDeIntegrantes = p.CantidadDeIntegrantes,
                    ExperienciaPersonal = p.ExperienciaPersonal,
                    Cuotas = p.Cuotas,
                    PrecioPorCuota = p.PrecioPorCuota,
                    MontoSolicitado = p.MontoSolicitado,
                    PorcentajeDeInteres = p.PorcentajeDeInteres,
                    MontoConseguido = p.MontoConseguido,
                    IdSolicitante = p.IdSolicitante,

                    // este dato vuelve como null de la busqueda en el repositorio pero no provoca error porque 
                    // esta definido como virtual en la entidad en el dominio
                    Financiaciones =p. Financiaciones
                        .Select(f => new Models.FinanciacionModel
                        {
                            IdFinanciacion = f.IdFinanciacion,
                            Monto = f.Monto,
                            Fecha = f.Fecha,
                            IdInversor = f.IdInversor
                        })

                }).ToList());
            }

            else
                return NotFound();
        }

    }
}
