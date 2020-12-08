using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{
    [Table("Solicitantes")]
    public class Solicitante : Usuario
    {
        // navegacion
        // ==========
        public ICollection<Proyecto> ProyectosDelSolicitante { get; set; }

        public Solicitante()
        {

        }
    }
}
