using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Dominio.Entidades;

namespace Datos
{
    public class Prestamos_P2P_Context: DbContext
    {
        public DbSet<Financiacion> Financiaciones { get; set; }
        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<Inversor> Inversores { get; set; }
        public DbSet<Solicitante> Solicitantes { get; set; }

        public Prestamos_P2P_Context() : base("myDB")
        {

        }
    }
}
