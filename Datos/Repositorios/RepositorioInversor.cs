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
    public class RepositorioInversor : IRepositorioUsuario<Inversor>
    {
        public bool Add(Inversor objeto)
        {
            if (objeto == null || !objeto.ValidarParaRepositorio())
            {
                return false;
            }

            try
            {
                using (Prestamos_P2P_Context db = new Prestamos_P2P_Context())
                {
                    db.Inversores.Add(objeto);
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

        public IEnumerable<Inversor> FindAll()
        {
            throw new NotImplementedException();
        }

        public Inversor FindById(int id)
        {
            try
            {
                using (Prestamos_P2P_Context db = new Prestamos_P2P_Context())
                {
                    return db.Inversores.Find(id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public bool ExistsByEmail(string email)
        {
            try
            {
                using (Prestamos_P2P_Context db = new Prestamos_P2P_Context())
                {
                    var existe = db.Inversores.Count(p => p.Email == email);

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

        public Inversor LoginAttempt(string inversorId, string inversorPass)
        {
            Inversor i = FindById(int.Parse(inversorId));

            if (i != null)
            {
                if (i.Pass == inversorPass)
                {
                    return i;
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


        public bool Remove(object id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Inversor objeto)
        {
            if (objeto != null && objeto.ValidarParaRepositorio())
            {
                using (Prestamos_P2P_Context db = new Prestamos_P2P_Context())
                {
                    var i = db.Inversores.Find(objeto.IdUsuario);

                    if (i == null) {
                        return false;
                    }

                    i.Nombre = objeto.Nombre;
                    i.Apellido = objeto.Apellido;
                    i.Pass = objeto.Pass;
                    i.FechaDeNacimiento = objeto.FechaDeNacimiento;
                    i.Email = objeto.Email;
                    i.Cell = objeto.Cell;
                    i.TienePassTemporal = objeto.TienePassTemporal;
                    i.MaxInvPorProyecto = objeto.MaxInvPorProyecto;
                    i.PresentacionInversor = objeto.PresentacionInversor;
                    i.FinanciacionesDelInversor = objeto.FinanciacionesDelInversor;

                    db.Entry(i).State = EntityState.Modified;
                    return db.SaveChanges() > 0;
                }
            }
            return false;
        }
    }
}
