using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVC_Presentacion.Models
{
    public class BusquedaProyectosModel
    {
        [Display(Name = "Presentado despues de: ")]
        [DataType(DataType.Date)]
        public DateTime? FechaInicio { get; set; }

        [Display(Name = "Presentado antes de: ")]
        [DataType(DataType.Date)]
        public DateTime? FechaFin { get; set; }

        [Display(Name = "Contiene en titulo: ")]
        public string ContieneEnTitulo { get; set; }

        [Display(Name = "Contiene en descripcion: ")]
        public string ContieneEnDescripcion { get; set; }

        [Display(Name = "Estado: ")]
        [RegularExpression("((^|, )(A|C))+$", ErrorMessage = "Los proyectos solo pueden estar en estado 'A' (abiertos) o 'C' (cerrado).")]
        public string Estado { get; set; }

        [Display(Name = "Monto Solicitado: ")]
        [Range(0, (double)decimal.MaxValue)]
        public decimal? MontoMaximo { get; set; }

        [Display(Name = "CI del solicitante: ")]
        public string CISolicitante { get; set; }

        [Required(ErrorMessage = "Ingrese tipo de busqueda!")]
        [Display(Name = "Tipo de busqueda: ")]
        [RegularExpression("((^|, )(and|or))+$", ErrorMessage = "Los tipos de busqueda pueden ser 'and' o 'or' ")]
        public string TipoDeBusqueda { get; set; }

        /*
        public string TipoDeUsuarioBuscador { get; set; }
        */
    }
}