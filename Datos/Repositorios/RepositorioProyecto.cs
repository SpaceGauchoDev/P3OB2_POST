using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Interfaces;
using Dominio.Entidades;

namespace Datos.Repositorios
{
    public class RepositorioProyecto : IRepositorioProyecto<Proyecto>
    {
        public bool Add(Proyecto objeto)
        {
            if (objeto == null || !objeto.ValidarParaRepositorio())
            {
                return false;
            }

            try
            {
                using (Prestamos_P2P_Context db = new Prestamos_P2P_Context())
                {
                    db.Proyectos.Add(objeto);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public IEnumerable<Proyecto> FindAll()
        {
            using (Prestamos_P2P_Context db = new Prestamos_P2P_Context())
            {
                var pro = db.Proyectos.ToList();
                return pro;
            }
        }

        public Proyecto FindById(int id)
        {
            try
            {
                using (Prestamos_P2P_Context db = new Prestamos_P2P_Context())
                {
                    return db.Proyectos.Find(id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public bool Remove(object objeto)
        {
            throw new NotImplementedException();
        }

        public bool Update(Proyecto objeto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Proyecto> BuscarAnd( DateTime? pFechaInicio,
                                                DateTime? pFechaFin,
                                                string pContieneEnTitulo,
                                                string pContieneEnDescripcion,
                                                string pEstado,
                                                decimal? pMontoMenorOIgualA,
                                                int? pIdUsuario)
        {
            Prestamos_P2P_Context db = new Prestamos_P2P_Context();
            var proyectos = from p in db.Proyectos select p;
            if (pFechaInicio != null && pFechaFin != null)
            {
                proyectos = proyectos.Where(proy => proy.FechaDePresentacion >= pFechaInicio && proy.FechaDePresentacion <= pFechaFin);
            }
            if (pFechaInicio == null && pFechaFin != null)
            {
                proyectos = proyectos.Where(proy => proy.FechaDePresentacion <= pFechaFin);
            }
            if (pFechaInicio != null && pFechaFin == null)
            {
                proyectos = proyectos.Where(proy => proy.FechaDePresentacion >= pFechaInicio);
            }
            if (!String.IsNullOrEmpty(pContieneEnTitulo))
            {
                proyectos = proyectos.Where(proy => proy.Titulo.Contains(pContieneEnTitulo));
            }
            if (!String.IsNullOrEmpty(pContieneEnDescripcion))
            {
                proyectos = proyectos.Where(proy => proy.Descripcion.Contains(pContieneEnDescripcion));
            }
            if (!String.IsNullOrEmpty(pEstado))
            {
                proyectos = proyectos.Where(proy => proy.Estado == pEstado);
            }
            if (pMontoMenorOIgualA != null)
            {
                proyectos = proyectos.Where(proy => proy.MontoSolicitado <= pMontoMenorOIgualA);
            }
            if (pIdUsuario != null)
            {
                proyectos = proyectos.Where(proy => proy.IdSolicitante == pIdUsuario);
            }
            return proyectos.ToList();
        }


        //TODOMDA: arreglar esta busqueda para que funcione como OR en lugar de como AND
        public IEnumerable<Proyecto> BuscarOr(  DateTime? pFechaInicio,
                                                DateTime? pFechaFin,
                                                string pContieneEnTitulo,
                                                string pContieneEnDescripcion,
                                                string pEstado,
                                                decimal? pMontoMenorOIgualA,
                                                int? pIdUsuario)
        {
            Prestamos_P2P_Context db = new Prestamos_P2P_Context();
            
            var proyectos = from p in db.Proyectos select p;
            var proyectosFecha = from p in db.Proyectos select p;
            var proyectosTitulo = from p in db.Proyectos select p;
            var proyectosDescripcion = from p in db.Proyectos select p;
            var proyectosEstado = from p in db.Proyectos select p;
            var proyectosMonto = from p in db.Proyectos select p;
            var proyectosSolicitante = from p in db.Proyectos select p;

            if (pFechaInicio != null && pFechaFin != null)
            {
                proyectosFecha = proyectosFecha.Where(proy => proy.FechaDePresentacion >= pFechaInicio && proy.FechaDePresentacion <= pFechaFin);
                proyectos = proyectos.Union(proyectosFecha);
            }
            if (pFechaInicio == null && pFechaFin != null)
            {
                proyectosFecha = proyectosFecha.Where(proy => proy.FechaDePresentacion <= pFechaFin);
                proyectos = proyectos.Union(proyectosFecha);
            }
            if (pFechaInicio != null && pFechaFin == null)
            {
                proyectosFecha = proyectosFecha.Where(proy => proy.FechaDePresentacion >= pFechaInicio);
                proyectos = proyectos.Union(proyectosFecha);
            }
            if (!String.IsNullOrEmpty(pContieneEnTitulo))
            {
                proyectosTitulo = proyectosTitulo.Where(proy => proy.Titulo.Contains(pContieneEnTitulo));
                proyectos = proyectos.Union(proyectosTitulo);
            }
            if (!String.IsNullOrEmpty(pContieneEnDescripcion))
            {
                proyectosDescripcion = proyectosDescripcion.Where(proy => proy.Descripcion.Contains(pContieneEnDescripcion));
                proyectos = proyectos.Union(proyectosDescripcion);
            }
            if (!String.IsNullOrEmpty(pEstado))
            {
                proyectosEstado = proyectosEstado.Where(proy => proy.Estado == pEstado);
                proyectos = proyectos.Union(proyectosEstado);
            }
            if (pMontoMenorOIgualA != null)
            {
                proyectosMonto = proyectosMonto.Where(proy => proy.MontoSolicitado <= pMontoMenorOIgualA);
                proyectos = proyectos.Union(proyectosMonto);
            }
            if (pIdUsuario != null)
            {
                proyectosSolicitante = proyectosSolicitante.Where(proy => proy.IdSolicitante == pIdUsuario);
                proyectos = proyectos.Union(proyectosSolicitante);
            }

            return proyectos.ToList();
        }

    }
}
