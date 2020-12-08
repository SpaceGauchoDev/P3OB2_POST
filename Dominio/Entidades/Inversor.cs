using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{
    [Table("Inversores")]
    public class Inversor : Usuario
    {
        [Range(0, (double)decimal.MaxValue)]
        public decimal MaxInvPorProyecto { get; set; }

        [StringLength(500)]
        public string PresentacionInversor { get; set; } // max 500 chars

        // navegacion
        // ==========
        public ICollection<Financiacion> FinanciacionesDelInversor { get; set; }

        public Inversor()
        {

        }

    }
}
