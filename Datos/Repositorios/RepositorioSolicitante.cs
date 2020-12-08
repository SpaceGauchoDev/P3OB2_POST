using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Interfaces;
using Dominio.Entidades;
using System.Data.Entity;

namespace Datos.Repositorios
{
    public class RepositorioSolicitante : IRepositorioUsuario<Solicitante>
    {
        public bool Add(Solicitante objeto)
        {
            if (objeto == null || !objeto.ValidarParaRepositorio()){
                return false;
            }

            try
            {
                using (Prestamos_P2P_Context db = new Prestamos_P2P_Context())
                {
                    db.Solicitantes.Add(objeto);
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

        public IEnumerable<Solicitante> FindAll()
        {
            using (Prestamos_P2P_Context db = new Prestamos_P2P_Context())
            {
                var sol = db.Solicitantes.ToList();
                return sol;
            }
        }

        public Solicitante FindById(int id)
        {
            try
            {
                using (Prestamos_P2P_Context db = new Prestamos_P2P_Context())
                {
                    return db.Solicitantes.Find(id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public Solicitante LoginAttempt(string solicitanteId, string inversorPass)
        {
            Solicitante s = FindById(int.Parse(solicitanteId));

            if (s != null)
            {
                if (s.Pass == inversorPass)
                {
                    return s;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public bool ExistsByEmail(string email)
        {
            try
            {
                using (Prestamos_P2P_Context db = new Prestamos_P2P_Context())
                {
                    var existe = db.Solicitantes.Count(p => p.Email == email);

                    if (existe == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public bool Remove(object id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Solicitante objeto)
        {
            if (objeto != null && objeto.ValidarParaRepositorio())
            {
                using (Prestamos_P2P_Context db = new Prestamos_P2P_Context())
                {
                    var s = db.Solicitantes.Find(objeto.IdUsuario);

                    if (s == null)
                    {
                        return false;
                    }

                    s.Nombre = objeto.Nombre;
                    s.Apellido = objeto.Apellido;
                    s.Pass = objeto.Pass;
                    s.FechaDeNacimiento = objeto.FechaDeNacimiento;
                    s.Email = objeto.Email;
                    s.Cell = objeto.Cell;
                    s.TienePassTemporal = objeto.TienePassTemporal;
                    s.ProyectosDelSolicitante = objeto.ProyectosDelSolicitante;

                    db.Entry(s).State = EntityState.Modified;
                    return db.SaveChanges() > 0;
                }
            }
            return false;
        }
    }
}
