using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{
    [Table("Proyectos")]
    public class Proyecto
    {
        // properties proyecto
        // ===================
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdProyecto { get; set; }

        [StringLength(1)]
        [Required]
        public string Estado { get; set; } // check A || C

        [StringLength(1)]
        [Required]
        public string TipoDeEquipo { get; set; } // check P || C

        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public string ImgURL { get; set; }

        [Required]
        [Column(TypeName = "Date")]
        public DateTime FechaDePresentacion { get; set; }

        // properties proyecto - cooperativo
        // =================================
        public int CantidadDeIntegrantes { get; set; }

        // properties proyecto - personal
        // ==============================
        public string ExperienciaPersonal { get; set; }

        // properties prestamo solicitado
        // ==============================
        [Required]
        public int Cuotas { get; set; }

        [Range(0, (double)decimal.MaxValue)]
        [Required]
        public decimal PrecioPorCuota { get; set; }

        [Range(0, (double)decimal.MaxValue)]
        [Required]
        public decimal MontoSolicitado { get; set; }

        [Range(0, (double)decimal.MaxValue)]
        [Required]
        public decimal PorcentajeDeInteres { get; set; }

        [Range(0, (double)decimal.MaxValue)]
        [Required]
        public decimal MontoConseguido { get; set; }

        // navegacion
        // ==========
        [ForeignKey("Solicitante")]
        public int IdSolicitante { get; set; }
        public Solicitante Solicitante { get; set; }

        // esto es virtual para que cuando devolvemos busquedas del repositorio para
        // webapi no pida Financiaciones como "cant be null"
        public virtual ICollection<Financiacion> Financiaciones { get; set; }

        public bool ValidarParaRepositorio()
        {
            bool result = false;

            // validamos que los valores monetarios sean positivos 
            result = ValidarMontos(Cuotas, PrecioPorCuota, PorcentajeDeInteres, MontoConseguido);

            // validamos que la composicion de los equipos cumpla con las reglas de negocio
            result = (result && ValidarEquipo(TipoDeEquipo, CantidadDeIntegrantes, ExperienciaPersonal));

            // validamos que el proyecto haya sido presentado por lo menos antes que hoy
            result = (result && (FechaDePresentacion.Date <= DateTime.Today.Date));

            // validamos el estado del proyecto en relacion a los montos
            result = (result && (ValidarEstado(Estado, MontoSolicitado, MontoConseguido)));

            return result;
        }

        public bool ValidarMontos(  int cuotas,
                                    decimal precioPorCuota, 
                                    decimal porcentaje,
                                    decimal montoConseguido)
        {
            bool result = false;

            result = (Cuotas >= 1);

            result = (result && (PrecioPorCuota >= 0));

            result = (result && (PorcentajeDeInteres >= 0));

            result = (result && (MontoConseguido >= 0));

            return result;
        }

        public bool ValidarEquipo(string tipoDeEquipo, int cantidadDeIntegrantes, string experiencia)
        {
            bool result = false;

            result = (tipoDeEquipo == "P" || tipoDeEquipo == "C");

            if (result && tipoDeEquipo == "P")
            {
                result = (cantidadDeIntegrantes == 1);
            }

            if (result && tipoDeEquipo == "P")
            {
                result = (experiencia != null && experiencia != "");
            }

            if (result && tipoDeEquipo == "C")
            {
                result = (cantidadDeIntegrantes > 1);
            }

            return result;
        }

        public bool ValidarEstado(string estado, decimal montoSolicitado, decimal montoConseguido)
        {
            bool result = false;

            result = (estado == "A" || estado == "C");

            if (result && estado == "A")
            {
                result = (montoConseguido < montoSolicitado);
            }

            if (result && estado == "C")
            {
                result = (montoConseguido >= montoSolicitado);
            }

            return result;
        }

    }
}
