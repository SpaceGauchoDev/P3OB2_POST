using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{
    [Table("Financiaciones")]
    public class Financiacion
    {
        [Key]
        public int IdFinanciacion { get; set; }
        [Required]
        public double Monto { get; set; }
        [Required]
        [Column(TypeName = "Date")]
        public DateTime Fecha { get; set; }

        // navegacion
        // ==========
        [ForeignKey("Inversor")]
        public int IdInversor { get; set; }
        public Inversor Inversor { get; set; }

        [ForeignKey("Proyecto")]
        public int IdProyecto { get; set; }
        public Proyecto Proyecto { get; set; }

        public Financiacion()
        {

        }
    }
}
